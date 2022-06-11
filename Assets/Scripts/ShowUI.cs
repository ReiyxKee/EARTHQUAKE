using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowUI : MonoBehaviour
{
    public Button Menu;
    public bool On;
    public Animator anim;
    public Animator Enter;
    public TextMeshProUGUI Shower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Show", On);
        Shower.text = On ? "HIDE RANKING" : "SHOW RANKING";
    }
    public void Toggle()
    {
        On = !On;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void EnterGame()
    {
        Enter.SetBool("Enter", true);
    }
}
