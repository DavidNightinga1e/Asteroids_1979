using Source.Interfaces;
using Source.Views;
using UnityEngine;

namespace Source.Models
{
	public class PlayerModel : IMovable, IRotatable
	{
		private float _cachedRotation;
		private Vector2 _cachedPosition;
		
		public Vector2 Speed { get; set; }
		public float AngularSpeed { get; set; }
		
		public PlayerView View { get; }
		
		public PlayerModel(PlayerView view)
		{
			View = view;
		}
		
		public Vector2 GetPosition() => _cachedPosition;

		public void SetPosition(Vector2 position) => _cachedPosition = position;

		public float GetRotation() => _cachedRotation;

		public void SetRotation(float rotation) => _cachedRotation = rotation;

		public Vector2 GetUp() => View.GetUp();

		public void ApplyModelToView()
		{
			View.SetPosition(GetPosition());
			View.SetRotation(GetRotation());
		}
	}
}