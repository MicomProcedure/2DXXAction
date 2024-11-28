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

    //スケールの初期値
    private Vector2 IniScale;
    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        IniScale = new Vector2(transform.localScale.x,transform.localScale.y);
        isAttaking = false;
        charging = false;
        FirstAttack = true;
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
                transform.localScale = new Vector2(IniScale.x + power,IniScale.y);
                isAttaking = true;
            }
        }
        else if(isAttaking)
        {
            //一度だけ来るように
            if(FirstAttack)
            {
                this.rigid2D.AddForce(new Vector2(transform.localScale.x*ChargeAttackDistance.x*power*100,ChargeAttackDistance.y),ForceMode2D.Impulse);
                FirstAttack = false;
            }

            //地面についているなら?一定時間なら?アタック終了
            //isAttaking = false;
        }
        else
        {
            power = 0;
            transform.localScale = IniScale;

        }

        animator.SetBool("charging", charging);
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
        //何かにあったたら攻撃終了
        isAttaking = false;
        FirstAttack = true;
    }
}
