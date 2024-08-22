﻿using System;
using Source.Components;
using Source.Events;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Controllers
{
	public class GameOverService : IService, IAwakable, IUpdatable
	{
		private readonly GameOverScreenComponent _gameOverScreenComponent;
		private readonly ScoreComponent _scoreComponent;

		public GameOverService(GameOverScreenComponent gameOverScreenComponent, ScoreComponent scoreComponent)
		{
			_gameOverScreenComponent = gameOverScreenComponent;
			_scoreComponent = scoreComponent;
		}

		public void Awake()
		{
			EventPool.OnGameOver.AddListener(OnGameOver);
			EventPool.OnGameStarted.AddListener(OnGameStarted);
		}

		private void OnGameStarted()
		{
			_gameOverScreenComponent.gameObject.SetActive(false);

			// todo: one entry point to all enemies 
			foreach (EnemyComponent enemy in Object.FindObjectsOfType<EnemyComponent>())
				Object.Destroy(enemy.gameObject);
		}

		private void OnGameOver()
		{
			_gameOverScreenComponent.gameObject.SetActive(true);
			_gameOverScreenComponent.TextMeshPro.text =
				$"<size=56><b>Game Over!</b></size>\nScore: {_scoreComponent.currentScore:000000}\n\n<size=36><i>press R to try again</i></size>";
		}

		public void Update()
		{
			if (_gameOverScreenComponent.gameObject.activeSelf && Input.GetKeyDown(KeyCode.R)) // bruh moment
			{
				EventPool.OnGameStarted.Invoke();
			}
		}
	}
}