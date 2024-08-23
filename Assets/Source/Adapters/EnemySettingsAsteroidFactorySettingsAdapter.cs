using System.Collections.Generic;
using Source.Components;
using Source.Interfaces;
using UnityEngine;

namespace Source.Adapters
{
	public class EnemySettingsAsteroidFactorySettingsAdapter : IAsteroidFactorySettingsProvider
	{
		private readonly EnemySpawnSettingsComponent _spawnSettings;
		
		public EnemySettingsAsteroidFactorySettingsAdapter(EnemySpawnSettingsComponent spawnSettingsComponent)
		{
			_spawnSettings = spawnSettingsComponent;
		}

		public IReadOnlyList<GameObject> Prefabs => _spawnSettings.asteroidPrefabs;
		public IReadOnlyList<float> Sizes => _spawnSettings.asteroidSizes;
		public float MinRotationSpeed => _spawnSettings.asteroidMinTorque;
		public float MaxRotationSpeed => _spawnSettings.asteroidMaxTorque;
		public float MinLinearSpeed => _spawnSettings.asteroidMinForce;
		public float MaxLinearSpeed => _spawnSettings.asteroidMaxForce;
		public float SpawnMinDelay => _spawnSettings.currentAsteroidSpawnCooldown;
		public float SpawnMaxDelay => _spawnSettings.currentAsteroidSpawnCooldown;
	}
}