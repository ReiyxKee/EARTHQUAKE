using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject CheckpointUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.SetFloat("Checkpoint_X", this.transform.position.x);
            PlayerPrefs.SetFloat("Checkpoint_Y", this.transform.position.y);
            PlayerPrefs.SetFloat("Checkpoint_Z", this.transform.position.z);

            PlayerPrefs.SetFloat("TimeSpend", GameObject.Find("Player_Prefab/Player").GetComponent<Player_Stat>().TimeSpend);

            CheckpointUI.SetActive(true);
            CheckpointUI.GetComponent<UI_Checkpoint>().timer = 3;
            CheckpointUI.GetComponent<UI_Checkpoint>().reset = false;

            Debug.Log(PlayerPrefs.GetFloat("TimeSpend"));
            Destroy(this.gameObject);
        }
    }
}