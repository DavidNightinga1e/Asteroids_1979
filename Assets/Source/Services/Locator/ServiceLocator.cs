using System;
using System.Collections.Generic;

namespace Source.Controllers
{
	public class ServiceLocator
	{
		private readonly Dictionary<Type, Service> _controllers = new();

		private readonly List<IAwakable> _awakables = new();
		private readonly List<IStartable> _startables = new();
		private readonly List<IUpdatable> _updatables = new();
		private readonly List<IFixedUpdatable> _fixedUpdatables = new();

		public void AddService<T>(T service) where T : Service
		{
			_controllers.Add(typeof(T), service);

			if (service is IAwakable awakable)
				_awakables.Add(awakable);
			if (service is IStartable startable)
				_startables.Add(startable);
			if (service is IUpdatable updatable)
				_updatables.Add(updatable);
			if (service is IFixedUpdatable fixedUpdatable)
				_fixedUpdatables.Add(fixedUpdatable);
			
			service.InjectLocator(this);
		}

		public T GetService<T>() where T : Service
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