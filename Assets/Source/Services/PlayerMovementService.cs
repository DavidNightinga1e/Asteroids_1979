using Source.Components;
using Source.Interfaces;
using Source.Models;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ServiceLocators
{
	public class PlayerMovementService : Service, IFixedUpdatable
	{
		private const float Acceleration = 0.01f;
		private const float AngularAcceleration = -0.05f;

		private readonly IMovable _playerMovable;
		private readonly IRotatable _playerRotatable;
		private readonly IPlayerDestroyBroadcaster _playerDestroyBroadcaster;
		private readonly PlayerModel _playerModel;

		public PlayerMovementService(IMovable playerMovable, IRotatable rotatable,
			IPlayerDestroyBroadcaster playerDestroyBroadcaster, PlayerModel model)
		{
			_playerMovable = playerMovable;
			_playerRotatable = rotatable;
			_playerDestroyBroadcaster = playerDestroyBroadcaster;
			_playerModel = model;

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
			float rotationInput = Input.GetAxis("Horizontal");
			float throttleInput = Input.GetAxis("Vertical");

			_playerModel.Speed += _playerRotatable.GetUp() * (throttleInput * Acceleration);
			_playerModel.AngularSpeed += rotationInput * AngularAcceleration;

			_playerMovable.SetPosition(_playerMovable.GetPosition() + _playerModel.Speed);
			_playerRotatable.SetRotation(_playerRotatable.GetRotation() + _playerModel.AngularSpeed);
		}
	}
}