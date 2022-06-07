using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagedUI : MonoBehaviour
{
    public Color On;
    public Color Off;
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            this.GetComponent<Image>().color = new Color32(255,0,0, (byte)((Mathf.Clamp(Timer, 0,1)/1) * 255));
        }
        else
        {
            this.GetComponent<Image>().color = Off;
        }
    }
}
