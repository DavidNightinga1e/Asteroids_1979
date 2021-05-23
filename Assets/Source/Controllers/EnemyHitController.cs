using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class EnemyHitController : MonoBehaviour
    {
        private void Awake()
        {
            EventPool.OnEnemyDestroyed.AddListener(OnEnemyDestroyed);
        }

        private void OnEnemyDestroyed(EnemyComponent enemyComponent)
        {
            Destroy(enemyComponent.gameObject);
        }
    }
}