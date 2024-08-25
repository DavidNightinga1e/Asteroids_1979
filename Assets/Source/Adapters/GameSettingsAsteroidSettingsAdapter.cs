using System.Collections.Generic;
using Source.Interfaces;
using Source.Settings;
using UnityEngine;

namespace Source.Adapters
{
	public class GameSettingsAsteroidSettingsAdapter : IAsteroidFactorySettingsProvider
	{
		private readonly GameSettingsScriptableObject _scriptableObject;

		public GameSettingsAsteroidSettingsAdapter(GameSettingsScriptableObject gameSettingsScriptableObject)
		{
			_scriptableObject = gameSettingsScriptableObject;
		}

		public IReadOnlyList<GameObject> Prefabs => _scriptableObject.AsteroidPrefabs;
		public IReadOnlyList<float> Sizes => _scriptableObject.AsteroidSizes;
		public float MinRotationSpeed => _scriptableObject.MinAsteroidRotationSpeed;
		public float MaxRotationSpeed => _scriptableObject.MaxAsteroidRotationSpeed;
		public float MinLinearSpeed => _scriptableObject.MinAsteroidLinearSpeed;
		public float MaxLinearSpeed => _scriptableObject.MaxAsteroidLinearSpeed;
		public float SpawnMinDelay => _scriptableObject.MinAsteroidSpawnDelay;
		public float SpawnMaxDelay => _scriptableObject.MaxAsteroidSpawnDelay;
	}
}