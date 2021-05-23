using Source.Common;
using UnityEngine;

namespace Source.Components
{
    public class BoundsComponent : MonoBehaviour
    {
        [SerializeField] private Bounds2D bounds;

        public Bounds2D Bounds => bounds;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(bounds.center, bounds.Size);
        }
    }
}