using System.Collections.Generic;
using Source.Components;
using Source.Events;
using Source.Interfaces;
using UnityEngine;

namespace Source.Controllers
{
	public class BulletBoundsService : IService, IUpdatable, IAwakable
	{
		private readonly List<BulletComponent> _bulletComponents = new();
		private readonly IBoundsProvider _boundsProvider;

		public BulletBoundsService(IBoundsProvider boundsProvider)
		{
			_boundsProvider = boundsProvider;
		}

		public void Awake()
		{
			EventPool.OnBulletSpawned.AddListener(OnBulletSpawned);
			EventPool.OnBulletDestroyed.AddListener(OnBulletDestroyed);
			EventPool.OnGameStarted.AddListener(OnGameStarted);
		}

		private void OnBulletDestroyed(BulletComponent arg0)
		{
			_bulletComponents.Remove(arg0);
		}

		private void OnGameStarted()
		{
			_bulletComponents.Clear();
		}

		public void Update()
		{
			Vector2 extents = _boundsProvider.GetBounds();

			foreach (BulletComponent bullet in _bulletComponents)
			{
				Vector2 p = bullet.TargetRigidbody2D.position;
				if (p.x > extents.x || p.y > extents.y ||
				    p.x < -extents.x || p.y < -extents.y)
					Object.Destroy(bullet.gameObject);
			}
		}

		private void OnBulletSpawned(BulletComponent arg0)
		{
			_bulletComponents.Add(arg0);
		}
	}
}