using Source.Interfaces;
using Source.Views;
using UnityEngine;

namespace Source.Adapters
{
	public class PlayerBulletPrefabProviderAdapter : IPrefabProvider
	{
		public GameObject Prefab { get; }

		public PlayerBulletPrefabProviderAdapter(PlayerView playerView)
		{
			Prefab = playerView.BulletPrefab;
		}
	}
}