using Source.Components;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerComponent _playerComponent;

    private const float MovementSpeed = 500f;
    private const float RotationSpeed = 100f;

    private void Awake()
    {
        this.AutoFindComponent(out _playerComponent);
    }

    private void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        _playerComponent.TargetRigidbody2D.AddForce(_playerComponent.transform.up *
                                                    (vertical * MovementSpeed * Time.deltaTime));
        _playerComponent.TargetRigidbody2D.AddTorque(-horizontal * RotationSpeed * Time.deltaTime);
    }
}