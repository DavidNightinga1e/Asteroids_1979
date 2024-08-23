using UnityEngine;

namespace Source.Interfaces
{
	public interface IRotatable
	{
		float GetRotation();
		void SetRotation(float rotation);
		Vector2 GetUp();
	}
}