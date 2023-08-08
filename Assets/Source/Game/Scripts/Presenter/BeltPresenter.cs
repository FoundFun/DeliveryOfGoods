using UnityEngine;
using DeliveryOfGoods.Model;

[RequireComponent(typeof(Rigidbody))]
public class BeltPresenter : MonoBehaviour
{
    [SerializeField] private bool _isRotatable = true;

    [SerializeField] private BeltView _onBeltView;

    private const float _speed = 2.3f;

    private readonly float _speedMaterial = 0.3f;

    private Belt _model;
    private Rigidbody _rigidbody;
    private Material _onBeltMaterial;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _onBeltMaterial = _onBeltView.GetComponent<Renderer>().material;
    }

    private void Start()
    {
        _model.Init(transform.eulerAngles.y);
    }

    private void FixedUpdate()
    {
        Scroll(_model.MoveDirection);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && _isRotatable)
            Rotate();
    }

    public void Init(Belt model)
    {
        _model = model;
    }

    private void Rotate()
    {
        _model.Rotate(transform);
    }

    private void Scroll(Vector3 direction)
    {
        Vector3 nextPosition = _rigidbody.position;
        _rigidbody.position += direction * _speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(nextPosition);

        _onBeltMaterial.mainTextureOffset += Vector2.up * _speedMaterial * _speed * Time.fixedDeltaTime;
    }
}