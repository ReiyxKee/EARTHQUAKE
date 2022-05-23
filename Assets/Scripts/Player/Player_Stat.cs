using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Stat : MonoBehaviour
{
    public GameObject GameOver;
    public Slider hp;
    public float HP = 100;
    public UX_Controls ux;

    public TextMeshProUGUI timer;
    public float TimeSpend;

    public void Start()
    {
        if (PlayerPrefs.HasKey("NextScene"))
        {
            if (PlayerPrefs.GetInt("NextScene") == 1)
            {
                TimeSpend = PlayerPrefs.GetFloat("TimeSpend");
                PlayerPrefs.SetInt("NextScene", 0);
            }
        }
    }

    public void Update()
    {
        hp.value = HP;
        TimeSpend += Time.deltaTime;

        timer.text = ((int)(TimeSpend / 60)).ToString("00") + ":" +((int)(TimeSpend % 60)).ToString("00");

        if (HP <= 0)
        {
            GameOver.SetActive(true);
            ux.CursorLocked = false;
        }
    }
}
