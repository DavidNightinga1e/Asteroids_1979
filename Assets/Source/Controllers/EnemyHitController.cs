using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class EnemyHitController : MonoBehaviour
    {
        private void Awake()
        {
            EventPool.OnEnemyHit.AddListener(OnEnemyHit);
        }

        private void OnEnemyHit(EnemyComponent enemyComponent)
        {
            Destroy(enemyComponent.gameObject);
        }
    }
}