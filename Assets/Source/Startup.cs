using ServiceLocators;
using UnityEngine;

namespace Source
{
	public static class Startup
	{
		[RuntimeInitializeOnLoadMethod]
		public static void OnLoad()
		{
			Application.targetFrameRate = 60;

			var locator = new AsteroidsServiceLocator();
			LocatorRunner.CreateNewRunner(locator);
		}
	}
}