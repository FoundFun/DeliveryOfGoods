using System.Collections.Generic;
using System.Linq;
using Source.Game.Scripts.Presenter;
using UnityEngine;

namespace Source.Game.Scripts.Spawn
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : BoxPresenter
    {
        private List<T> _poolObject;

        protected List<T> Initialize(T[] gameObject, GameObject container)
        {
            _poolObject = new List<T>();

            foreach (T tObject in gameObject)
            {
                T template = Instantiate(tObject, container.transform);
                template.gameObject.SetActive(false);

                _poolObject.Add(template);
            }

            return _poolObject;
        }

        protected bool TryGetObject(out T gameObject, int index)
        {
            gameObject = _poolObject.Where(template => template.gameObject.activeSelf == false).ElementAtOrDefault(index);

            return gameObject != null;
        }
    }
}