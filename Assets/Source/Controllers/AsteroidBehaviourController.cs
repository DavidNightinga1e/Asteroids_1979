using System.Collections.Generic;
using Source.Components;
using Source.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Controllers
{
    public class AsteroidBehaviourController : MonoBehaviour
    {
        [SerializeField] private List<AsteroidComponent> asteroidPrefabs;

        public readonly List<float> AsteroidSizes = new List<float> {1, 1.5f, 2f};

        public float minForce = 50f;
        public float maxForce = 450f;
        public float minTorque = 10f;
        public float maxTorque = 360f;

        private void Awake()
        {
            EventPool.OnEnemyDestroyed.AddListener(OnEnemyDestroyed);
        }

        private void OnEnemyDestroyed(EnemyComponent arg0)
        {
            if (!(arg0 is AsteroidComponent asteroidComponent)) return;

            var parentSize = asteroidComponent.size;
            if (parentSize <= 0) return;

            var asteroidType = Random.Range(0, asteroidPrefabs.Count);
            var numberOfAsteroids = Random.Range(1, 4);
            for (int i = 0; i < numberOfAsteroids; i++)
            {
                var instance = Instantiate(
                    asteroidPrefabs[asteroidType].gameObject,
                    asteroidComponent.transform.position,
                    Quaternion.Euler(0, 0, Random.Range(0, 360)));

                var newAsteroid = instance.GetComponent<AsteroidComponent>();
                newAsteroid.TargetRigidbody2D.AddForce(
                    newAsteroid.transform.up * Random.Range(minForce, maxForce));
                newAsteroid.TargetRigidbody2D.AddTorque(Random.Range(minTorque, maxTorque));
                newAsteroid.size = Random.Range(0, parentSize);
                newAsteroid.transform.localScale = Vector3.one * AsteroidSizes[newAsteroid.size];
                EventPool.OnEnemySpawned.Invoke(newAsteroid);
            }
        }
    }
}