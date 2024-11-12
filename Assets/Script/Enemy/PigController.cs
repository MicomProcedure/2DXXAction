using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public PlayerDeadController PlayerDeadController;
    //どのくらい飛ばし率を追加するか
    public float AddBurstRate = 5.5f;
    // 力の強さ
    public float forceAmount = 10f; 
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerDeadController.HitEnemy == false)
        {
            //飛ばし率の追加
            PlayerDeadController.BurstRate += AddBurstRate;
            //操作不能
            PlayerDeadController.HitEnemy = true;
            //SE
            PlayerDeadController.PlayHitSE();
            // ヒットストップ処理挿入
            HitStopController.instance.StartHitStop(0.7f);

            // プレイヤーのRigidbody2Dを取得
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 左方向に力を加える（x軸負方向）
                rb.AddForce(Vector2.left * forceAmount*PlayerDeadController.BurstRate, ForceMode2D.Impulse);
            }
        }
    }
}
