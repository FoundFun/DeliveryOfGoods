using UnityEngine;

public abstract class ScreenPresenter : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Open()
    {
        OpenScreen();
    }

    public void Close()
    {
        CloseScreen();
    }

    protected virtual void OpenScreen()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    protected virtual void CloseScreen()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}