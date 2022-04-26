using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Manager : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerParent;
    public Vector3 LatestCheckPoint;
    // Start is called before the first frame update
    void Start()
    {
        LatestCheckPoint = Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToCheckpoint()
    {
        Player.GetComponent<CharacterController>().enabled = false;
        Player.transform.position = LatestCheckPoint;
        Player.GetComponent<CharacterController>().enabled = true;
        Player.GetComponent<Player_Stat>().HP = 100;
    }

}
