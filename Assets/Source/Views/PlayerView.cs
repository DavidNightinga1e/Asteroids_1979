using System;
using Source.Interfaces;
using UnityEngine;

namespace Source.Views
{
	public class PlayerView : RigidbodyActorView, IPlayerDestroyBroadcaster, ILaserView
	{
		public event Action OnPlayerDestroy;

		// todo: do not serialize refs like that, move to addressables or resources 
		[SerializeField] private GameObject bulletPrefab;

		[SerializeField] private LineRenderer laserRenderer;

		public GameObject BulletPrefab => bulletPrefab;

		private void OnTriggerEnter2D(Collider2D col)
		{
			OnPlayerDestroy?.Invoke();
		}

		public void SetVisible(bool value)
		{
			laserRenderer.enabled = value;
		}
	}
}