using System;
using Source.Interfaces;
using UnityEngine;

namespace Source.Views
{
	public class BulletView : RigidbodyActorView, IDestroyBroadcaster, ICollisionBroadcaster
	{
		public event Action<IDestroyBroadcaster> OnDestroyed;
		public event Action<ICollisionBroadcaster> OnCollided;

		private void OnDestroy()
		{
			OnDestroyed?.Invoke(this);
		}
		
		private void OnTriggerEnter2D(Collider2D _)
		{
			OnCollided?.Invoke(this);
		}
	}
}