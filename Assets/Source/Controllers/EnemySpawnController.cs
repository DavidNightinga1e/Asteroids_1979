using System;
using System.Collections;
using System.Collections.Generic;
using Source.Components;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Controllers
{
    public class EnemySpawnController : MonoBehaviour
    {
        public Vector2 enemySpawnExtents;
        public List<EnemyComponent> enemyPrefabs;

        public float defaultSpawnCooldown = 1f;
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
                var enemyType = Random.Range(0, enemyPrefabs.Count);
                var instance = Instantiate(
                    enemyPrefabs[enemyType].gameObject,
                    point,
                    Quaternion.Euler(0, 0, rotation));
                var enemyComponent = instance.GetComponent<EnemyComponent>();
                enemyComponent.TargetRigidbody2D.AddForce(
                    enemyComponent.transform.up * Random.Range(minForce, maxForce));
                enemyComponent.TargetRigidbody2D.AddTorque(Random.Range(minTorque, maxTorque));
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