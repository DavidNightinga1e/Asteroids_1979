using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Components
{
    public class EnemySpawnSettingsComponent : MonoBehaviour
    {
        public List<GameObject> asteroidPrefabs;
        public List<GameObject> ufoPrefabs;
        public List<float> asteroidSizes = new List<float> {1, 1.5f, 2f};
        public float asteroidMinForce = 50f;
        public float asteroidMaxForce = 450f;
        public float asteroidMinTorque = 10f;
        public float asteroidMaxTorque = 360f;
        public float flyingPlateSpeed = 1f;
        public float currentAsteroidSpawnCooldown = 1f;
        public float AsteroidSpawnDispersion => currentAsteroidSpawnCooldown * 0.25f;
        public float currentFlyingPlateSpawnCooldown = 1f;
        public float FlyingPlateSpawnDispersion => currentAsteroidSpawnCooldown * 0.25f;
        public bool spawn = true;
    }
}