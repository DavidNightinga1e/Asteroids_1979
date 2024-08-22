﻿using System;
using System.Collections;
using System.Collections.Generic;
using Source.Components;
using Source.Events;
using Source.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Source.Controllers
{
	public class AsteroidBehaviourController : IController, IAwakable, IUpdatable
	{
		private EnemySpawnSettingsComponent _settings;
		private Camera _camera;

		private float _nextSpawnTime;

		private (float min, float max) WaitTimeRange =>
		(
			_settings.currentAsteroidSpawnCooldown - _settings.AsteroidSpawnDispersion,
			_settings.currentAsteroidSpawnCooldown + _settings.AsteroidSpawnDispersion);

		public void Awake()
		{
			_settings = Object.FindObjectOfType<EnemySpawnSettingsComponent>();
			_camera = Camera.main;

			EventPool.OnEnemyHit.AddListener(OnEnemyHit);
		}

		public void Update()
		{
			if (_nextSpawnTime > Time.time)
				return;

			if (!_settings.spawn)
				return;
			
			(Vector2 point, float rotation) = RandomPointAndDirectionOnBounds();
			List<AsteroidComponent> asteroidPrefabs = _settings.asteroidPrefabs;
			int asteroidIndex = Random.Range(0, asteroidPrefabs.Count);
			GameObject instance = Object.Instantiate(
				asteroidPrefabs[asteroidIndex].gameObject,
				point,
				Quaternion.Euler(0, 0, rotation));
			var asteroidComponent = instance.GetComponent<AsteroidComponent>();
			InitializeAsteroid(asteroidComponent, _settings.asteroidSizes.Count);
			EventPool.OnEnemySpawned.Invoke(asteroidComponent);
			
			var (min, max) = WaitTimeRange;
			var waitTime = Random.Range(min, max);
			_nextSpawnTime = Time.time + waitTime;
		}

		private void InitializeAsteroid(AsteroidComponent asteroidComponent, int maxSize)
		{
			asteroidComponent.TargetRigidbody2D.AddForce(
				asteroidComponent.transform.up *
				Random.Range(_settings.asteroidMinForce, _settings.asteroidMaxForce));
			asteroidComponent.TargetRigidbody2D.AddTorque(Random.Range(_settings.asteroidMinTorque,
				_settings.asteroidMaxTorque));
			asteroidComponent.Size = Random.Range(0, maxSize);
			asteroidComponent.transform.localScale = Vector3.one * _settings.asteroidSizes[asteroidComponent.Size];
		}

		private (Vector2, float) RandomPointAndDirectionOnBounds()
		{
			Vector2 spawnExtents = _camera.GetOrthographicExtents();

			float RandomOnX() => Random.Range(-spawnExtents.x, spawnExtents.x);
			float RandomOnY() => Random.Range(-spawnExtents.x, spawnExtents.x);

			var side = Random.Range(0, 4);
			var point = side switch
			{
				0 => new Vector2(-spawnExtents.x, RandomOnY()),
				1 => new Vector2(RandomOnX(), spawnExtents.y),
				2 => new Vector2(spawnExtents.x, RandomOnY()),
				3 => new Vector2(RandomOnX(), -spawnExtents.y),
				_ => throw new Exception()
			};

			var direction = Random.Range(-45, 45) + side switch
			{
				0 => -90,
				1 => -180,
				2 => -270,
				3 => 0,
				_ => throw new Exception()
			};

			return (point, direction);
		}

		private void OnEnemyHit(EnemyComponent arg0)
		{
			if (!(arg0 is AsteroidComponent asteroidComponent))
				return;

			var parentSize = asteroidComponent.Size;
			if (parentSize > 0)
			{
				var numberOfAsteroids = Random.Range(1, 4);
				for (int i = 0; i < numberOfAsteroids; i++)
				{
					var asteroidType = Random.Range(0, _settings.asteroidPrefabs.Count);
					var instance = Object.Instantiate(
						_settings.asteroidPrefabs[asteroidType].gameObject,
						asteroidComponent.transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)),
						GetRandomRotation());

					var newAsteroid = instance.GetComponent<AsteroidComponent>();
					InitializeAsteroid(newAsteroid, parentSize);
					EventPool.OnEnemySpawned.Invoke(newAsteroid);
				}
			}

			Object.Destroy(asteroidComponent.gameObject);
		}

		private static Quaternion GetRandomRotation() => Quaternion.Euler(0, 0, Random.Range(0, 360));
	}
}