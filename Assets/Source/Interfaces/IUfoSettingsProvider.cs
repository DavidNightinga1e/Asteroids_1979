using System.Collections.Generic;
using UnityEngine;

namespace Source.Interfaces
{
	public interface IUfoSettingsProvider
	{
		IReadOnlyList<GameObject> Prefabs { get; }
		float Speed { get; }
		float SpawnDelay { get; }
	}
}