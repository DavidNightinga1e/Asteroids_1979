using Source.Models;
using Source.Views;

namespace ServiceLocators
{
	public class ScoreService : Service
	{
		private readonly ScoreView _scoreView;

		public ScoreModel ScoreModel { get; set; } = new();

		public ScoreService(ScoreView scoreView)
		{
			_scoreView = scoreView;
		}

		public void OnGameRestart()
		{
			ScoreModel.Score = 0;
			UpdateText();
		}

		private void UpdateText()
		{
			_scoreView.TextMeshPro.text = $"Score: {ScoreModel.Score:000000}";
		}

		public void OnEnemyHit(EnemyModel enemyModel)
		{
			ScoreModel.Score += enemyModel switch
			{
				AsteroidModel asteroidModel => 400 - 100 * asteroidModel.Size,
				UfoModel _ => 300,
				_ => 800
			};

			UpdateText();
		}
	}
}