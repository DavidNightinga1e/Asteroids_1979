using Source.Components;
using UnityEngine;

namespace ServiceLocators
{
	public class GameLoopService : Service, IUpdatable, IStartable
	{
		private readonly GameOverScreenView _gameOverScreenView;
		private readonly ScoreComponent _scoreComponent;

		public GameLoopService(GameOverScreenView gameOverScreenView, ScoreComponent scoreComponent)
		{
			_gameOverScreenView = gameOverScreenView;
			_scoreComponent = scoreComponent;
		}

		public void Start()
		{
			OnGameStarted();
		}

		public void OnGameOver()
		{
			_gameOverScreenView.gameObject.SetActive(true);
			_gameOverScreenView.TextMeshPro.text =
				$"<size=56><b>Game Over!</b></size>\nScore: {_scoreComponent.currentScore:000000}\n\n<size=36><i>press R to try again</i></size>";
		}

		public void Update()
		{
			if (_gameOverScreenView.gameObject.activeSelf && Input.GetKeyDown(KeyCode.R))
			{
				OnGameStarted();

				ServiceLocator.GetService<LivesService>().ResetLives();
				ServiceLocator.GetService<AsteroidBehaviourService>().OnGameRestart();
				ServiceLocator.GetService<ScoreService>().OnGameRestart();
			}
		}

		private void OnGameStarted()
		{
			_gameOverScreenView.gameObject.SetActive(false);
		}
	}
}