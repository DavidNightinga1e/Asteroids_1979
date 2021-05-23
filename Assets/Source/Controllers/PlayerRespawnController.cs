using System;
using System.Collections;
using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class PlayerRespawnController : MonoBehaviour
    {
        public Vector2 spawnPosition;

        private PlayerComponent _playerComponent;

        private void Awake()
        {
            this.AutoFindComponent(out _playerComponent);

            EventPool.OnGameStarted.AddListener(OnGameStarted);
            EventPool.OnPlayerDestroyed.AddListener(OnPlayerDestroyed);
            EventPool.OnGameOver.AddListener(OnGameOver);
        }

        private void Start()
        {
            EventPool.OnGameStarted.Invoke();
        }

        private void OnGameOver()
        {
            _playerComponent.gameObject.SetActive(false);
        }

        private void OnPlayerDestroyed()
        {
            MakeInvincible();
            MoveToSpawn();
        }

        private void OnGameStarted()
        {
            MoveToSpawn();
        }

        private void MoveToSpawn()
        {
            _playerComponent.transform.position = spawnPosition;
            _playerComponent.TargetRigidbody2D.velocity = Vector2.zero;
            _playerComponent.transform.rotation = Quaternion.identity;
        }

        private void MakeInvincible()
        {
            StartCoroutine(InvincibilityCoroutine());

            IEnumerator InvincibilityCoroutine()
            {
                _playerComponent.PolygonCollider2D.enabled = false;

                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(0.3f);
                    _playerComponent.SpriteRenderer.enabled = false;
                    yield return new WaitForSeconds(0.3f);
                    _playerComponent.SpriteRenderer.enabled = true;
                }

                _playerComponent.PolygonCollider2D.enabled = true;
            }
        }
    }
}