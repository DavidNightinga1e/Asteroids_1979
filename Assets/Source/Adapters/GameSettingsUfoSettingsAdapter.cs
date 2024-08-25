using System.Collections.Generic;
using Source.Interfaces;
using Source.Settings;
using UnityEngine;

namespace Source.Adapters
{
	public class GameSettingsUfoSettingsAdapter : IUfoSettingsProvider
	{
		private readonly GameSettingsScriptableObject _scriptableObject;
		
		public GameSettingsUfoSettingsAdapter(GameSettingsScriptableObject scriptableObject)
		{
			_scriptableObject = scriptableObject;
		}

		public IReadOnlyList<GameObject> Prefabs => _scriptableObject.UfoPrefabs;
		public float Speed => _scriptableObject.UfoSpeed;
		public float SpawnDelay => _scriptableObject.UfoSpawnDelay;
	}
}