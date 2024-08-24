using System;
using System.Collections.Generic;
using Source.Interfaces;
using Source.Models;
using Source.Views;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Source.Factories
{
	public static class AsteroidFactory
	{
		public static AsteroidModel CreateRandomAsteroid(
			IAsteroidFactorySettingsProvider settingsProvider, IBoundsProvider boundsProvider)
		{
			int i = Random.Range(0, settingsProvider.Sizes.Count);
			GameObject prefab = settingsProvider.Prefabs[i];

			(Vector2 point, float rotation) = RandomPointAndDirectionOnBounds(boundsProvider);

			Quaternion quaternion = Quaternion.Euler(0, 0, rotation);
			GameObject instance = Object.Instantiate(prefab, point, quaternion);
			var view = instance.GetComponent<AsteroidView>();
			Transform transform = view.Rb.transform;

			float speed = Random.Range(settingsProvider.MinLinearSpeed, settingsProvider.MaxLinearSpeed);
			float rotationSpeed = Random.Range(settingsProvider.MinRotationSpeed, settingsProvider.MaxRotationSpeed);
			transform.localScale = Vector3.one * settingsProvider.Sizes[i];
			Vector3 direction = transform.up * speed;

			return new AsteroidModel(view, i, direction, rotationSpeed);
		}

		private static (Vector2, float) RandomPointAndDirectionOnBounds(IBoundsProvider boundsProvider)
		{
			Vector2 spawnExtents = boundsProvider.GetBounds();

			float RandomOnX() => Random.Range(-spawnExtents.x, spawnExtents.x);
			float RandomOnY() => Random.Range(-spawnExtents.y, spawnExtents.y);

			int side = Random.Range(0, 4);
			Vector2 point = side switch
			{
				0 => new Vector2(-spawnExtents.x, RandomOnY()),
				1 => new Vector2(RandomOnX(), spawnExtents.y),
				2 => new Vector2(spawnExtents.x, RandomOnY()),
				3 => new Vector2(RandomOnX(), -spawnExtents.y),
				_ => throw new Exception()
			};

			int direction = Random.Range(-45, 45) + side switch
			{
				0 => -90,
				1 => -180,
				2 => -270,
				3 => 0,
				_ => throw new Exception()
			};

			return (point, direction);
		}

		public static AsteroidModel CreateChildAsteroid(IAsteroidFactorySettingsProvider settings,
			IMovable parentMovable, AsteroidModel parentModel)
		{
			IReadOnlyList<GameObject> prefabs = settings.Prefabs;
			int i = Random.Range(0, prefabs.Count);
			Vector2 position = parentMovable.GetPosition() + new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
			GameObject instance = Object.Instantiate(prefabs[i], position, GetRandomRotation());
			var view = instance.GetComponent<AsteroidView>();
			Transform transform = view.Rb.transform;

			float speed = Random.Range(parentModel.Direction.magnitude, settings.MaxLinearSpeed);
			float angularSpeed = Random.Range(settings.MinRotationSpeed, settings.MaxRotationSpeed);
			int size = Random.Range(0, parentModel.Size);
			transform.localScale = Vector3.one * settings.Sizes[size];
			Vector3 direction = transform.up * speed;

			return new AsteroidModel(view, size, direction, angularSpeed);
		}

		private static Quaternion GetRandomRotation() => Quaternion.Euler(0, 0, Random.Range(0, 360));
	}
}