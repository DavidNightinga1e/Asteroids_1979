using Source.Controllers;

namespace Source
{
	public class AsteroidsServiceLocator : ServiceLocator
	{
		public AsteroidsServiceLocator()
		{
			AddService(new AsteroidBehaviourService());
			AddService(new BulletBoundsService());
			AddService(new BulletDestroyService());
			AddService(new EnemyBoundsService());
			AddService(new FlyingPlateBehaviourService());
			AddService(new GameOverService());
			AddService(new LivesService());
			AddService(new PlayerBoundsService());
			AddService(new PlayerMovementService());
			AddService(new PlayerRespawnService());
			AddService(new PlayerShootService());
			AddService(new ScoreService());
		}
	}
}