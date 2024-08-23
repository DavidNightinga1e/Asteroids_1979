using Source.Models;
using Source.Views;
using UnityEditor.SceneManagement;

namespace ServiceLocators
{
	public class PlayerLifetimeService : Service, IFixedUpdatable
	{
		public PlayerModel Model { get; }
		
		public PlayerLifetimeService(PlayerView view)
		{
			Model = new PlayerModel(view);
		}

		public void FixedUpdate()
		{
			Model.ApplyModelToView();
		}
	}
}