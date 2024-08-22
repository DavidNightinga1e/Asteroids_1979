using System.Collections.Generic;
using Source.Components;
using Source.Events;
using Source.Utilities;
using UnityEngine;

namespace Source.Controllers
{
	public class BulletBoundsController : IController, IUpdatable, IAwakable
	{
		private readonly List<BulletComponent> _bulletComponents = new();
		private Camera _cameraComponent;

		public void Awake()
		{
			_cameraComponent = Camera.main;

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
			Vector2 extents = _cameraComponent.GetOrthographicExtents();

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