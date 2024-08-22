using Source.Controllers;

namespace Source
{
	public class AsteroidsControllerLocator : ControllerLocator
	{
		public AsteroidsControllerLocator()
		{
			AddController(new AsteroidBehaviourController());
			AddController(new BulletBoundsController());
			AddController(new BulletDestroyController());
			AddController(new EnemyBoundsController());
			AddController(new FlyingPlateBehaviourController());
			AddController(new GameOverController());
			AddController(new LivesController());
			AddController(new PlayerBoundsController());
			AddController(new PlayerMovementController());
			AddController(new PlayerRespawnController());
			AddController(new PlayerShootController());
			AddController(new ScoreController());
		}
	}
}