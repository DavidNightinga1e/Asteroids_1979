using System;
using Source.Interfaces;
using Source.Models;
using Source.Views;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Source.Factories
{
	public static class UfoFactory
	{
		public static UfoModel CreateUfo(IUfoSettingsProvider settingsProvider, IBoundsProvider boundsProvider)
		{
			int prefabsCount = settingsProvider.Prefabs.Count;
			int i = Random.Range(0, prefabsCount);

			Vector2 position = RandomPointOnBounds(boundsProvider);
			Quaternion rotation = Quaternion.identity;
			GameObject prefab = settingsProvider.Prefabs[i];

			GameObject instance = Object.Instantiate(prefab, position, rotation);
			var ufoView = instance.GetComponent<UfoView>();

			return new UfoModel(ufoView);
		}

		private static Vector2 RandomPointOnBounds(IBoundsProvider boundsProvider)
		{
			Vector2 spawnExtents = boundsProvider.GetBounds();
			
			float RandomOnX() => Random.Range(-spawnExtents.x, spawnExtents.x);
			float RandomOnY() => Random.Range(-spawnExtents.y, spawnExtents.y);
			
			int side = Random.Range(0, 4);
			return side switch
			{
				0 => new Vector2(-spawnExtents.x, RandomOnY()),
				1 => new Vector2(RandomOnX(), spawnExtents.y),
				2 => new Vector2(spawnExtents.x, RandomOnY()),
				3 => new Vector2(RandomOnX(), -spawnExtents.y),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}
}