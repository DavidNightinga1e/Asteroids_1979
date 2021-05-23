using System;
using UnityEngine;

namespace Source.Components
{
    public class GameComponentBase : MonoBehaviour
    {
        [Header("Game Component Base")] [SerializeField] private Rigidbody2D targetRigidbody2D;
        [SerializeField] private PolygonCollider2D polygonCollider2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Rigidbody2D TargetRigidbody2D => targetRigidbody2D;
        public PolygonCollider2D PolygonCollider2D => polygonCollider2D;
        public SpriteRenderer SpriteRenderer => spriteRenderer;

#if UNITY_EDITOR
        [ContextMenu("Auto Set")]
        public void AutoSet()
        {
            this.AutoGetChildComponent(out targetRigidbody2D);
            this.AutoGetChildComponent(out polygonCollider2D);
            this.AutoGetChildComponent(out spriteRenderer);
        }
#endif
    }
}