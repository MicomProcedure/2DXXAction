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

    //飛び率が徐々に変わる処理
    public float duration = 0.5f; // 数値が変化するのにかける時間

    // Start is called before the first frame update
    void Start()
    {
        BurstRateText1.text = "BurstRate:";
        BurstRateText2.text = "                0.0%";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator UpdateTextValue(float start, float target) 
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float currentValue = Mathf.RoundToInt(Mathf.Lerp(start, target, t));
            BurstRateText2.text = "                " + currentValue.ToString("F1") + "%";
            yield return null;
        }

        // 最後に確実に目標値をセット
        BurstRateText2.text = "                " + target.ToString("F1") + "%";
    }

    public void StartChangeText(float start,float target)
    {
        StartCoroutine(UpdateTextValue(start,target));
    }
}
