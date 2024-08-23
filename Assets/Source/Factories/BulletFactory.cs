using Source.Interfaces;
using Source.Models;
using Source.Views;
using UnityEngine;

namespace Source.Factories
{
	public static class BulletFactory 
	{
		public static BulletModel CreateBullet(IPrefabProvider prefabProvider, PlayerView playerView, float bulletScale)
		{
			GameObject prefab = prefabProvider.Prefab;
			Vector2 rbPosition = playerView.Rb.position;
			Vector2 spawnOffset = playerView.Rb.transform.up * 1f;
			Quaternion rotation = Quaternion.AngleAxis(playerView.GetRotation(), Vector3.forward);
			GameObject instance = Object.Instantiate(prefab, rbPosition + spawnOffset, rotation);
			instance.transform.localScale = Vector3.one * bulletScale;
			var bulletView = instance.GetComponent<BulletView>();
			return new BulletModel(bulletView);
		}
	}
}