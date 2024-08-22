using System.Collections.Generic;
using Source.Components;
using Source.Events;
using Source.Utilities;
using UnityEngine;

namespace Source.Controllers
{
	public class EnemyBoundsController : MonoBehaviour
	{
		private readonly List<EnemyComponent> _enemyComponents = new();

		private Camera _cameraComponent;

		private void Awake()
		{
			this.AutoFindComponent(out _cameraComponent);

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

		private void Update()
		{
			Vector2 extents = _cameraComponent.GetOrthographicExtents();
			foreach (EnemyComponent enemyComponent in _enemyComponents)
			{
				Vector2 p = enemyComponent.TargetRigidbody2D.position;
				const float eps = 0.1f;
				if (p.x > extents.x + eps || p.x < -extents.x - eps ||
				    p.y > extents.y + eps || p.y < -extents.y - eps)
				{
					Destroy(enemyComponent.gameObject);
				}
			}
		}

		private void OnEnemySpawned(EnemyComponent arg0)
		{
			_enemyComponents.Add(arg0);
		}
	}
}