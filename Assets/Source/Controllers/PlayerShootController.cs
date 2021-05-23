using System.Collections;
using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class PlayerShootController : MonoBehaviour
    {
        public BulletComponent bulletPrefab;
        public float spawnOffset = 1f;
        public float shootImpulse = 100f;
        public float reloadTime = 0.3f;
        public float bulletSize = 0.4f;

        private PlayerComponent _playerComponent;
        private bool _isReady = true;
        private bool _isGameStarted;

        private void Awake()
        {
            this.AutoFindComponent(out _playerComponent);

            EventPool.OnGameStarted.AddListener(() => _isGameStarted = true);
            EventPool.OnGameOver.AddListener(() => _isGameStarted = false);
        }

        private void Update()
        {
            if (Input.GetButton("Fire") && _isReady && _isGameStarted)
                Fire();
        }

        private void Fire()
        {
            Reload();

            var spawnPosition = _playerComponent.transform.position + _playerComponent.transform.up * spawnOffset;
            var spawnRotation = _playerComponent.transform.rotation;
            var instance = Instantiate(bulletPrefab.gameObject, spawnPosition, spawnRotation);
            instance.transform.localScale = Vector3.one * bulletSize;
            var bulletComponent = instance.GetComponent<BulletComponent>();
            bulletComponent.TargetRigidbody2D.AddForce(bulletComponent.transform.up * shootImpulse,
                ForceMode2D.Impulse);

            EventPool.OnBulletSpawned.Invoke(bulletComponent);
        }

        private void Reload()
        {
            StartCoroutine(ReloadCoroutine());

            IEnumerator ReloadCoroutine()
            {
                _isReady = false;
                yield return new WaitForSeconds(reloadTime);
                _isReady = true;
            }
        }
    }
}