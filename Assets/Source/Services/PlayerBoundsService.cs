using Source.Interfaces;
using UnityEngine;

namespace Source.Controllers
{
	public class PlayerBoundsService : Service, IFixedUpdatable
	{
		private readonly IMovable _playerMovable;
		private readonly IBoundsProvider _boundsProvider;

		public PlayerBoundsService(IMovable playerMovable, IBoundsProvider boundsProvider)
		{
			_playerMovable = playerMovable;
			_boundsProvider = boundsProvider;
		}

		public void FixedUpdate()
		{
			Vector2 extents = _boundsProvider.GetBounds();

			Vector2 p = _playerMovable.GetPosition();
			if (p.x > extents.x)
				_playerMovable.SetPosition(new Vector2(-extents.x, p.y));
			else if (p.x < -extents.x)
				_playerMovable.SetPosition(new Vector2(extents.x, p.y));
			else if (p.y > extents.y)
				_playerMovable.SetPosition(new Vector2(p.x, -extents.y));
			else if (p.y < -extents.y)
				_playerMovable.SetPosition(new Vector2(p.x, extents.y));
		}
	}
}