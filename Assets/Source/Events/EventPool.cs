using Source.Components;
using UnityEngine.Events;

namespace Source.Events
{
    public static class EventPool
    {
        public static readonly UnityEvent OnPlayerDestroyed = new UnityEvent();
        public static readonly UnityEvent OnGameOver = new UnityEvent();
        public static readonly UnityEvent OnGameStarted = new UnityEvent();
        public static readonly UnityEvent<BulletComponent> OnBulletSpawned = new UnityEvent<BulletComponent>();
        public static readonly UnityEvent<BulletComponent> OnBulletDestroyed = new UnityEvent<BulletComponent>();
        public static readonly UnityEvent<EnemyComponent> OnEnemyDestroyed = new UnityEvent<EnemyComponent>();
        public static readonly UnityEvent<EnemyComponent> OnEnemyHit = new UnityEvent<EnemyComponent>();
        public static readonly UnityEvent<EnemyComponent> OnEnemySpawned = new UnityEvent<EnemyComponent>();
        public static readonly UnityEvent<BulletComponent> OnBulletHit = new UnityEvent<BulletComponent>();
    }
}