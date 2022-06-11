using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UploadHighscore : MonoBehaviour
{
    public dreamloLeaderBoard leaderBoard;
    public DownloadHighScores download;
    public InputField input;

    public GameObject AntiCheat;
    public GameObject Submission;
    public GameObject BackMenu;

    public string Name;
    
    public float _P;
    public float Time;

    public bool Test;
    // Start is called before the first frame update
    void Start()
    {
        _P = PlayerPrefs.GetFloat("TimeSpend");
    }

    // Update is called once per frame
    void Update()
    {
        Time = PlayerPrefs.GetFloat("TimeSpend");
        Name = input.text;

        if (Time != _P)
        {
            AntiCheat.SetActive(true);
        }

        if (Test)
        {
            leaderBoard.AddScore(Name, (int)(-Time * 1000));
            Test = false;
        }
    }

    public void Submit()
    {
        if (Time == _P)
        {
            leaderBoard.AddScore(Name, (int)(-Time * 1000));
            Submission.SetActive(false);
            BackMenu.SetActive(true);
        }
    }
}
