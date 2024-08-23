namespace ServiceLocators
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