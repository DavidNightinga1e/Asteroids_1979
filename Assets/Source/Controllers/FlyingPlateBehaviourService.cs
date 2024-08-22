using System.Collections.Generic;
using Source.Components;
using Source.Events;
using Source.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Source.Controllers
{
	public class FlyingPlateBehaviourService : IAwakable, IUpdatable, IService
	{
		private readonly EnemySpawnSettingsComponent _settings;
		private readonly PlayerComponent _playerComponent;
		private readonly IBoundsProvider _boundsProvider;

		private float _nextSpawnTime;

		private readonly List<FlyingPlateComponent> _flyingPlateComponents = new();

		private (float min, float max) WaitTimeRange =>
		(
			_settings.currentFlyingPlateSpawnCooldown - _settings.FlyingPlateSpawnDispersion,
			_settings.currentFlyingPlateSpawnCooldown + _settings.FlyingPlateSpawnDispersion);

		public FlyingPlateBehaviourService(EnemySpawnSettingsComponent enemySpawnSettingsComponent, PlayerComponent playerComponent, IBoundsProvider boundsProvider)
		{
			_settings = enemySpawnSettingsComponent;
			_playerComponent = playerComponent;
			_boundsProvider = boundsProvider;
		}

		public void Awake()
		{
			EventPool.OnEnemyHit.AddListener(OnEnemyHit);
			EventPool.OnEnemySpawned.AddListener(OnEnemySpawned);
			EventPool.OnEnemyDestroyed.AddListener(OnEnemyDestroyed);
		}

		private void OnEnemyDestroyed(EnemyComponent arg0)
		{
			if (arg0 is FlyingPlateComponent flyingPlateComponent)
			{
				_flyingPlateComponents.Remove(flyingPlateComponent);
			}
		}

		private void OnEnemySpawned(EnemyComponent arg0)
		{
			if (arg0 is FlyingPlateComponent flyingPlateComponent)
			{
				_flyingPlateComponents.Add(flyingPlateComponent);
			}
		}

		public void Update()
		{
			UpdateMovement();
			UpdateSpawn();
		}

		private void UpdateSpawn()
		{
			if (_nextSpawnTime > Time.time)
				return;

			if (!_settings.spawn)
				return;

			Vector2 point = RandomPointOnBounds();
			int i = Random.Range(0, _settings.flyingPlatePrefabs.Count);
			GameObject instance = Object.Instantiate(
				_settings.flyingPlatePrefabs[i].gameObject,
				point,
				Quaternion.identity);
			var flyingPlateComponent = instance.GetComponent<FlyingPlateComponent>();
			EventPool.OnEnemySpawned.Invoke(flyingPlateComponent);


			(float min, float max) = WaitTimeRange;
			float waitTime = Random.Range(min, max);
			_nextSpawnTime = Time.time + waitTime;
		}

		private void UpdateMovement()
		{
			foreach (FlyingPlateComponent c in _flyingPlateComponents)
			{
				// todo: to rigidbody
				c.transform.Translate((_playerComponent.transform.position - c.transform.position).normalized *
				                      (_settings.flyingPlateSpeed * Time.deltaTime));
			}
		}

		private Vector2 RandomPointOnBounds()
		{
			Vector2 spawnExtents = _boundsProvider.GetBounds();
			Vector2 random = Random.insideUnitCircle.normalized;
			return new Vector2(spawnExtents.x * random.x, spawnExtents.y * random.y);
		}

		private void OnEnemyHit(EnemyComponent arg0)
		{
			if (arg0 is not FlyingPlateComponent flyingPlateComponent)
				return;

			Object.Destroy(flyingPlateComponent.gameObject);
		}
	}
}