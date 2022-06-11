using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DownloadHighScores : MonoBehaviour
{
    public GameObject Error;

    public string privateCode = "EogXkMZ3_EGlGcPevnGgkQGsDSTmYvVkm9XCfonBGNog";
    public string publicCode = "62a213d38f40bb11c0787cdb";
    const string webURL = "http://dreamlo.com/lb/"; // Website the keys are for


    public float ResetInterval = 60;
    public float timer;

    public ScoreSlot[] Scoreslot;
    public Ranking[] rankings;

    public bool init;



    public bool initToTop;
    public bool animated;

    public bool ToTop;
    public bool ToBottom;

    public ScrollRect Board;
    public TextMeshProUGUI countdown;
    public TextMeshProUGUI YOURSCORE;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DatabaseDownload");
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        countdown.text = "BOARD RESETS IN: " + ((int)(ResetInterval - timer) / 60).ToString("00") + ":" + ((int)(ResetInterval - timer) % 60).ToString("00");

        timer += Time.unscaledDeltaTime;

        if (initToTop && !animated)
        {
            ToTop = true;
            animated = true;
        }

        if (ToTop)
        {
            if (Board.normalizedPosition.y < 1)
            {
                Board.normalizedPosition = new Vector2(Board.normalizedPosition.x, Board.normalizedPosition.y + 2.5f * Time.deltaTime);
            }
            else
            {
                ToTop = false;
            }
        }
        
        if (ToBottom)
        {
            if (Board.normalizedPosition.y > 0)
            {
                Board.normalizedPosition = new Vector2(Board.normalizedPosition.x, Board.normalizedPosition.y - 2.5f * Time.deltaTime);
            }
            else
            {
                ToTop = false;
            }
        }

        if (timer >= ResetInterval)
        {
            Reset();
        }


        if (rankings.Length == 0)
        {
            ResetInterval = 5;
        }
        else
        {
            if (Scoreslot[0].Name.text != (rankings[0].name))
            {
                ResetInterval = 5;
            }
            else
            {

                ResetInterval = 60;
            }
        }

        YOURSCORE.text = "YOUR CLEAR TIME: " + ((int)(PlayerPrefs.GetFloat("TimeSpend")) / 60).ToString("00") + ":" + ((int)(PlayerPrefs.GetFloat("TimeSpend")) % 60).ToString("00") + ((PlayerPrefs.GetFloat("TimeSpend") % 1)).ToString(".00");

    }

    public void ToTopList()
    {
        ToTop = true;
    }
    
    public void ToBottomList()
    {
        ToBottom = true;
    }

    public void Reset()
    {
        timer = 0;
        DownloadScores();
        SetScoresToMenu();
    }

    public void SetScoresToMenu() //Assigns proper name and score for each text value
    {
        for (int i = 0; i < Scoreslot.Length; i++)
        {
            if (i < rankings.Length && rankings.Length > 0)
            {
                if (rankings[i].name.Length > 0)
                {
                    Scoreslot[i].rank = rankings[i].rank + 1;

                    if (i > 0)
                    {
                        if (Scoreslot[i].rank == Scoreslot[i - 1].rank)
                        {
                            Scoreslot[i].Rank.text = "=";
                        }
                        else
                        {
                            Scoreslot[i].Rank.text = (rankings[i].rank + 1).ToString();
                        }
                    }
                    else
                    {
                        Scoreslot[i].Rank.text = (rankings[i].rank + 1).ToString();
                    }


                    Scoreslot[i].Name.text = rankings[i].name;

                    Scoreslot[i].Time.text = ((int)(rankings[i].time / 60)).ToString("00") + ":" + ((int)(rankings[i].time) % 60).ToString("00") + ((rankings[i].time % 1)).ToString(".00"); 
                }
                else
                {
                    Scoreslot[i].rank = 0;
                    Scoreslot[i].Rank.text = "-";
                    Scoreslot[i].Name.text = "-";
                    Scoreslot[i].Time.text = "-";
                }
            }
            else
            {
                Scoreslot[i].rank = 0;
                Scoreslot[i].Rank.text = "-";
                Scoreslot[i].Name.text = "-";
                Scoreslot[i].Time.text = "-";
            }
        }

        init = true;
    }

    public void DownloadScores()
    {
        StartCoroutine("DatabaseDownload");
    }

    IEnumerator DatabaseDownload()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/0/50");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.text);
            OrganizeInfo(www.text);
            Error.SetActive(false);
        }
        else
        {
            Error.SetActive(true);
        }
    }

    void OrganizeInfo(string rawData) //Divides Scoreboard info by new lines
    {
        string[] entries = rawData.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        Array.Reverse(entries);
        rankings = new Ranking[entries.Length];

        for (int i = 0; i < entries.Length; i++) //For each entry in the string array
        {
            Debug.Log(i);

            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string _username = entryInfo[0];
            float _time = -float.Parse(entryInfo[1]) / 1000;
            int rank = i;

            if (i > 0)
            {
                if (_time == rankings[i - 1].time)
                {
                    rank = rankings[i - 1].rank;
                }
            }

            rankings[i] = new Ranking(rank , _username, _time);
        }


        SetScoresToMenu();
    }

}

[System.Serializable]
public class Ranking
{
    public int rank;
    public string name;
    public float time;


    public Ranking(int _rank, string _username, float _score)
    {
        rank = _rank;
        name = _username;
        time = _score;
    }
}