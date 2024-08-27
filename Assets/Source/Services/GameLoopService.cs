using Source.Components;
using UnityEngine;

namespace ServiceLocators
{
	public class GameLoopService : Service, IUpdatable, IStartable
	{
		private readonly GameOverScreenView _gameOverScreenView;

		private const string ResultFormat =
			"<size=56><b>Game Over!</b></size>\nScore: {0:000000}\n\n" +
			"<size=36><i>press R to try again</i></size>";

		public GameLoopService(GameOverScreenView gameOverScreenView)
		{
			_gameOverScreenView = gameOverScreenView;
		}

		public void Start()
		{
			OnGameStarted();
		}

		public void OnGameOver()
		{
			var scoreService = ServiceLocator.GetService<ScoreService>();
			int score = scoreService.ScoreModel.Score;
			_gameOverScreenView.gameObject.SetActive(true);
			_gameOverScreenView.TextMeshPro.text = string.Format(ResultFormat, score);
		}

		public void Update()
		{
			if (_gameOverScreenView.gameObject.activeSelf && Input.GetKeyDown(KeyCode.R))
			{
				OnGameStarted();

				// демонстрация альтернативного подхода
				// не люблю такое за обфускацию usage в среде разработки 
				ServiceLocator.GetService<LivesService>().ResetLives();
				ServiceLocator.GetService<AsteroidBehaviourService>().OnGameRestart();
				ServiceLocator.GetService<UfoBehaviourService>().OnGameRestart();
				ServiceLocator.GetService<ScoreService>().OnGameRestart();
			}
		}

		private void OnGameStarted()
		{
			_gameOverScreenView.gameObject.SetActive(false);
		}
	}
}