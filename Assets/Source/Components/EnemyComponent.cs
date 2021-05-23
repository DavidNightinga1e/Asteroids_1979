using System;
using Source.Events;
using UnityEngine;

namespace Source.Components
{
    public class EnemyComponent : GameComponentBase
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            EventPool.OnEnemyDestroyed.Invoke(this);
        }

        private void OnDestroy()
        {
            EventPool.OnEnemyDestroyed.Invoke(this);
        }
    }
}