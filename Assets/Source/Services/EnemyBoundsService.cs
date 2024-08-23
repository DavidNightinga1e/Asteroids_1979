using System.Collections.Generic;
using Source.Components;
using Source.Interfaces;
using Source.Models;
using Source.Utilities;
using Source.Views;
using UnityEngine;

namespace ServiceLocators
{
	public class EnemyBoundsService : Service, IUpdatable
	{
		private readonly List<EnemyModel> _models = new();

		private readonly IBoundsProvider _boundsProvider;
		private readonly IEnemyLifetimeBroadcaster _enemyLifetimeBroadcaster;

		public EnemyBoundsService(IBoundsProvider boundsProvider, IEnemyLifetimeBroadcaster lifetimeBroadcaster)
		{
			_boundsProvider = boundsProvider;
			_enemyLifetimeBroadcaster = lifetimeBroadcaster;

			lifetimeBroadcaster.OnEnemySpawn += OnSpawn; // todo add unsub
			lifetimeBroadcaster.OnEnemyDestroy += OnEnemyDestroyed; // todo add unsub
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