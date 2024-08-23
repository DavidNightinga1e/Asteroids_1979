using System;
using Source.Interfaces;
using UnityEngine;

namespace Source.Views
{
	public class PlayerView : RigidbodyActorView, IPlayerDestroyBroadcaster
	{
		public event Action OnPlayerDestroy;

		// todo: do not serialize refs like that, move to addressables or resources 
		[SerializeField] private GameObject bulletPrefab;

		public GameObject BulletPrefab => bulletPrefab;

		private void OnTriggerEnter2D(Collider2D col)
		{
			OnPlayerDestroy?.Invoke();
		}
	}
}