using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ObjectPool<T> : MonoBehaviour where T : BoxPresenter
{
    private List<T> _poolObject;

    protected List<T> Initialize(T[] gameObject, GameObject container)
    {
        _poolObject = new List<T>();

        for (int i = 0; i < gameObject.Length; i++)
        {
            T template = Instantiate(gameObject[i], container.transform);
            template.gameObject.SetActive(false);

            _poolObject.Add(template);
        }

        return _poolObject;
    }

    protected bool TryGetObject(out T gameObject)
    {
        gameObject = _poolObject.FirstOrDefault(template => template.gameObject.activeSelf == false && template.IsBuy == true);

        return gameObject != null;
    }
}