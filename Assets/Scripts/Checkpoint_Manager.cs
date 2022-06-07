using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint_Manager : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerParent;
    public Vector3 LatestCheckPoint;
    public int thisRoomCode;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Checkpoint_X") && PlayerPrefs.HasKey("Checkpoint_Y") && PlayerPrefs.HasKey("Checkpoint_Z"))
        {
            LatestCheckPoint = new Vector3(PlayerPrefs.GetFloat("Checkpoint_X"), PlayerPrefs.GetFloat("Checkpoint_Y"), PlayerPrefs.GetFloat("Checkpoint_Z"));
        }

        if (PlayerPrefs.HasKey("RestartGame") && PlayerPrefs.GetInt("RestartGame") == 1)
        {
            ToCheckpoint();
            Player.GetComponent<Player_Stat>().TimeSpend = PlayerPrefs.GetFloat("TimeSpend");
            PlayerPrefs.SetInt("RestartGame", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartCheckPoint()
    {
        PlayerPrefs.SetInt("RestartGame", 1);
        SceneManager.LoadScene(thisRoomCode);
    }

    public void ToCheckpoint()
    {
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = LatestCheckPoint;
        Player.GetComponent<CharacterController>().enabled = true;
        Player.GetComponent<Player_Stat>().HP = PlayerPrefs.GetFloat("HP");
    }

}
