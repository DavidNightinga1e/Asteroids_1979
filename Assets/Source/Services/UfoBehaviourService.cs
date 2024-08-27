using System;
using System.Collections.Generic;
using Source.Factories;
using Source.Interfaces;
using Source.Models;
using Source.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ServiceLocators
{
	public class UfoBehaviourService : Service, IUpdatable, IFixedUpdatable, IEnemyLifetimeBroadcaster
	{
		public event Action<EnemyModel> OnEnemySpawn;
		public event Action<EnemyModel> OnEnemyDestroy;
		
		private readonly List<UfoModel> _models = new();

		private readonly IBoundsProvider _boundsProvider;
		private readonly IUfoSettingsProvider _ufoSettings;
		private readonly IMovable _playerMovable;

		private float _nextSpawnTime;

		public UfoBehaviourService(IUfoSettingsProvider ufoSettings, IBoundsProvider boundsProvider,
			IMovable playerMovable)
		{
			_ufoSettings = ufoSettings;
			_boundsProvider = boundsProvider;
			_playerMovable = playerMovable;
		}

		public void Update()
		{
			UpdateSpawn();
		}

		public void FixedUpdate()
		{
			UpdateMovement();
		}
		
		private void UpdateSpawn()
		{
			if (_nextSpawnTime > Time.time)
				return;

			UfoModel ufoModel = UfoFactory.CreateUfo(_ufoSettings, _boundsProvider);
			_models.Add(ufoModel);
			ufoModel.OnCollided += OnCollided;
			ufoModel.OnDestroyed += OnDestroyed;
			OnEnemyDestroy?.Invoke(ufoModel);

			_nextSpawnTime = Time.time + _ufoSettings.SpawnDelay;
		}

		private void OnCollided(ICollisionBroadcaster obj)
		{
			var ufoModel = (UfoModel)obj;
			Object.Destroy(ufoModel.View.gameObject);
		}

		private void OnDestroyed(IDestroyBroadcaster obj)
		{
			var ufoModel = (UfoModel)obj;
			_models.Remove(ufoModel);
			OnEnemyDestroy?.Invoke(ufoModel);
			ServiceLocator.GetService<ScoreService>().OnEnemyHit(ufoModel);
		}

		private void UpdateMovement()
		{
			foreach (UfoModel model in _models)
			{
				UfoView view = model.UfoView;

				Vector2 position = view.GetPosition();
				Vector2 playerPosition = _playerMovable.GetPosition();
				Vector2 direction = playerPosition - position; 
				position += direction.normalized * _ufoSettings.Speed;
				view.SetPosition(position);
			}
		}

		public void OnGameRestart()
		{
			foreach (UfoModel ufoModel in _models)
			{
				Object.Destroy(ufoModel.View.gameObject);
			}
		}
	}
}