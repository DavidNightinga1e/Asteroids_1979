using Source.Interfaces;
using Source.Utilities;
using UnityEngine;

namespace Source.Adapters
{
	public class OrthographicCameraBoundsProvider : IBoundsProvider
	{
		private readonly Camera _camera;
		
		public OrthographicCameraBoundsProvider(Camera camera)
		{
			_camera = camera;
		}
		
		public Vector2 GetBounds()
		{
			return _camera.GetOrthographicExtents();
		}
	}
}