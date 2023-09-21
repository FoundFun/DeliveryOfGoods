using Source.Game.Scripts.Configure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Game.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        private Config _config;

        public void Init(Config config)
        {
            _config = config;
        }
    
        public void Load()
        {
            SceneManager.LoadScene(_config.NameScene + _config.CurrentLevel);
        }
    }
}
