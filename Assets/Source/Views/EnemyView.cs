using System;
using Source.Interfaces;
using UnityEngine;

namespace Source.Views
{
	public class EnemyView : RigidbodyActorView, ICollisionBroadcaster, IDestroyBroadcaster
	{
		public event Action<ICollisionBroadcaster> OnCollided;
		public event Action<IDestroyBroadcaster> OnDestroyed;
		
		private void OnTriggerEnter2D(Collider2D c)
		{
			if (c.gameObject.layer == LayerMask.NameToLayer("Bullet"))
				OnCollided?.Invoke(this);
		}

		private void OnDestroy()
		{
			OnDestroyed?.Invoke(this);
		}
	}
}