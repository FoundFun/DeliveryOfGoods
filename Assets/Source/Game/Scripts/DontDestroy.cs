using UnityEngine;

namespace Source.Game.Scripts
{
    public class DontDestroy : MonoBehaviour
    {
        private void Start() => 
            DontDestroyOnLoad(gameObject);
    }
}