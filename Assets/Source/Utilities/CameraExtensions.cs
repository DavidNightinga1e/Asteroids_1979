using UnityEngine;

namespace Source.Utilities
{
	public static class CameraExtensions
	{
		public static Vector2 GetOrthographicExtents(this Camera c)
		{
			float cameraAspect = c.aspect;
			float size = c.orthographicSize;
			return new Vector2(size * cameraAspect, size);
		}
	}
}