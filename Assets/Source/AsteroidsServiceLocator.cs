using Source.Adapters;
using Source.Components;
using Source.Controllers;
using Source.Interfaces;
using UnityEngine;

namespace Source
{
	public class AsteroidsServiceLocator : ServiceLocator
	{
		public AsteroidsServiceLocator()
		{
			var enemySpawnSettingsComponent = Object.FindObjectOfType<EnemySpawnSettingsComponent>();
			var playerComponent = Object.FindObjectOfType<PlayerComponent>();
			var gameOverScreenComponent = Object.FindObjectOfType<GameOverScreenComponent>();
			var scoreComponent = Object.FindObjectOfType<ScoreComponent>();
			var livesComponent = Object.FindObjectOfType<LivesComponent>();

			IBoundsProvider boundsProvider = new OrthographicCameraBoundsProvider(Camera.main);

			AddService(new AsteroidBehaviourService(enemySpawnSettingsComponent, boundsProvider));
			AddService(new BulletBoundsService(boundsProvider));
			AddService(new BulletDestroyService());
			AddService(new EnemyBoundsService(boundsProvider));
			AddService(new FlyingPlateBehaviourService(enemySpawnSettingsComponent, playerComponent, boundsProvider));
			AddService(new GameOverService(gameOverScreenComponent, scoreComponent));
			AddService(new LivesService(livesComponent));
			AddService(new PlayerBoundsService(playerComponent, boundsProvider));
			AddService(new PlayerMovementService(playerComponent));
			AddService(new PlayerRespawnService(playerComponent));
			AddService(new PlayerShootService(playerComponent));
			AddService(new ScoreService(scoreComponent));
		}
	}
}