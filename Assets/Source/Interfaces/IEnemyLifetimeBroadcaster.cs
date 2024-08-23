using System;
using Source.Models;

namespace Source.Interfaces
{
	public interface IEnemyLifetimeBroadcaster
	{
		event Action<EnemyModel> OnEnemySpawn;
		event Action<EnemyModel> OnEnemyDestroy;
	}
}