using UnityEngine;

namespace Source.Game.Scripts
{
    public class DontDestoy : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}