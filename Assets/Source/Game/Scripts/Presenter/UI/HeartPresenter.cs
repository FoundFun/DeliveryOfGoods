using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HeartPresenter : MonoBehaviour
{
    [SerializeField] private Image _image;

    public float Fill { get; private set; }

    public void ToFill()
    {
        _image.fillAmount = 1;
        Fill = _image.fillAmount;
    }

    public void Empty()
    {
        _image.fillAmount = 0;
        Fill = _image.fillAmount;
    }
}