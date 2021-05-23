using System;
using Source.Events;
using UnityEngine;

namespace Source.Components
{
    public class BulletComponent : GameComponentBase
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            EventPool.OnBulletHit.Invoke(this);
        }

        private void OnDestroy()
        {
            EventPool.OnBulletDestroyed.Invoke(this);
        }
    }
}