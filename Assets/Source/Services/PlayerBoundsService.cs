using Source.Components;
using Source.Interfaces;
using Source.Utilities;
using UnityEngine;

namespace Source.Controllers
{
	public class PlayerBoundsService : IService, IUpdatable
	{
		private readonly PlayerComponent _playerComponent;
		private readonly IBoundsProvider _boundsProvider;

		public PlayerBoundsService(PlayerComponent playerComponent, IBoundsProvider boundsProvider)
		{
			_playerComponent = playerComponent;
			_boundsProvider = boundsProvider;
		}

		public void Update()
		{
			Vector2 extents = _boundsProvider.GetBounds();

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