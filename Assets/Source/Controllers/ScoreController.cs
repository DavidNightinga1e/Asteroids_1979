using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class ScoreController : IController, IAwakable
    {
        private ScoreComponent _scoreComponent;

        public void Awake()
        {
            _scoreComponent = Object.FindObjectOfType<ScoreComponent>();

            EventPool.OnEnemyHit.AddListener(OnEnemyHit);
            EventPool.OnGameStarted.AddListener(OnGameStarted);
        }

        private void OnGameStarted()
        {
            _scoreComponent.currentScore = 0;
            UpdateText();
        }

        private void UpdateText()
        {
            _scoreComponent.TextMeshPro.text = $"Score: {_scoreComponent.currentScore:000000}";
        }

        private void OnEnemyHit(EnemyComponent enemyComponent)
        {
            switch (enemyComponent)
            {
                case AsteroidComponent asteroidComponent:
                    _scoreComponent.currentScore += 400 - 100 * asteroidComponent.Size;
                    break;
                case FlyingPlateComponent _:
                    _scoreComponent.currentScore += 800;
                    break;
            }

            UpdateText();
        }
    }
}