using System;

namespace Source.Interfaces
{
	public interface IPlayerDestroyBroadcaster
	{
		public event Action OnPlayerDestroy;
	}
}