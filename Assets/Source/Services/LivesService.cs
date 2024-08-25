using Source.Interfaces;
using Source.Views;

namespace ServiceLocators
{
	public class LivesService : Service
	{
		private const int MaxLives = 3;

		private readonly LivesView _livesView;
		private int _currentLives;

		public LivesService(LivesView livesView,
			IPlayerDestroyBroadcaster playerDestroyBroadcaster)
		{
			_livesView = livesView;

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
			_livesView.Lives[2].enabled = _currentLives > 2;
			_livesView.Lives[1].enabled = _currentLives > 1;
			_livesView.Lives[0].enabled = _currentLives > 0;
		}
	}
}