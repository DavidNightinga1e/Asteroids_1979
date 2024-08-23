using System.Collections.Generic;
using Source.Adapters;
using Source.Factories;
using Source.Interfaces;
using Source.Models;
using Source.Views;
using UnityEngine;

namespace ServiceLocators
{
	public class PlayerShootService : Service, IUpdatable
	{
		private readonly List<BulletModel> _bullets = new();

		private const float Speed = 1f;
		private const float ReloadTime = 0.3f;
		private const float BulletSize = 0.4f;

		private readonly PlayerView _playerView;
		private readonly IBoundsProvider _boundsProvider;
		private readonly IPlayerInputProvider _playerInputProvider;

		private bool _isGameStarted;
		private float _nextReadyTime;

		private bool IsReady => Time.time > _nextReadyTime;

		public PlayerShootService(PlayerView playerView, IBoundsProvider boundsProvider, IPlayerInputProvider playerInputProvider)
		{
			_playerView = playerView;
			_boundsProvider = boundsProvider;
			_playerInputProvider = playerInputProvider;
		}
		
		public void Update()
		{
			if (_playerInputProvider.IsFire && IsReady)
				Fire();

			UpdateMovement();
			UpdateBounds();
		}

		private void UpdateBounds()
		{
			Vector2 extents = _boundsProvider.GetBounds();
			foreach (BulletModel bullet in _bullets)
			{
				Vector2 p = bullet.View.GetPosition();
				if (p.x > extents.x || p.y > extents.y ||
				    p.x < -extents.x || p.y < -extents.y)
					Object.Destroy(bullet.View.gameObject);
			}
		}

		private void UpdateMovement()
		{
			foreach (BulletModel model in _bullets)
			{
				Vector2 position = model.View.GetPosition();
				Vector3 direction = model.View.Rb.transform.up;
				var direction2d = new Vector2(direction.x, direction.y);
				position += direction2d * Speed;
				model.View.SetPosition(position);
			}
		}

		private void Fire()
		{
			_nextReadyTime = Time.time + ReloadTime;
			BulletModel bulletModel = BulletFactory.CreateBullet(new PlayerBulletPrefabProviderAdapter(_playerView),
				_playerView, BulletSize);
			_bullets.Add(bulletModel);
			bulletModel.OnCollided += OnBulletCollided;
			bulletModel.OnDestroyed += OnBulletDestroyed;
		}

		private void OnBulletDestroyed(IDestroyBroadcaster obj)
		{
			_bullets.Remove((BulletModel)obj);
		}

		private void OnBulletCollided(ICollisionBroadcaster obj)
		{
			Object.Destroy(((BulletModel)obj).View.gameObject);
		}
	}
}