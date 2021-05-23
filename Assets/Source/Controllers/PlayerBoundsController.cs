using Source.Components;
using Source.Events;
using UnityEngine;

public class PlayerBoundsController : MonoBehaviour
{
    private BoundsComponent _boundsComponent;
    private PlayerComponent _playerComponent;

    private void Awake()
    {
        this.AutoFindComponent(out _playerComponent);
        this.AutoFindComponent(out _boundsComponent);
    }

    private void Update()
    {
        if (!_boundsComponent.Bounds.Contains(_playerComponent.transform.position))
            EventPool.OnPlayerDestroyed.Invoke();
    }
}