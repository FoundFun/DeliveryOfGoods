using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DeliveryOfGoods.Model;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Config _config;
    
    public void Load()
    {
        SceneManager.LoadScene(_config.NameScene + _config.CurrentLevel);
    }
}
