using System;
using System.Collections.Generic;

namespace Source.Controllers
{
	public class ControllerLocator
	{
		private readonly Dictionary<Type, IController> _controllers = new();

		private readonly List<IAwakable> _awakables = new();
		private readonly List<IStartable> _startables = new();
		private readonly List<IUpdatable> _updatables = new();
		private readonly List<IFixedUpdatable> _fixedUpdatables = new();

		public void AddController<T>(T controller) where T : IController
		{
			_controllers.Add(typeof(T), controller);

			if (controller is IAwakable awakable)
				_awakables.Add(awakable);
			if (controller is IStartable startable)
				_startables.Add(startable);
			if (controller is IUpdatable updatable)
				_updatables.Add(updatable);
			if (controller is IFixedUpdatable fixedUpdatable)
				_fixedUpdatables.Add(fixedUpdatable);
		}

		public T GetController<T>() where T : IController
		{
			Type key = typeof(T);
			bool containsKey = _controllers.ContainsKey(key);
			if (containsKey)
				return (T)_controllers[key];
			return default;
		}

		public void OnAwake()
		{
			foreach (IAwakable awakable in _awakables)
				awakable.Awake();
		}

		public void OnStart()
		{
			foreach (IStartable startable in _startables)
				startable.Start();
		}

		public void OnUpdate()
		{
			foreach (IUpdatable updatable in _updatables)
				updatable.Update();
		}

		public void OnFixedUpdate()
		{
			foreach (IFixedUpdatable fixedUpdatable in _fixedUpdatables)
				fixedUpdatable.FixedUpdate();
		}
	}
}