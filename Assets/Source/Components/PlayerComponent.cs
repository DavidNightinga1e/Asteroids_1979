using System;
using Source.Events;
using UnityEngine;

namespace Source.Components
{
    public class PlayerComponent : GameComponentBase
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            EventPool.OnPlayerDestroyed.Invoke();
        }
    }
}