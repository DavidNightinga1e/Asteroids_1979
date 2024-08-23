using System;

namespace Source.Interfaces
{
	public interface ICollisionBroadcaster
	{
		event Action<ICollisionBroadcaster> OnCollided;
	}
}