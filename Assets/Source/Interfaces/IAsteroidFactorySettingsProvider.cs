using System.Collections.Generic;
using UnityEngine;

namespace Source.Interfaces
{
	public interface IAsteroidFactorySettingsProvider
	{
		IReadOnlyList<GameObject> Prefabs { get; }
		IReadOnlyList<float> Sizes { get; }
		float MinRotationSpeed { get; }
		float MaxRotationSpeed { get; }
		float MinLinearSpeed { get; }
		float MaxLinearSpeed { get; }
		float SpawnMinDelay { get; }
		float SpawnMaxDelay { get; }
	}
}