using Source.Interfaces;
using UnityEngine.InputSystem;

namespace Source.Adapters
{
	public class InputSystemPlayerInputAdapter : IPlayerInputProvider
	{
		public float Rotation => _rotateAction.ReadValue<float>();
		public bool IsMove => _moveAction.IsPressed();
		public bool IsFire => _fireAction.IsPressed();
		public bool IsLaserFire => _fireLaserAction.WasPerformedThisFrame();

		private readonly InputAction _fireAction;
		private readonly InputAction _moveAction;
		private readonly InputAction _rotateAction;
		private readonly InputAction _fireLaserAction;

		public InputSystemPlayerInputAdapter(InputActionAsset inputActionAsset)
		{
			InputActionMap inputActionMap = inputActionAsset.FindActionMap("Player");
			_fireAction = inputActionMap.FindAction("Fire");
			_moveAction = inputActionMap.FindAction("Move");
			_rotateAction = inputActionMap.FindAction("Rotate");
			_fireLaserAction = inputActionMap.FindAction("FireLaser");
		}
	}
}