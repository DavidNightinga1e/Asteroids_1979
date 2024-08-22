using System;
using UnityEngine;

namespace Source.Controllers
{
	/// <summary>
	/// Можно написать кастомный PlayerLoop чтобы монобех не кидать
	/// https://docs.unity3d.com/ScriptReference/LowLevel.PlayerLoopSystem.html
	/// </summary>
	public class LocatorRunner : MonoBehaviour
	{
		private ControllerLocator ControllerLocator { get; set; }

		private void Start() => ControllerLocator.OnStart();

		private void Update() => ControllerLocator.OnUpdate();

		private void FixedUpdate() => ControllerLocator.OnFixedUpdate();

		public static void CreateNewRunner(ControllerLocator locator)
		{
			var o = new GameObject("Runner");
			var addedComponent = o.AddComponent<LocatorRunner>();
			addedComponent.ControllerLocator = locator;
			locator.OnAwake();
		}
	}
}