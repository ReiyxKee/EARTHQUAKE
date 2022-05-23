using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPS : MonoBehaviour
{
    public Player_Stat playerstat;
    public float dps_dmg;
    bool dps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dps)
        {
            GameObject.Find("Player_Prefab/Player").GetComponent<Player_Stat>().HP -=Time.deltaTime * dps_dmg;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            dps = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            dps = false;
        }
    }
}
