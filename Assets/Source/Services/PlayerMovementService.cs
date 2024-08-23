using Source.Components;
using Source.Interfaces;
using Source.Models;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ServiceLocators
{
	public class PlayerMovementService : Service, IFixedUpdatable, IUpdatable
	{
		private const float Acceleration = 0.5F;
		private const float AngularAcceleration = -2.5F;

		private readonly IMovable _playerMovable;
		private readonly IRotatable _playerRotatable;
		private readonly IPlayerDestroyBroadcaster _playerDestroyBroadcaster;
		private readonly PlayerModel _playerModel;
		private readonly IPlayerInputProvider _playerInputProvider;

		public PlayerMovementService(IMovable playerMovable, IRotatable rotatable,
			IPlayerDestroyBroadcaster playerDestroyBroadcaster, PlayerModel model,
			IPlayerInputProvider playerInputProvider)
		{
			_playerMovable = playerMovable;
			_playerRotatable = rotatable;
			_playerDestroyBroadcaster = playerDestroyBroadcaster;
			_playerModel = model;
			_playerInputProvider = playerInputProvider;

			_playerDestroyBroadcaster.OnPlayerDestroy += OnPlayerDestroyHandler;
		}

		private void OnPlayerDestroyHandler()
		{
			_playerModel.Speed = Vector2.zero;
			_playerModel.AngularSpeed = 0;

			_playerMovable.SetPosition(Vector2.zero);
			_playerRotatable.SetRotation(0);
		}

		public void FixedUpdate()
		{
			_playerRotatable.SetRotation(_playerRotatable.GetRotation() + _playerModel.AngularSpeed);
			_playerMovable.SetPosition(_playerMovable.GetPosition() + _playerModel.Speed);
		}

		public void Update()
		{
			float rotationInput = _playerInputProvider.Rotation;
			bool isThrottle = _playerInputProvider.IsMove;

			if (isThrottle)
				_playerModel.Speed += _playerRotatable.GetUp() * (Acceleration * Time.deltaTime);
			_playerModel.AngularSpeed += rotationInput * AngularAcceleration * Time.deltaTime;
		}
	}
}