using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxPresenter : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private bool _isBuy;

    private Rigidbody _rigidbody;

    public int Price => _price;

    public bool IsBuy => _isBuy;

    public Vector3 TargetPosition { get; private set; }

    public void Init()
    {
        TargetPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Complete()
    {
        Debug.Log("+1");
    }

    public void Return()
    {
        gameObject.SetActive(false);
        _rigidbody.velocity = Vector3.zero;
    }

    public void Buy()
    {
        _isBuy = true;
    }
}