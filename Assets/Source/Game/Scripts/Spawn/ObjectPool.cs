using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    private List<T> _poolObject = new List<T>();

    protected void Initialize(T[] gameObject, GameObject container)
    {
        for (int i = 0; i < gameObject.Length; i++)
        {
            T template = Instantiate(gameObject[i], container.transform);
            template.gameObject.SetActive(false);

            _poolObject.Add(template);
        }
    }

    protected bool TryGetObject(out T gameObject)
    {
        gameObject = _poolObject.FirstOrDefault(template => template.gameObject.activeSelf == false);

        return gameObject != null;
    }
}