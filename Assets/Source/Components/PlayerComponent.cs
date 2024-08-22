using System;
using Source.Events;
using UnityEngine;

namespace Source.Components
{
    public class PlayerComponent : GameComponentBase
    {
        [SerializeField] private GameObject bulletPrefab;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            EventPool.OnPlayerDestroyed.Invoke();
        }

        public GameObject BulletPrefab => bulletPrefab;
    }
}