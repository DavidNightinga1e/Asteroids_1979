using System;
using System.Collections.Generic;
using Source.Factories;
using Source.Interfaces;
using Source.Models;
using Source.Views;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace ServiceLocators
{
	public class AsteroidBehaviourService : Service, IUpdatable, IFixedUpdatable, IEnemyLifetimeBroadcaster
	{
		public event Action<EnemyModel> OnEnemySpawn;
		public event Action<EnemyModel> OnEnemyDestroy;

		private readonly List<AsteroidModel> _asteroids = new();

		private readonly IAsteroidFactorySettingsProvider _asteroidFactorySettings;
		private readonly IBoundsProvider _boundsProvider;

		private float _nextSpawnTime;

		public AsteroidBehaviourService(
			IAsteroidFactorySettingsProvider asteroidFactorySettings,
			IBoundsProvider boundsProvider)
		{
			_asteroidFactorySettings = asteroidFactorySettings;
			_boundsProvider = boundsProvider;
		}

		public void OnGameRestart()
		{
			foreach (AsteroidModel asteroid in _asteroids)
			{
				Object.Destroy(asteroid.View.gameObject);
			}
		}

		public void Update()
		{
			UpdateSpawn();
		}

		public void FixedUpdate()
		{
			UpdateMovement();
		}

		private void UpdateMovement()
		{
			foreach (AsteroidModel model in _asteroids)
			{
				AsteroidView view = model.AsteroidView;

				Vector2 position = view.GetPosition();
				position += model.Direction;
				view.SetPosition(position);

				float rotation = view.GetRotation();
				rotation += model.AngularSpeed;
				view.SetRotation(rotation);
			}
		}

		private void UpdateSpawn()
		{
			if (_nextSpawnTime > Time.time)
				return;

			AsteroidModel asteroid = AsteroidFactory.CreateRandomAsteroid(_asteroidFactorySettings, _boundsProvider);
			AddAsteroid(asteroid);

			float waitTime = Random.Range(_asteroidFactorySettings.SpawnMinDelay,
				_asteroidFactorySettings.SpawnMaxDelay);
			_nextSpawnTime = Time.time + waitTime;
		}

		private void AsteroidCollisionHandler(ICollisionBroadcaster obj)
		{
			var model = (AsteroidModel)obj;

			int parentSize = model.Size;
			if (parentSize > 0)
			{
				int numberOfAsteroids = Random.Range(1, 4);
				for (var i = 0; i < numberOfAsteroids; i++)
				{
					AsteroidModel childAsteroid =
						AsteroidFactory.CreateChildAsteroid(_asteroidFactorySettings, model.View, model);
					AddAsteroid(childAsteroid);
				}
			}

			Object.Destroy(model.View.gameObject);
		}

		private void AddAsteroid(AsteroidModel model)
		{
			_asteroids.Add(model);
			model.OnDestroyed += AsteroidDestroyedHandler;
			model.OnCollided += AsteroidCollisionHandler;
			OnEnemySpawn?.Invoke(model);
		}

		private void AsteroidDestroyedHandler(IDestroyBroadcaster obj)
		{
			var asteroidModel = (AsteroidModel)obj;
			asteroidModel.OnDestroyed -= AsteroidDestroyedHandler;
			_asteroids.Remove(asteroidModel);
			OnEnemyDestroy?.Invoke(asteroidModel);
			ServiceLocator.GetService<ScoreService>().OnEnemyHit(asteroidModel);
		}
	}
}