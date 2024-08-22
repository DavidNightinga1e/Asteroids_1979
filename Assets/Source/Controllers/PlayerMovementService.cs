using Source.Components;
using Source.Events;
using UnityEngine;

namespace Source.Controllers
{
	public class PlayerMovementService : IService, IAwakable, IFixedUpdatable
	{
		private const float Acceleration = 0.01f;
		private const float AngularAcceleration = -0.05f;

		private Vector2 _speed;
		private float _rotationalSpeed;
		private PlayerComponent _playerComponent;

		public void Awake()
		{
			_playerComponent = Object.FindObjectOfType<PlayerComponent>();

			EventPool.OnPlayerDestroyed.AddListener(() =>
			{
				_speed = Vector2.zero;
				_rotationalSpeed = 0;
			});
		}

		public void FixedUpdate()
		{
			float rotationInput = Input.GetAxis("Horizontal");
			float throttleInput = Input.GetAxis("Vertical");

			_speed += StripZ(_playerComponent.TargetRigidbody2D.transform.up * (throttleInput * Acceleration));
			_rotationalSpeed += rotationInput * AngularAcceleration;

			_playerComponent.TargetRigidbody2D.MovePosition(_playerComponent.TargetRigidbody2D.position + _speed);
			_playerComponent.TargetRigidbody2D.MoveRotation(_playerComponent.TargetRigidbody2D.rotation +
			                                                _rotationalSpeed);
		}

		private static Vector2 StripZ(Vector3 v) => new(v.x, v.y);
	}
}