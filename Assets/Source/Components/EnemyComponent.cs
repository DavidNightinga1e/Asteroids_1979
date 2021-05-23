using Source.Events;
using UnityEngine;

namespace Source.Components
{
    public class EnemyComponent : GameComponentBase
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            EventPool.OnEnemyHit.Invoke(this);
        }
    }
}