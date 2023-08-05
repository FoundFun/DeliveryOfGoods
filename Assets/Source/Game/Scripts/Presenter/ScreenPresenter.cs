using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScreenPresenter : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}