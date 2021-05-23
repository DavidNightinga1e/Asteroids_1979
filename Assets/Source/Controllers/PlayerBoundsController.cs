using Source.Components;
using Source.Events;
using UnityEngine;

public class PlayerBoundsController : MonoBehaviour
{
    public Bounds bounds;

    private PlayerComponent _playerComponent;

    private void Awake()
    {
        this.AutoFindComponent(out _playerComponent);
    }

    private void Update()
    {
        if (!bounds.Contains(_playerComponent.transform.position)) 
            EventPool.OnPlayerDestroyed.Invoke();
    }
}