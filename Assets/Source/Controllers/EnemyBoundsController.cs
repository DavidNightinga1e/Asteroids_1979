using System.Collections.Generic;
using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class EnemyBoundsController : MonoBehaviour
    {
        private BoundsComponent _boundsComponent;
        private readonly List<EnemyComponent> _enemyComponents = new List<EnemyComponent>();

        private void Awake()
        {
            this.AutoFindComponent(out _boundsComponent);

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
            foreach (var enemyComponent in _enemyComponents)
                if (!_boundsComponent.Bounds.Contains(enemyComponent.transform.position))
                    Destroy(enemyComponent.gameObject);
        }

        private void OnEnemySpawned(EnemyComponent arg0)
        {
            _enemyComponents.Add(arg0);
        }
    }
}