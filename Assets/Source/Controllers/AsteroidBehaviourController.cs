using System;
using System.Collections;
using Source.Components;
using Source.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Controllers
{
    public class AsteroidBehaviourController : MonoBehaviour
    {
        private EnemySpawnSettingsComponent _settings;

        private (float min, float max) WaitTimeRange =>
        (
            _settings.currentAsteroidSpawnCooldown - _settings.AsteroidSpawnDispersion,
            _settings.currentAsteroidSpawnCooldown + _settings.AsteroidSpawnDispersion);

        private void Awake()
        {
            this.AutoFindComponent(out _settings);
            EventPool.OnEnemyHit.AddListener(OnEnemyHit);
            StartCoroutine(EnemySpawnEnumerator());
        }

        IEnumerator EnemySpawnEnumerator()
        {
            while (_settings.spawn)
            {
                var (min, max) = WaitTimeRange;
                var waitTime = Random.Range(min, max);
                yield return new WaitForSeconds(waitTime);

                var (point, rotation) = RandomPointAndDirectionOnBounds();
                var asteroidPrefabs = _settings.asteroidPrefabs;
                var asteroidIndex = Random.Range(0, asteroidPrefabs.Count);
                var instance = Instantiate(
                    asteroidPrefabs[asteroidIndex].gameObject,
                    point,
                    Quaternion.Euler(0, 0, rotation));
                var asteroidComponent = instance.GetComponent<AsteroidComponent>();
                InitializeAsteroid(asteroidComponent, _settings.asteroidSizes.Count);
                EventPool.OnEnemySpawned.Invoke(asteroidComponent);
            }
        }

        private void InitializeAsteroid(AsteroidComponent asteroidComponent, int maxSize)
        {
            asteroidComponent.TargetRigidbody2D.AddForce(
                asteroidComponent.transform.up *
                Random.Range(_settings.asteroidMinForce, _settings.asteroidMaxForce));
            asteroidComponent.TargetRigidbody2D.AddTorque(Random.Range(_settings.asteroidMinTorque,
                _settings.asteroidMaxTorque));
            asteroidComponent.Size = Random.Range(0, maxSize);
            asteroidComponent.transform.localScale = Vector3.one * _settings.asteroidSizes[asteroidComponent.Size];
        }

        private (Vector2, float) RandomPointAndDirectionOnBounds()
        {
            var spawnExtents = _settings.enemySpawnExtents;

            float RandomOnX() => Random.Range(-spawnExtents.x, spawnExtents.x);
            float RandomOnY() => Random.Range(-spawnExtents.x, spawnExtents.x);

            var side = Random.Range(0, 4);
            var point = side switch
            {
                0 => new Vector2(-spawnExtents.x, RandomOnY()),
                1 => new Vector2(RandomOnX(), spawnExtents.y),
                2 => new Vector2(spawnExtents.x, RandomOnY()),
                3 => new Vector2(RandomOnX(), -spawnExtents.y),
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

        private void OnEnemyHit(EnemyComponent arg0)
        {
            if (!(arg0 is AsteroidComponent asteroidComponent)) 
                return;

            var parentSize = asteroidComponent.Size;
            if (parentSize > 0)
            {
                var numberOfAsteroids = Random.Range(1, 4);
                for (int i = 0; i < numberOfAsteroids; i++)
                {
                    var asteroidType = Random.Range(0, _settings.asteroidPrefabs.Count);
                    var instance = Instantiate(
                        _settings.asteroidPrefabs[asteroidType].gameObject,
                        asteroidComponent.transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)),
                        GetRandomRotation());

                    var newAsteroid = instance.GetComponent<AsteroidComponent>();
                    InitializeAsteroid(newAsteroid, parentSize);
                    EventPool.OnEnemySpawned.Invoke(newAsteroid);
                }
            }

            Destroy(asteroidComponent.gameObject);
        }

        private static Quaternion GetRandomRotation() => Quaternion.Euler(0, 0, Random.Range(0, 360));
    }
}