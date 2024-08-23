using Source.Adapters;
using Source.Components;
using Source.Controllers;
using Source.Interfaces;
using Source.Models;
using Source.Views;
using UnityEngine;

namespace Source
{
	public class AsteroidsServiceLocator : ServiceLocator
	{
		public AsteroidsServiceLocator()
		{
			var enemySpawnSettingsComponent = Object.FindObjectOfType<EnemySpawnSettingsComponent>();
			var playerView = Object.FindObjectOfType<PlayerView>();
			var gameOverScreenComponent = Object.FindObjectOfType<GameOverScreenView>();
			var scoreComponent = Object.FindObjectOfType<ScoreComponent>();
			var livesComponent = Object.FindObjectOfType<LivesComponent>();
			var debugUiView = Object.FindObjectOfType<DebugUiView>();

			IBoundsProvider boundsProvider = new OrthographicCameraBoundsProvider(Camera.main);
			IAsteroidFactorySettingsProvider asteroidFactorySettingsProvider =
				new EnemySettingsAsteroidFactorySettingsAdapter(enemySpawnSettingsComponent);

			var asteroidBehaviourService =
				new AsteroidBehaviourService(asteroidFactorySettingsProvider, boundsProvider);

			var playerLifetimeService = new PlayerLifetimeService(playerView);
			PlayerModel playerModel = playerLifetimeService.Model;

			AddService(asteroidBehaviourService);
			AddService(new EnemyBoundsService(boundsProvider, asteroidBehaviourService));
			//AddService(new FlyingPlateBehaviourService(enemySpawnSettingsComponent, playerComponent, boundsProvider));
			AddService(new GameLoopService(gameOverScreenComponent, scoreComponent));
			AddService(new LivesService(livesComponent, playerView));
			AddService(new PlayerMovementService(playerModel, playerModel, playerView, playerModel));
			AddService(new PlayerBoundsService(playerModel, boundsProvider));
			AddService(new PlayerShootService(playerView, boundsProvider));
			AddService(playerLifetimeService);
			AddService(new ScoreService(scoreComponent));
			AddService(new DebugUiService(debugUiView, playerModel));
		}
	}
}