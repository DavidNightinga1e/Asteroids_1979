using System;
using Source.Interfaces;
using UnityEngine;

namespace Source.Views
{
	public class RigidbodyActorView : MonoBehaviour, IMovable, IRotatable
	{
		[SerializeField] private Rigidbody2D _rb;

		public Rigidbody2D Rb => _rb;
		
		public Vector2 GetPosition()
		{
			return _rb.position;
		}

		public void SetPosition(Vector2 position)
		{
			_rb.MovePosition(position);
		}

		public float GetRotation()
		{
			return _rb.rotation;
		}

		public void SetRotation(float rotation)
		{
			_rb.MoveRotation(rotation);
		}

		public Vector2 GetUp()
		{
			return transform.up;
		}

		private void OnValidate()
		{
			_rb = GetComponent<Rigidbody2D>();
		}
	}
}