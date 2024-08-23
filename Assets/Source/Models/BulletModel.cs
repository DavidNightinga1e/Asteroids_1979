using System;
using Source.Interfaces;
using Source.Views;

namespace Source.Models
{
	public class BulletModel : ICollisionBroadcaster, IDestroyBroadcaster
	{
		public event Action<ICollisionBroadcaster> OnCollided;
		public event Action<IDestroyBroadcaster> OnDestroyed;

		public BulletView View { get; }

		public BulletModel(BulletView view)
		{
			View = view;
			view.OnCollided += OnCollidedHandler;
			view.OnDestroyed += OnDestroyedHandler;
		}

		private void OnDestroyedHandler(IDestroyBroadcaster obj)
		{
			OnDestroyed?.Invoke(this);
			obj.OnDestroyed -= OnDestroyedHandler;
		}

		private void OnCollidedHandler(ICollisionBroadcaster obj)
		{
			OnCollided?.Invoke(this);
		}
	}
}