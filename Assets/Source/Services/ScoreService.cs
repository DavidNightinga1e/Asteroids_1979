using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class ScoreService : IService, IAwakable
    {
        private readonly ScoreComponent _scoreComponent;

        public ScoreService(ScoreComponent scoreComponent)
        {
            _scoreComponent = scoreComponent;
        }

        public void Awake()
        {
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