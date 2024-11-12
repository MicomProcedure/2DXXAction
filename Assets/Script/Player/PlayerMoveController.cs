using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    //キーについて
    public PlayerKeyCode playerKeyCode;

    //ジャンプ力
    public Vector3 JumpForce;

    //地面にいるかどうか
    public bool Grounded = false;

    //移動速度
    public float MoveSpeed;

    //Rigidbody
    Rigidbody2D rigid2D;

    //向いている方向
    int key = 1;
    //アニメーション
    Animator animator;
    //相手に当たって操作が不能な時
    public float HitEnemyStopTime = 0.5f;
    float HitEnemyDelta = 0;

    //監督
    public PlayerDeadController PlayerDeadController;
    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.Grounded = false;
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //ジャンプについての処理
        Jump();
        //移動について
        Move();
        //反転
        ChangeAngle();
        //アニメの速度
        AnimationSpeed();
    }

    //画面外に出たら勝手に呼ばれる関数
    void OnBecameInvisible() 
    {
        if(this.gameObject.activeSelf)
        {
            PlayerDeadController.PlayerDead = true;
        }
    }

    private void AnimationSpeed()
    {
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);
        if(this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = (speedx + 3)/ 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }
        
    }

    private void ChangeAngle()
    {
        if(key == 1 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        }
        else if(key == -1 && 0 < transform.localScale.x)
        {
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        }
    }

    private void Move()
    {
        if(PlayerDeadController.HitEnemy == false)
        {
            //右に動く
            if(Input.GetKey(playerKeyCode.MoveRight))
            {
                this.rigid2D.velocity = new Vector2(MoveSpeed,this.rigid2D.velocity.y);
                this.key = 1;
            }
            //左に動く
            else if(Input.GetKey(playerKeyCode.MoveLeft))
            {
                this.rigid2D.velocity = new Vector2(-MoveSpeed,this.rigid2D.velocity.y);
                this.key = -1;
            }
            else
            {
                this.rigid2D.velocity = new Vector2(0,this.rigid2D.velocity.y);
            }
        }
        else
        {
            HitEnemyDelta += Time.deltaTime;
            if(HitEnemyStopTime<HitEnemyDelta)
            {
                HitEnemyDelta = 0;
                PlayerDeadController.HitEnemy = false;
            }
        }
        
    }

    private void Jump()
    {
        if(Input.GetKeyDown(playerKeyCode.JumpKey) && this.Grounded)
        {
            //上向きに力を加える
            rigid2D.AddForce(JumpForce);
            //空中にいる判定にする
            this.Grounded = false;
            //アニメーション
            this.animator.SetTrigger("JumpTrigger");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            this.Grounded = true;
        }
    }
}
