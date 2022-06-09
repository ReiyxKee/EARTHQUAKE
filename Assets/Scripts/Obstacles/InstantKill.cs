using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour
{
    public AudioSource instantkillsound, instantkillsound2;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player_Stat>().HP = 0;

            instantkillsound = GameObject.FindGameObjectWithTag("GetHitSound2").GetComponent<AudioSource>();
            instantkillsound2 = GameObject.FindGameObjectWithTag("GetHitSound").GetComponent<AudioSource>();
            instantkillsound.Play();
            instantkillsound2.Play();
        }
    }
}
