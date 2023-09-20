using Source.Game.Scripts.Config;
using Source.Game.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Config _config;
    
    public void Load()
    {
        SceneManager.LoadScene(_config.NameScene + _config.CurrentLevel);
    }
}
