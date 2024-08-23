using System;
using UnityEngine;

namespace ServiceLocators
{
	/// <summary>
	/// Можно написать кастомный PlayerLoop чтобы монобех не кидать
	/// https://docs.unity3d.com/ScriptReference/LowLevel.PlayerLoopSystem.html
	/// </summary>
	public class LocatorRunner : MonoBehaviour
	{
		private ServiceLocator ServiceLocator { get; set; }

		private void Start() => ServiceLocator.OnStart();

		private void Update() => ServiceLocator.OnUpdate();

		private void FixedUpdate() => ServiceLocator.OnFixedUpdate();

		public static void CreateNewRunner(ServiceLocator locator)
		{
			var o = new GameObject("Runner");
			var addedComponent = o.AddComponent<LocatorRunner>();
			addedComponent.ServiceLocator = locator;
			locator.OnAwake();
		}
	}
}