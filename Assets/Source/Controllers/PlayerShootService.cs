using System.Collections;
using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
	public class PlayerShootService : IService, IAwakable, IUpdatable
	{
		private const float SpawnOffset = 2.2f;
		private const float ShootImpulse = 100f;
		private const float ReloadTime = 0.3f;
		private const float BulletSize = 0.4f;

		private PlayerComponent _playerComponent;
		private bool _isGameStarted;
		private float _nextReadyTime;

		private bool IsReady => Time.time > _nextReadyTime;

		public void Awake()
		{
			_playerComponent = Object.FindObjectOfType<PlayerComponent>();

			EventPool.OnGameStarted.AddListener(() => _isGameStarted = true);
			EventPool.OnGameOver.AddListener(() => _isGameStarted = false);
		}

		public void Update()
		{
			if (Input.GetButton("Fire") && IsReady && _isGameStarted)
				Fire();
		}

		private void Fire()
		{
			_nextReadyTime = Time.time + ReloadTime;

			Vector3 spawnPosition = _playerComponent.transform.position + _playerComponent.transform.up * SpawnOffset;
			Quaternion spawnRotation = _playerComponent.transform.rotation;
			GameObject instance = Object.Instantiate(_playerComponent.BulletPrefab.gameObject, spawnPosition, spawnRotation);
			instance.transform.localScale = Vector3.one * BulletSize;
			var bulletComponent = instance.GetComponent<BulletComponent>();
			bulletComponent.TargetRigidbody2D.AddForce(bulletComponent.transform.up * ShootImpulse,
				ForceMode2D.Impulse);

			EventPool.OnBulletSpawned.Invoke(bulletComponent);
		}
	}
}