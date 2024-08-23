using Source.Interfaces;
using UnityEngine.InputSystem;

namespace Source.Adapters
{
	public class InputSystemPlayerInputAdapter : IPlayerInputProvider
	{
		public float Rotation { get; }
		public bool IsMove { get; }
		public bool IsFire => _fireAction.IsPressed();

		private readonly InputAction _fireAction;

		public InputSystemPlayerInputAdapter(InputActionAsset inputActionAsset)
		{
			InputActionMap inputActionMap = inputActionAsset.FindActionMap("Player");
			_fireAction = inputActionMap.FindAction("Fire");
		}
	}
}