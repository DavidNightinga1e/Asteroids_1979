using Source.Views;
using UnityEngine;

namespace Source.Models
{
	public class AsteroidModel : EnemyModel
	{
		public int Size { get; }
		public Vector2 Direction { get; }
		public float AngularSpeed { get; }
		public AsteroidView AsteroidView => (AsteroidView)View;

		public AsteroidModel(AsteroidView view, int size, Vector2 direction, float angularSpeed) : base(view)
		{
			Size = size;
			Direction = direction;
			AngularSpeed = angularSpeed;
		}
	}
}