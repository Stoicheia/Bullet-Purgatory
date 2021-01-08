using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GoldIndicatorUI : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    public void Refresh()
    {
        goldText.text = GlobalStats.instance.Coins.ToString();
    }
}
