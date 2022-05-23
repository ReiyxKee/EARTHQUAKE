using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Checkpoint : MonoBehaviour
{
    public float timer = 3;
    public bool reset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (timer <= 0 && !reset)
        {
            reset = true;

            this.gameObject.SetActive(false);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
