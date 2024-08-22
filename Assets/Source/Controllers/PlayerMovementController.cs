using Source.Components;
using Source.Events;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovementController : MonoBehaviour
{
	public float acceleration = 500f;
	public float angularAcceleration = 100f;

	private Vector2 _speed;
	private float _rotationalSpeed;
	private PlayerComponent _playerComponent;

	private void Awake()
	{
		this.AutoFindComponent(out _playerComponent);
		
		EventPool.OnPlayerDestroyed.AddListener(() =>
		{
			_speed = Vector2.zero;
			_rotationalSpeed = 0;
		});
	}

	private void FixedUpdate()
	{
		float rotationInput = Input.GetAxis("Horizontal");
		float throttleInput = Input.GetAxis("Vertical");
		
		_speed += StripZ(_playerComponent.TargetRigidbody2D.transform.up * (throttleInput * acceleration));
		_rotationalSpeed += rotationInput * angularAcceleration;

		_playerComponent.TargetRigidbody2D.MovePosition(_playerComponent.TargetRigidbody2D.position + _speed);
		_playerComponent.TargetRigidbody2D.MoveRotation(_playerComponent.TargetRigidbody2D.rotation + _rotationalSpeed);
	}

	private static Vector2 StripZ(Vector3 v) => new(v.x, v.y);
}