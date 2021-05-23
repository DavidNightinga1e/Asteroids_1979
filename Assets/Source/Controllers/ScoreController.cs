using System;
using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class ScoreController : MonoBehaviour
    {
        private ScoreComponent _scoreComponent;

        private int _currentScore;
        
        private void Awake()
        {
            this.AutoFindComponent(out _scoreComponent);
            
            EventPool.OnEnemyDestroyed.AddListener(OnEnemyHit);
            EventPool.OnGameStarted.AddListener(OnGameStarted);
        }

        private void OnGameStarted()
        {
            _currentScore = 0;
        }

        private void OnEnemyHit(EnemyComponent enemyComponent)
        {
            _currentScore += 200; // todo add dependencies 
            _scoreComponent.TextMeshPro.text = $"Score: {_currentScore:000000}";
        }
    }
}