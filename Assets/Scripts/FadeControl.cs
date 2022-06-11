using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{
    public Image Screen;
    public bool FadeIn;
    public bool FadedIn;

    public bool FadeOut;
    public bool FadedOut;
    
    public int FadeSpeed;

    public float timer;
    public int alpha;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeIn)
        {
            if (alpha > 0)
            {
                FadedIn = false;
                timer += Time.deltaTime;
                if (timer > 0.05f)
                {
                    timer = 0;
                    alpha -= FadeSpeed;
                }
            }
            else if(alpha <=0)
            {
                alpha = 0;
                FadedIn = true;
                FadeIn = false;
            }
        }

        if (FadeOut)
        {
            if (alpha < 255)
            {
                FadedOut = false;

                timer += Time.deltaTime;
                if (timer > 0.05f)
                {
                    timer = 0;
                    alpha += FadeSpeed;
                }
            }
            else if(alpha >= 255)
            {
                alpha = 255;
                FadedOut = true;
                FadeOut = false;
            }
        }
        alpha = Mathf.Clamp(alpha, 0, 255);
        Screen.color = new Color32(0, 0, 0, (byte)alpha);
    }
}
