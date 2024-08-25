using System.Collections.Generic;
using Source.Components;
using Source.Interfaces;
using UnityEngine;

namespace Source.Adapters
{
	public class EnemySettingsUfoSettingsProviderAdapter : IUfoSettingsProvider
	{
		private readonly EnemySpawnSettingsComponent _enemySpawnSettings;

		public EnemySettingsUfoSettingsProviderAdapter(EnemySpawnSettingsComponent enemySpawnSettingsComponent)
		{
			_enemySpawnSettings = enemySpawnSettingsComponent;
		}

		public IReadOnlyList<GameObject> Prefabs => _enemySpawnSettings.ufoPrefabs;
		public float Speed => _enemySpawnSettings.flyingPlateSpeed;
		public float SpawnDelay => _enemySpawnSettings.currentFlyingPlateSpawnCooldown;
	}
}