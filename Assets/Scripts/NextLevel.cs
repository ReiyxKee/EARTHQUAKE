using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int SceneNumber;

    public bool FadeReq;
    public bool TriggerbyFade;
    public FadeControl fadeControl;

    private void Update()
    {
        if (TriggerbyFade)
        {
            if (fadeControl.FadedOut)
            {
                ChangeScene(SceneNumber);
                PlayerPrefs.SetFloat("TimeSpend", GameObject.Find("Player_Prefab/Player").GetComponent<Player_Stat>().TimeSpend);
                PlayerPrefs.SetInt("NextScene", 1);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!FadeReq)
            {
                ChangeScene(SceneNumber);
                PlayerPrefs.SetFloat("TimeSpend", GameObject.Find("Player_Prefab/Player").GetComponent<Player_Stat>().TimeSpend);
                PlayerPrefs.SetInt("NextScene", 1);
            }
            else
            {
                TriggerbyFade = true;
                fadeControl.FadeOut = true;
            }
        }
    }

    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void ResetPlayerPref()
    {
        PlayerPrefs.DeleteAll();
    }
}
