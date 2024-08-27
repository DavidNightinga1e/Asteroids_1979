using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Views
{
	public class PlayerInputView : MonoBehaviour
	{
		[SerializeField] private PlayerInput _playerInput;

		public InputActionAsset InputActionAsset => _playerInput.actions;

#if UNITY_EDITOR
		private void OnValidate()
		{
			_playerInput = GetComponent<PlayerInput>();
		}
#endif
	}
}