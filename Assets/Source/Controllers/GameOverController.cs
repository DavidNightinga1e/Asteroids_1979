using System;
using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
    public class GameOverController : MonoBehaviour
    {
        private GameOverScreenComponent _gameOverScreenComponent;
        private ScoreComponent _scoreComponent;

        private void Awake()
        {
            this.AutoFindComponent(out _gameOverScreenComponent);
            this.AutoFindComponent(out _scoreComponent);

            EventPool.OnGameOver.AddListener(OnGameOver);
            EventPool.OnGameStarted.AddListener(OnGameStarted);
        }

        private void OnGameStarted()
        {
            _gameOverScreenComponent.gameObject.SetActive(false);
            foreach (var enemy in FindObjectsOfType<EnemyComponent>()) 
                Destroy(enemy.gameObject);
        }

        private void OnGameOver()
        {
            _gameOverScreenComponent.gameObject.SetActive(true);
            _gameOverScreenComponent.TextMeshPro.text =
                $"<size=56><b>Game Over!</b></size>\nScore: {_scoreComponent.currentScore:000000}\n\n<size=36><i>press R to try again</i></size>";
        }

        private void Update()
        {
            if (_gameOverScreenComponent.gameObject.activeSelf && Input.GetKeyDown(KeyCode.R)) // bruh moment
            {
                EventPool.OnGameStarted.Invoke();
            }
        }
    }
}