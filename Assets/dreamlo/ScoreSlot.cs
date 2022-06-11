using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreSlot : MonoBehaviour
{
    public int rank;
    public TextMeshProUGUI Rank;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Time;
    public Image Background;

    private void Update()
    {
        switch(rank)
        {
            case 1:
                Background.color = new Color32(255, 205, 0, 200);
                break;
            case 2:
                Background.color = new Color32(219, 219, 219, 200);
                break;
            case 3:
                Background.color = new Color32(255, 124, 0, 200);
                break;
        }
    }

}
