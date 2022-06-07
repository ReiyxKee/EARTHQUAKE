using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging_Debris : MonoBehaviour
{
    public GameObject Damaging;
    public bool Floored = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Damaging == null)
        {
            Damaging = GameObject.Find("DMGIMG");
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (Damaging == null)
        {
            Damaging = GameObject.Find("DMGIMG");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && !Floored)
        {
            Damaging.GetComponent<DamagedUI>().Timer += 0.75f;
            collision.gameObject.GetComponent<Player_Stat>().HP -= 10;
            Floored = true;
        }

        if (collision.gameObject.layer == 6)
        {

            this.gameObject.layer = 6;
            Floored = true;
        }
    }
}
