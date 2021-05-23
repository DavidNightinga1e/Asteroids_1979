using System.Collections.Generic;
using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class BulletBoundsController : MonoBehaviour
    {
        private readonly List<BulletComponent> _bulletComponents = new List<BulletComponent>();
        private BoundsComponent _boundsComponent;

        private void Awake()
        {
            this.AutoFindComponent(out _boundsComponent);

            EventPool.OnBulletSpawned.AddListener(OnBulletSpawned);
            EventPool.OnBulletDestroyed.AddListener(OnBulletDestroyed);
            EventPool.OnGameStarted.AddListener(OnGameStarted);
        }

        private void OnBulletDestroyed(BulletComponent arg0)
        {
            _bulletComponents.Remove(arg0);
        }

        private void OnGameStarted()
        {
            _bulletComponents.Clear();
        }

        private void Update()
        {
            foreach (var bullet in _bulletComponents)
                if (!_boundsComponent.Bounds.Contains(bullet.transform.position))
                    Destroy(bullet.gameObject);
        }

        private void OnBulletSpawned(BulletComponent arg0)
        {
            _bulletComponents.Add(arg0);
        }
    }
}