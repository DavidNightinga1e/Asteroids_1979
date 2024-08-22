﻿using System.Collections.Generic;
using Source.Components;
using Source.Events;
using Source.Interfaces;
using Source.Utilities;
using UnityEngine;

namespace Source.Controllers
{
	public class EnemyBoundsService : IService, IAwakable, IUpdatable
	{
		private readonly List<EnemyComponent> _enemyComponents = new();

		private readonly IBoundsProvider _boundsProvider;

		public EnemyBoundsService(IBoundsProvider boundsProvider)
		{
			_boundsProvider = boundsProvider;
		}

		public void Awake()
		{
			EventPool.OnEnemySpawned.AddListener(OnEnemySpawned);
			EventPool.OnGameStarted.AddListener(OnGameStarted);
			EventPool.OnEnemyDestroyed.AddListener(OnEnemyDestroyed);
		}

		private void OnEnemyDestroyed(EnemyComponent arg0)
		{
			_enemyComponents.Remove(arg0);
		}

		private void OnGameStarted()
		{
			_enemyComponents.Clear();
		}

		public void Update()
		{
			Vector2 extents = _boundsProvider.GetBounds();
			foreach (EnemyComponent enemyComponent in _enemyComponents)
			{
				Vector2 p = enemyComponent.TargetRigidbody2D.position;
				const float eps = 0.1f;
				if (p.x > extents.x + eps || p.x < -extents.x - eps ||
				    p.y > extents.y + eps || p.y < -extents.y - eps)
				{
					Object.Destroy(enemyComponent.gameObject);
				}
			}
		}

		private void OnEnemySpawned(EnemyComponent arg0)
		{
			_enemyComponents.Add(arg0);
		}
	}
}