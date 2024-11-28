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
    [Header("相手に当たって操作が不能な時間")]
    public float HitEnemyStopTime = 0.5f;
    float HitEnemyDelta = 0;

    //監督
    public PlayerDeadController PlayerDeadController;
    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.Grounded = false;
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
    }

    //画面外に出たら勝手に呼ばれる関数
    void OnBecameInvisible() 
    {
        if(this.gameObject.activeSelf)
        {
            PlayerDeadController.PlayerDead = true;
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
            //蛙，蛇以外の操作
            if(gameObject.name != "PlayerFlog" && gameObject.name != "PlayerSnake")
            {
                NormalMove();
            }
            //蛙
            else if(gameObject.name == "PlayerFlog")     
            {
                FlogMove();
            }
            //蛇
            else if(gameObject.name == "PlayerSnake")
            {
                SnakeMove();
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
        if(gameObject.name != "PlayerFlog")
        {
            NormalJump();
        }
        else if(gameObject.name == "PlayerFlog")
        {
            FlogJump();
        }
    }

    private void NormalMove()
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

    private void NormalJump()
    {
        if(Input.GetKeyDown(playerKeyCode.JumpKey) && this.Grounded)
        {
            //上向きに力を加える
            rigid2D.AddForce(JumpForce);
            //空中にいる判定にする
            this.Grounded = false;
        }
    }

    private void FlogMove()
    {
        //右を向く
        if(Input.GetKey(playerKeyCode.MoveRight))
        {
           key = 1;
           if(this.rigid2D.velocity.x < 0)
           {
                this.rigid2D.velocity = new Vector2(this.rigid2D.velocity.x*0.9f,this.rigid2D.velocity.y);
           }
        }
        //左を向く
        else if(Input.GetKey(playerKeyCode.MoveLeft))
        {
            key = -1;
            if(this.rigid2D.velocity.x > 0)
            {
                this.rigid2D.velocity = new Vector2(this.rigid2D.velocity.x*0.9f,this.rigid2D.velocity.y);
            }
        }

        //地面にいるならば速度0
        if(Grounded)
        {
            this.rigid2D.velocity = new Vector2(0,this.rigid2D.velocity.y);
        }
    }

    private void FlogJump()
    {
        //蛙特有のジャンプの処理
        if(Input.GetKeyDown(playerKeyCode.JumpKey) && Grounded)
        {
            rigid2D.AddForce(new Vector2(Mathf.Sign(transform.localScale.x)*JumpForce.x,JumpForce.y));
            Grounded = false;
        }
    }

    private void SnakeMove()
    {
        SnakeController snakeController = gameObject.GetComponent<SnakeController>();
        
        //体を伸ばしてないときは普通の動き
        if(snakeController.isAttaking==false && snakeController.charging == false)
        {
            NormalMove();
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
