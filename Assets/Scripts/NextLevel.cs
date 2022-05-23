using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int SceneNumber;
 void OnTriggerEnter(Collider other)
    {
        ChangeScene(SceneNumber);
        PlayerPrefs.SetFloat("TimeSpend", GameObject.Find("Player_Prefab/Player").GetComponent<Player_Stat>().TimeSpend);
        PlayerPrefs.SetInt("NextScene", 1);
    }

    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
