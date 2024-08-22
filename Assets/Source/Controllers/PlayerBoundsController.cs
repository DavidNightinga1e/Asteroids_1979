using Source.Components;
using Source.Events;
using Source.Utilities;
using UnityEngine;

public class PlayerBoundsController : MonoBehaviour
{
    private PlayerComponent _playerComponent;
    private Camera _camera;

    private void Awake()
    {
        this.AutoFindComponent(out _playerComponent);
        this.AutoFindComponent(out _camera);
    }

    private void Update()
    {
        Vector2 extents = _camera.GetOrthographicExtents();

        Rigidbody2D targetRb = _playerComponent.TargetRigidbody2D;
        Vector2 p = targetRb.position;
        if (p.x > extents.x)
            targetRb.position = new Vector2(-extents.x, p.y);
        else if (p.x < -extents.x)
            targetRb.position = new Vector2(extents.x, p.y);
        else if (p.y > extents.y)
            targetRb.position = new Vector2(p.x, -extents.y);
        else if (p.y < -extents.y)
            targetRb.position = new Vector2(p.x, extents.y);
    }
}