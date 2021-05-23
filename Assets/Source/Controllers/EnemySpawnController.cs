using System;
using System.Collections;
using System.Collections.Generic;
using Source.Components;
using Source.Events;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Source.Controllers
{
    public class EnemySpawnController : MonoBehaviour
    {
        public Vector2 enemySpawnExtents;
        public List<AsteroidComponent> asteroidPrefabs;

        public readonly List<float> AsteroidSizes = new List<float> {1, 1.5f, 2f};

        public float currentSpawnCooldown = 1f;
        public float minForce = 50f;
        public float maxForce = 450f;
        public float minTorque = 10f;
        public float maxTorque = 360f;

        public float SpawnDispersion => currentSpawnCooldown * 0.25f;

        private bool _spawn = true;

        private void Awake()
        {
            StartCoroutine(EnemySpawnEnumerator());
        }

        IEnumerator EnemySpawnEnumerator()
        {
            while (_spawn)
            {
                var waitTime = Random.Range(
                    currentSpawnCooldown - SpawnDispersion,
                    currentSpawnCooldown + SpawnDispersion);
                yield return new WaitForSeconds(waitTime);
                var (point, rotation) = RandomPointAndDirectionOnBounds();
                var enemyType = Random.Range(0, asteroidPrefabs.Count);
                var instance = Instantiate(
                    asteroidPrefabs[enemyType].gameObject,
                    point,
                    Quaternion.Euler(0, 0, rotation));
                var asteroidComponent = instance.GetComponent<AsteroidComponent>();
                asteroidComponent.TargetRigidbody2D.AddForce(
                    asteroidComponent.transform.up * Random.Range(minForce, maxForce));
                asteroidComponent.TargetRigidbody2D.AddTorque(Random.Range(minTorque, maxTorque));
                asteroidComponent.size = Random.Range(0, AsteroidSizes.Count);
                asteroidComponent.transform.localScale = Vector3.one * AsteroidSizes[asteroidComponent.size];
                EventPool.OnEnemySpawned.Invoke(asteroidComponent);
            }
        }

        private (Vector2, float) RandomPointAndDirectionOnBounds()
        {
            float RandomOnX() => Random.Range(-enemySpawnExtents.x, enemySpawnExtents.x);
            float RandomOnY() => Random.Range(-enemySpawnExtents.x, enemySpawnExtents.x);

            var side = Random.Range(0, 4);
            var point = side switch
            {
                0 => new Vector2(-enemySpawnExtents.x, RandomOnY()),
                1 => new Vector2(RandomOnX(), enemySpawnExtents.y),
                2 => new Vector2(enemySpawnExtents.x, RandomOnY()),
                3 => new Vector2(RandomOnX(), -enemySpawnExtents.y),
                _ => throw new Exception()
            };
            var direction = Random.Range(-45, 45) + side switch
            {
                0 => -90,
                1 => -180,
                2 => -270,
                3 => 0,
                _ => throw new Exception()
            };

            return (point, direction);
        }
    }
}