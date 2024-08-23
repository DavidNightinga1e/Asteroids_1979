using Source.Models;

namespace ServiceLocators
{
	public class ScoreService : Service
	{
		private readonly ScoreComponent _scoreComponent;

		public ScoreService(ScoreComponent scoreComponent)
		{
			_scoreComponent = scoreComponent;
		}

		public void OnGameRestart()
		{
			_scoreComponent.currentScore = 0;
			UpdateText();
		}

		private void UpdateText()
		{
			_scoreComponent.TextMeshPro.text = $"Score: {_scoreComponent.currentScore:000000}";
		}

		public void OnEnemyHit(EnemyModel enemyModel)
		{
			_scoreComponent.currentScore += enemyModel switch
			{
				AsteroidModel asteroidModel => 400 - 100 * asteroidModel.Size,
				_ => 800
			};

			UpdateText();
		}
	}
}