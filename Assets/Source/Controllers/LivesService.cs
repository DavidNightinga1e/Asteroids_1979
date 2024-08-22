using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class LivesService : IService, IAwakable
    {
        private const int MaxLives = 3;

        private LivesComponent _livesComponent;
        private int _currentLives;

        public void Awake()
        {
            _livesComponent = Object.FindObjectOfType<LivesComponent>();
            
            EventPool.OnPlayerDestroyed.AddListener(OnPlayerDestroyed);
            EventPool.OnGameStarted.AddListener(OnGameStarted);
        }

        private void OnGameStarted()
        {
            _currentLives = MaxLives;
            
            UpdateLivesDisplay();
        }

        private void OnPlayerDestroyed()
        {
            _currentLives--;
            if (_currentLives < 1)
                EventPool.OnGameOver.Invoke();
            
            UpdateLivesDisplay();
        }

        private void UpdateLivesDisplay()
        {
            _livesComponent.Lives[2].enabled = _currentLives > 2;
            _livesComponent.Lives[1].enabled = _currentLives > 1;
            _livesComponent.Lives[0].enabled = _currentLives > 0;
        }
    }
}