using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerDeadController PlayerDeadController;
    //どのくらい飛ばし率を追加するか
    public float AddBurstRate = 5.5f;
    // 力の強さ
    public float forceAmount = 10f; 
    //飛ばす方向の指定
    public bool AnyWhere = false;
    public bool AddForceRight = false;
    //飛び率のテキスト
    public GameObject BurstRateText;
    //飛び率の初期値
    float IniBurstRate;
    //プレイヤーの足についてる当たり判定が反応しているかどうか
    public bool FootTrigger = false;

    //このモンスターがすでに死んでいるかどうか
    private bool AlreadyDead = false;
    //監督
    public CameraShake CameraShake;
    public PlayerUIController playerUIController;

    void Start()
    {
        IniBurstRate = PlayerDeadController.BurstRate;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerDeadController.HitEnemy == false && AlreadyDead == false)
        {
            //蛇かつアタック中の時は何もしない
            if(collision.gameObject.name == "PlayerSnake")
            {
                SnakeController snakeController = collision.gameObject.GetComponent<SnakeController>();
                if(snakeController.isAttaking)
                {
                    return;
                }
                else
                {
                    PlayerVSEnemy(collision);
                }
            }
            
            else
            {
                PlayerVSEnemy(collision);
            }
        }
    }


    private void PlayerVSEnemy(Collision2D collision)
    {
        // プレイヤーの位置と敵の位置を取得
        Vector3 playerPosition = collision.transform.position;
        Vector3 enemyPosition = transform.position;

        
        //飛ばし率の追加
        StartCoroutine(playerUIController.UpdateTextValue(PlayerDeadController.BurstRate-IniBurstRate,PlayerDeadController.BurstRate+AddBurstRate-IniBurstRate));
        PlayerDeadController.BurstRate += AddBurstRate;
        //操作不能
        PlayerDeadController.HitEnemy = true;
        //SE
        PlayerDeadController.PlayHitSE();
        //カメラ揺らす
        StartCoroutine(CameraShake.Shake(0.3f, 0.2f));
        
        //プレイヤーの飛ばされてるアニメーション
        collision.gameObject.GetComponent<Animator>().SetTrigger("Damaged");
        //飛び率のアニメーション
        BurstRateText.GetComponent<Animator>().SetTrigger("Damaged");
        // ヒットストップ処理挿入
        HitStopController.instance.StartHitStop(0.7f);
        // プレイヤーのRigidbody2Dを取得
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        //飛ぶ方向(可変的)
        if(AnyWhere)
        {
            rb.AddForce(Mathf.Sign(collision.gameObject.transform.localScale.x)*(-1)*new Vector2(1,0)* forceAmount*PlayerDeadController.BurstRate, ForceMode2D.Impulse);
        }
        else if (AddForceRight == false)
        {
            // 左方向に力を加える（x軸負方向）
            rb.AddForce(Vector2.left * forceAmount*PlayerDeadController.BurstRate, ForceMode2D.Impulse);
        }
        else
        {
            //右側に力を加える
            rb.AddForce(Vector2.right * forceAmount*PlayerDeadController.BurstRate, ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //プレイヤーの足についているトリガーに衝突した時は敵を倒すときのみ
            //したがってここで敵を倒す処理をする

            //飛ばし率の減少
            if(PlayerDeadController.BurstRate-AddBurstRate/2<=0)
            {
                playerUIController.StartChangeText(PlayerDeadController.BurstRate-IniBurstRate,0.0f);
                PlayerDeadController.BurstRate = 0;
            }
            else
            {
                playerUIController.StartChangeText(PlayerDeadController.BurstRate-IniBurstRate,PlayerDeadController.BurstRate-IniBurstRate-AddBurstRate/2);
                PlayerDeadController.BurstRate -= AddBurstRate/2;
            }
            
            // プレイヤーが敵の上を踏んだ場合: 敵を倒す
            //SE
            PlayerDeadController.PlayKnockEnemySE();
            TextReduceAnimaton();
            // ヒットストップ処理挿入
            HitStopController.instance.StartHitStop(0.3f);
            AlreadyDead = true;
            Destroy(gameObject);  //ここでオブジェクトがなくなるので単純にコルーチンをスタートするだけではテキストは徐々に減らなかった
        }
    }

    // プレイヤーが敵の上に乗っているかを判定するメソッド
    private bool IsAboveEnemy(Vector3 playerPosition, Vector3 enemyPosition, Collision2D collision)
    {
        
        //プレイヤーの当たり判定の大きさ取得
        Vector2 colliderSize = collision.gameObject.GetComponent<CapsuleCollider2D>().size;
        // プレイヤーが敵の上にいるかかつ速度が負の方向かどうかを判定
        if (playerPosition.y > enemyPosition.y+colliderSize.y*collision.gameObject.transform.localScale.y/2 && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            return true;
        }

        return false;
    }

    //蛇でも使うので関数化
    public void TextReduceAnimaton()
    {
        //文字のアニメーション
        BurstRateText.GetComponent<Animator>().SetTrigger("Reduce");
    }
}
