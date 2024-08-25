using Source.Adapters;
using Source.Components;
using ServiceLocators;
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
			var enemySpawnSettingsComponent =
				Object.FindObjectOfType<EnemySpawnSettingsComponent>(); // todo make scriptable object
			var playerView = Object.FindObjectOfType<PlayerView>();
			var gameOverScreenComponent = Object.FindObjectOfType<GameOverScreenView>();
			var scoreComponent = Object.FindObjectOfType<ScoreView>();
			var livesComponent = Object.FindObjectOfType<LivesComponent>();
			var debugUiView = Object.FindObjectOfType<DebugUiView>();

			IBoundsProvider boundsProvider = new OrthographicCameraBoundsProvider(Camera.main);
			IPlayerInputProvider playerInputProvider =
				new InputSystemPlayerInputAdapter(enemySpawnSettingsComponent.inputActionAsset);

			IAsteroidFactorySettingsProvider asteroidFactorySettingsProvider =
				new EnemySettingsAsteroidFactorySettingsAdapter(enemySpawnSettingsComponent);
			var asteroidBehaviourService =
				new AsteroidBehaviourService(asteroidFactorySettingsProvider, boundsProvider);

			var ufoSettingsProvider = new EnemySettingsUfoSettingsProviderAdapter(enemySpawnSettingsComponent);
			var ufoBehaviourService = new UfoBehaviourService(ufoSettingsProvider, boundsProvider, playerView);

			var playerLifetimeService = new PlayerLifetimeService(playerView);
			PlayerModel playerModel = playerLifetimeService.Model;

			AddService(asteroidBehaviourService);
			AddService(ufoBehaviourService);
			AddService(new EnemyBoundsService(boundsProvider, asteroidBehaviourService, ufoBehaviourService));
			AddService(new GameLoopService(gameOverScreenComponent));
			AddService(new LivesService(livesComponent, playerView));
			AddService(
				new PlayerMovementService(playerModel, playerModel, playerView, playerModel, playerInputProvider));
			AddService(new PlayerBoundsService(playerModel, boundsProvider));
			AddService(new PlayerShootService(playerView, boundsProvider, playerInputProvider));
			AddService(new PlayerLaserService(playerModel, playerInputProvider, playerView, playerView, playerView));
			AddService(playerLifetimeService);
			AddService(new ScoreService(scoreComponent));
			AddService(new DebugUiService(debugUiView, playerModel));
		}
	}
}