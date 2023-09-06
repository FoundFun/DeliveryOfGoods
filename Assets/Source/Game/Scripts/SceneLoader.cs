using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DeliveryOfGoods.Model;

public class SceneLoader : MonoBehaviour
{
    public void Load()
    {
        SceneManager.LoadScene(Config.NameScene + Config.CurrentLevel);
    }
}
