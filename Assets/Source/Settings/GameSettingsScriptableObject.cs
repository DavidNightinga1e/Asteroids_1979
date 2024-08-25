using System.Collections.Generic;
using UnityEngine;

namespace Source.Settings
{
	[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
	public class GameSettingsScriptableObject : ScriptableObject
	{
		[Header("Asteroids"), SerializeField] private List<GameObject> asteroidPrefabs;
		[SerializeField] private List<float> asteroidSizes;
		[SerializeField] private float minAsteroidRotationSpeed;
		[SerializeField] private float maxAsteroidRotationSpeed;
		[SerializeField] private float minAsteroidLinearSpeed;
		[SerializeField] private float maxAsteroidLinearSpeed;
		[SerializeField] private float minAsteroidSpawnDelay;
		[SerializeField] private float maxAsteroidSpawnDelay;
		[Header("Ufo"), SerializeField] private List<GameObject> ufoPrefabs;
		[SerializeField] private float ufoSpeed;
		[SerializeField] private float ufoSpawnDelay;

		public List<GameObject> AsteroidPrefabs => asteroidPrefabs;
		public List<float> AsteroidSizes => asteroidSizes;
		public float MinAsteroidRotationSpeed => minAsteroidRotationSpeed;
		public float MaxAsteroidRotationSpeed => maxAsteroidRotationSpeed;
		public float MinAsteroidLinearSpeed => minAsteroidLinearSpeed;
		public float MaxAsteroidLinearSpeed => maxAsteroidLinearSpeed;
		public float MinAsteroidSpawnDelay => minAsteroidSpawnDelay;
		public float MaxAsteroidSpawnDelay => maxAsteroidSpawnDelay;
		
		public List<GameObject> UfoPrefabs => ufoPrefabs;
		public float UfoSpeed => ufoSpeed;
		public float UfoSpawnDelay => ufoSpawnDelay;
	}
}