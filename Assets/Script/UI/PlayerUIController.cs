using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    //飛び率に関して
    public TextMeshProUGUI BurstRateText1;
    public TextMeshProUGUI BurstRateText2;
    public PlayerDeadController PlayerDeadController;

    // Start is called before the first frame update
    void Start()
    {
        BurstRateText1.text = "BurstRate:";
        BurstRateText2.text = "                0.0%";
    }

    // Update is called once per frame
    void Update()
    {
        BurstRateText2.text = "                " + PlayerDeadController.BurstRate.ToString("F1") + "%";
    }
}
