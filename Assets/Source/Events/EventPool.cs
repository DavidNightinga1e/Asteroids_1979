using Source.Components;
using UnityEngine.Events;

namespace Source.Events
{
    public static class EventPool
    {
        public static readonly UnityEvent OnPlayerDestroyed = new();
        public static readonly UnityEvent OnGameOver = new();
        public static readonly UnityEvent OnGameStarted = new();
        public static readonly UnityEvent<BulletComponent> OnBulletSpawned = new();
        public static readonly UnityEvent<BulletComponent> OnBulletDestroyed = new();
        public static readonly UnityEvent<EnemyComponent> OnEnemyDestroyed = new();
        public static readonly UnityEvent<EnemyComponent> OnEnemyHit = new();
        public static readonly UnityEvent<EnemyComponent> OnEnemySpawned = new();
        public static readonly UnityEvent<BulletComponent> OnBulletHit = new();
    }
}