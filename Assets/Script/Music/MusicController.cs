using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource aud;
    public bool MusicLooped = false;
    //音楽の再生中の時間を取得
    private float previousTime;

    // Start is called before the first frame update
    void Start()
    {
        this.aud = GetComponent<AudioSource>();

        this.previousTime = aud.time;
    }

    // Update is called once per frame
    void Update()
    {
        // AudioSourceが再生中かつループしている場合
        if (aud.isPlaying && aud.loop)
        {
            // 再生位置が前回のフレームよりも小さければループしたと判断
            if (aud.time < previousTime)
            {
                MusicLooped = true;
            }
            previousTime = aud.time;
        }
    }
}
