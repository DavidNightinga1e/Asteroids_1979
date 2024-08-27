using Source.Adapters;
using Source.Components;
using ServiceLocators;
using Source.Interfaces;
using Source.Models;
using Source.Settings;
using Source.Views;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source
{
	public class AsteroidsServiceLocator : ServiceLocator
	{
		public AsteroidsServiceLocator()
		{
			// В идеальном мире использовать Addressables и асинхронщину, но так меньше кода писать
			var gameSettingsScriptableObject = Resources.Load<GameSettingsScriptableObject>("GameSettings");
			
			// Эти штуки так или иначе надо пробрасывать сюда, 
			// можно навернуть DI контейнер для монобехов, но он в конечном счете будет синглтоном или количество
			// findObject не изменится, поменяется ток что мы ищем 
			// можно сделать один монобех и сериализовать ссылки, тогда искать только один этот монобех, но выглядит
			// как сомнительное решение
			var playerView = Object.FindObjectOfType<PlayerView>();
			var gameOverScreenComponent = Object.FindObjectOfType<GameOverScreenView>();
			var scoreComponent = Object.FindObjectOfType<ScoreView>();
			var livesComponent = Object.FindObjectOfType<LivesView>();
			var debugUiView = Object.FindObjectOfType<DebugUiView>();
			var playerInputView = Object.FindObjectOfType<PlayerInputView>();

			IBoundsProvider boundsProvider = new OrthographicCameraBoundsProvider(Camera.main);
			
			InputActionAsset inputActionAsset = playerInputView.InputActionAsset;
			IPlayerInputProvider playerInputProvider = new InputSystemPlayerInputAdapter(inputActionAsset);

			IAsteroidFactorySettingsProvider asteroidFactorySettingsProvider =
				new GameSettingsAsteroidSettingsAdapter(gameSettingsScriptableObject);
			var asteroidBehaviourService =
				new AsteroidBehaviourService(asteroidFactorySettingsProvider, boundsProvider);

			IUfoSettingsProvider ufoSettingsProvider = new GameSettingsUfoSettingsAdapter(gameSettingsScriptableObject);
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