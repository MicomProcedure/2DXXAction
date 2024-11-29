using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    //アニメーション
    Animator animator;

    Rigidbody2D rigid2D;

    [Header("チャージアタックボタン")]
    public KeyCode AttackKey;
    private float power = 0;
    public bool isAttaking = false;
    private bool FirstAttack = true;
    public bool charging = false;
    public float addpower = 0.1f;
    [Header("チャージアタックで進む距離")]
    public Vector3 ChargeAttackDistance;
    [Header("チャージアタックの敵に対する強さ")]
    public float SnakeAttackForce = 1;
    
    //スケールの初期値
    private Vector2 IniScale;
    //SE
    AudioSource aud;
    public AudioClip BatSE;
    //監督
    public PlayerDeadController playerDeadController;
    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        IniScale = new Vector2(transform.localScale.x,transform.localScale.y);
        isAttaking = false;
        charging = false;
        FirstAttack = true;
        //SE
        this.aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //アニメの速度
        AnimationSpeed();    
        //チャージアタック
        ChargeAttack();    
    }

    private void ChargeAttack()
    {
        //パワーをためる
        if(Input.GetKey(AttackKey))
        {
            charging = true;
            //速度0に
            this.rigid2D.velocity = new Vector2(0,rigid2D.velocity.y);
        }
        //キーを離したらパワー解放
        else
        {
            charging = false;
        }
        
        //チャージ中に関する動き
        if(charging)    
        {
            if(transform.localScale.x < 0.13)
            {
                power += addpower;
                //体を伸ばす
                transform.localScale = new Vector2(Mathf.Sign(transform.localScale.x)*IniScale.x + Mathf.Sign(transform.localScale.x)*power,IniScale.y);
                isAttaking = true;
            }
        }
        else if(isAttaking)
        {
            //一度だけ来るように
            if(FirstAttack)
            {
                this.rigid2D.AddForce(new Vector2(Mathf.Sign(transform.localScale.x)*ChargeAttackDistance.x*power*100,ChargeAttackDistance.y),ForceMode2D.Impulse);
                FirstAttack = false;
            }
        }

        animator.SetBool("charging", charging);
        animator.SetBool("isAttaking",isAttaking);
    }

    private void AnimationSpeed()
    {
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);
        if(this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx/ 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //敵に当たった場合は敵を吹き飛ばす
        if(collision.gameObject.tag == "Enemy" && isAttaking)
        {
            Rigidbody2D Rigid2DEnemy = collision.gameObject.GetComponent<Rigidbody2D>();
            //アニメーション,colliderをオフに
            collision.gameObject.GetComponent<Animator>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            // ヒットストップ処理挿入
            HitStopController.instance.StartHitStop(0.3f);
            //SE
            this.aud.PlayOneShot(BatSE);
            //相手をとばす
            Rigid2DEnemy.AddForce(Mathf.Sign(transform.localScale.x)*new Vector2(1,0.5f)*power*SnakeAttackForce, ForceMode2D.Impulse);
            //飛び率に関して
            EnemyController  enemyController = collision.gameObject.GetComponent<EnemyController>();
            //半分になるという設定
            playerDeadController.BurstRate /= 2;
            enemyController.TextReduceAnimaton();
        }
        //何かにあったたら攻撃終了
        isAttaking = false;
        FirstAttack = true;
        power = 0;
        transform.localScale = IniScale;
    }
}
