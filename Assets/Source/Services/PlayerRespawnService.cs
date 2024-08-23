using System;
using System.Collections;
using Source.Components;
using Source.Events;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Controllers
{
	public class PlayerRespawnService : IService, IAwakable, IStartable
	{
		private readonly Vector2 _spawnPosition = Vector2.zero;

		private readonly PlayerComponent _playerComponent;

		public PlayerRespawnService(PlayerComponent playerComponent)
		{
			_playerComponent = playerComponent;
		}

		public void Awake()
		{
			EventPool.OnGameStarted.AddListener(OnGameStarted);
			EventPool.OnPlayerDestroyed.AddListener(OnPlayerDestroyed);
			EventPool.OnGameOver.AddListener(OnGameOver);
		}

		public void Start()
		{
			EventPool.OnGameStarted.Invoke();
		}

		private void OnGameOver()
		{
			_playerComponent.gameObject.SetActive(false);
		}

		private void OnPlayerDestroyed()
		{
			MoveToSpawn();
		}

		private void OnGameStarted()
		{
			_playerComponent.gameObject.SetActive(true);
			MoveToSpawn();
		}

		private void MoveToSpawn()
		{
			_playerComponent.transform.position = _spawnPosition;
			_playerComponent.TargetRigidbody2D.velocity = Vector2.zero;
			_playerComponent.transform.rotation = Quaternion.identity;
		}
	}
}