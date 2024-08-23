using System;

namespace Source.Interfaces
{
	public interface IDestroyBroadcaster
	{
		event Action<IDestroyBroadcaster> OnDestroyed;
	}
}