using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    //飛び率に関して
    public TextMeshProUGUI BurstRateText;
    public PlayerDeadController PlayerDeadController;

    // Start is called before the first frame update
    void Start()
    {
        BurstRateText.text = "BurstRate:0.0%";
    }

    // Update is called once per frame
    void Update()
    {
        BurstRateText.text = "BurstRate:" + PlayerDeadController.BurstRate.ToString("F1") + "%";
    }
}
