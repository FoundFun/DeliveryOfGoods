using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class ConveyorPipePresenter : MonoBehaviour
{
    private const float SpeedRotation = 1.07f;
    private const float SpeedMaterial = 0.3f;
    private const float MultiplierSpeed = 400;

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
        Quaternion move = Quaternion.Euler(0, MultiplierSpeed * SpeedRotation / (51 * Mathf.PI), 0);
        _rigidbody.MoveRotation(rotation * move);

        _material.mainTextureOffset += Vector2.right * SpeedMaterial * SpeedRotation * Time.fixedDeltaTime;
    }
}