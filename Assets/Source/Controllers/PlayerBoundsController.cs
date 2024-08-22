using Source.Components;
using Source.Utilities;
using UnityEngine;

namespace Source.Controllers
{
	public class PlayerBoundsController : IController, IUpdatable, IAwakable
	{
		private PlayerComponent _playerComponent;
		private Camera _camera;

		public void Awake()
		{
			_playerComponent = Object.FindObjectOfType<PlayerComponent>();
			_camera = Camera.main;
		}

		public void Update()
		{
			Vector2 extents = _camera.GetOrthographicExtents();

			Rigidbody2D targetRb = _playerComponent.TargetRigidbody2D;
			Vector2 p = targetRb.position;
			if (p.x > extents.x)
				targetRb.position = new Vector2(-extents.x, p.y);
			else if (p.x < -extents.x)
				targetRb.position = new Vector2(extents.x, p.y);
			else if (p.y > extents.y)
				targetRb.position = new Vector2(p.x, -extents.y);
			else if (p.y < -extents.y)
				targetRb.position = new Vector2(p.x, extents.y);
		}
	}
}