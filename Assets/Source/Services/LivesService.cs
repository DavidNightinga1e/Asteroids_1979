using System;
using Source.Interfaces;

namespace ServiceLocators
{
	public class LivesService : Service
	{
		private const int MaxLives = 3;

		private readonly LivesComponent _livesComponent;
		private int _currentLives;

		public LivesService(LivesComponent livesComponent,
			IPlayerDestroyBroadcaster playerDestroyBroadcaster)
		{
			_livesComponent = livesComponent;

			playerDestroyBroadcaster.OnPlayerDestroy += OnPlayerDestroyed;
		}

		public void ResetLives()
		{
			_currentLives = MaxLives;
			UpdateLivesDisplay();
		}

		private void OnPlayerDestroyed()
		{
			_currentLives--;
			if (_currentLives < 1)
			{
				ServiceLocator.GetService<GameLoopService>().OnGameOver();
			}

			UpdateLivesDisplay();
		}

		private void UpdateLivesDisplay()
		{
			_livesComponent.Lives[2].enabled = _currentLives > 2;
			_livesComponent.Lives[1].enabled = _currentLives > 1;
			_livesComponent.Lives[0].enabled = _currentLives > 0;
		}
	}
}