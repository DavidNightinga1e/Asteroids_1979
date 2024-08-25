using System.Collections.Generic;
using Source.Interfaces;
using Source.Models;
using UnityEngine;

namespace ServiceLocators
{
	public class EnemyBoundsService : Service, IUpdatable
	{
		private readonly List<EnemyModel> _models = new();

		private readonly IBoundsProvider _boundsProvider;

		public EnemyBoundsService(IBoundsProvider boundsProvider,
			params IEnemyLifetimeBroadcaster[] lifetimeBroadcasters)
		{
			_boundsProvider = boundsProvider;

			foreach (IEnemyLifetimeBroadcaster broadcaster in lifetimeBroadcasters)
			{
				broadcaster.OnEnemySpawn += OnSpawn; 
				broadcaster.OnEnemyDestroy += OnEnemyDestroyed;
			}
		}

		private void OnEnemyDestroyed(EnemyModel model)
		{
			_models.Remove(model);
		}

		private void OnSpawn(EnemyModel model)
		{
			_models.Add(model);
		}

		public void Update()
		{
			Vector2 extents = _boundsProvider.GetBounds();
			foreach (EnemyModel model in _models)
			{
				Vector2 p = model.View.GetPosition();
				const float eps = 0.1f;
				if (p.x > extents.x + eps || p.x < -extents.x - eps ||
				    p.y > extents.y + eps || p.y < -extents.y - eps)
				{
					Object.Destroy(model.View.gameObject);
				}
			}
		}
	}
}