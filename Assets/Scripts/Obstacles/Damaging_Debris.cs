using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging_Debris : MonoBehaviour
{
    public GameObject Damaging;
    public bool Floored = false;

    public AudioSource gethitsound, gethitsound2;
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
            GetHitSoundPlay();
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

    private void GetHitSoundPlay()
    {
        gethitsound = GameObject.FindGameObjectWithTag("GetHitSound").GetComponent<AudioSource>();
        gethitsound2 = GameObject.FindGameObjectWithTag("GetHitSound2").GetComponent<AudioSource>();
        gethitsound.Play();
        gethitsound2.Play();
    }
}
