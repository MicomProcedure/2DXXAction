using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public AudioClip KnockEnemySE;
    //SEが一度だけ呼ばれるように
    private bool isSceneChange = false; 

    //赤いパネル
    public GameObject RedPanel;

    float Scenedelta = 0;

    //監督
    public CameraShake CameraShake;

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
        }

        //飛ばし率100超えたら死亡
        if(100<=BurstRate)
        {
            PlayerDead = true;
        }

        //死んだときの処理
        if(PlayerDead)
        {    
            if(isSceneChange == false)
            {
                isSceneChange = true;
                PlayHitSE();
            }
            //カメラ揺らす
            StartCoroutine(CameraShake.Shake(0.3f, 0.6f));
            //画面を赤くする
            RedPanel.SetActive(true);
            // SEの再生時間だけ待ってからシーンを変更
            WaitAndChangeScene(HitSE.length);
        }
    }
    private void WaitAndChangeScene(float delay)
    {
        Scenedelta += Time.deltaTime;
        if(delay < Scenedelta)
        {
            SceneManager.LoadScene("MainScene");   // シーンを変更
        }
    }

    public void PlayHitSE()
    {
        //音楽
        this.aud.PlayOneShot(HitSE);
    }
    public void PlayKnockEnemySE()
    {
        //音楽
        this.aud.PlayOneShot(KnockEnemySE);
    }
}
