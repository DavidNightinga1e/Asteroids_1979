using Source.Components;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovementController : MonoBehaviour
{
    public float movementSpeed = 500f;
    public float rotationSpeed = 100f;

    private PlayerComponent _playerComponent;

    private void Awake()
    {
        this.AutoFindComponent(out _playerComponent);
    }

    private void Update()
    {
        var rotate = Input.GetAxis("Rotate");
        var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        _playerComponent.TargetRigidbody2D.AddForce(_playerComponent.transform.TransformVector(move) * (movementSpeed * Time.deltaTime));
        _playerComponent.transform.Rotate(Vector3.forward, -rotate * rotationSpeed * Time.deltaTime);
    }
}