namespace Source.Controllers
{
	public abstract class Service
	{
		protected ServiceLocator ServiceLocator { get; private set; }

		public void InjectLocator(ServiceLocator serviceLocator)
		{
			ServiceLocator = serviceLocator;
		}
	}
}