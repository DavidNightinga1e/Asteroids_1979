using System;
using Source.Interfaces;
using Source.Views;

namespace Source.Models
{
	public class EnemyModel : ICollisionBroadcaster, IDestroyBroadcaster
	{
		public event Action<ICollisionBroadcaster> OnCollided;
		public event Action<IDestroyBroadcaster> OnDestroyed;

		public EnemyView View { get; }

		public EnemyModel(EnemyView view)
		{
			View = view;
			
			view.OnDestroyed += OnDestroyedHandler;
			view.OnCollided += OnCollidedHandler;
		}

		private void OnCollidedHandler(ICollisionBroadcaster obj)
		{
			OnCollided?.Invoke(this);
		}

		private void OnDestroyedHandler(IDestroyBroadcaster obj)
		{
			OnDestroyed?.Invoke(this);
			obj.OnDestroyed -= OnDestroyedHandler;
		}
	}
}