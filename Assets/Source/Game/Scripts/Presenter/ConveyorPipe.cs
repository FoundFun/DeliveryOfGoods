using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class ConveyorPipe : MonoBehaviour
{
    private const float _speedRotation = 1.07f;

    private readonly float _speedMaterial = 0.3f;

    private Rigidbody _rigidbody;
    private Material _material;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _material = GetComponent<Renderer>().material;
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        Quaternion rotation = _rigidbody.rotation;
        Quaternion move = Quaternion.Euler(0, 400 * _speedRotation / (51 * Mathf.PI), 0);
        _rigidbody.MoveRotation(rotation * move);

        _material.mainTextureOffset += Vector2.right * _speedMaterial * _speedRotation * Time.fixedDeltaTime;
    }
}