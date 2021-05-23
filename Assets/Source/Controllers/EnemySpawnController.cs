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
        public Bounds enemySpawnBounds;
        public List<AsteroidComponent> asteroidsPrefabs;
        public List<FlyingPlateComponent> flyingPlatesPrefabs;

        public float defaultSpawnCooldown = 1f;
        public float currentSpawnCooldown = 1f;

        public float SpawnDispersion => currentSpawnCooldown * 0.25f;

        private bool _spawn = true;

        private void Awake()
        {
        }

        IEnumerator EnemySpawnEnumerator()
        {
            while (_spawn)
            {
                var waitTime = Random.Range(
                    currentSpawnCooldown - SpawnDispersion,
                    currentSpawnCooldown + SpawnDispersion);
                yield return new WaitForSeconds(waitTime);
                
            }
        }
    }
}