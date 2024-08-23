using UnityEngine;

namespace Source.Interfaces
{
	public interface IMovable
	{
		Vector2 GetPosition();
		void SetPosition(Vector2 position);
	}
}