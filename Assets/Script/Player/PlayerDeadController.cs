using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadController : MonoBehaviour
{
    //復活するy座標
    public float RevivalY = 3.0f;
    [SerializeField] PlayerChangeController PlayerChangeController;

    public bool PlayerDead = false;

    //飛ばし率
    public float BurstRate = 0.0f;
    //敵に当たった
    public bool HitEnemy = false;
    //SE
    AudioSource aud;
    public AudioClip HitSE;

    // Start is called before the first frame update
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが落ちた時の処理
        if(PlayerChangeController.PlayerObjectList[PlayerChangeController.NowCharacterIndex].transform.position.y < -15)
        {
            PlayerDead = true;
            PlayerChangeController.PlayerObjectList[PlayerChangeController.NowCharacterIndex].transform.position = new Vector2(Camera.main.transform.position.x,RevivalY);
        }

        //飛ばし率100超えたら死亡
        if(100<=BurstRate)
        {
            PlayerDead = true;
        }
    }

    public void PlayHitSE()
    {
        //音楽
        this.aud.PlayOneShot(HitSE);
    }
}
