using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlogController : MonoBehaviour
{
    //アニメーション
    Animator animator;
    //飛んでいるか
    public bool isJumping = false;
    int key = 1;
    //Rigidbody
    Rigidbody2D rigid2D;
    [Header("ジャンプ力(正の値を入力)")]
    public Vector2 FlogJumpForce;
    [Header("減速の割合")]
    public float DecreaseSpeed = 0.05f;


    public PlayerKeyCode playerKeyCode;


    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ジャンプ中かどうかのフラグをAnimatorに渡す
        animator.SetBool("isJumping", isJumping);

        Jamp();
        Move();
        ChangeAngle();
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
        //右を向く
        if(Input.GetKey(playerKeyCode.MoveRight))
        {
           key = 1;
           if(this.rigid2D.velocity.x < 0)
           {
                this.rigid2D.velocity = new Vector2(this.rigid2D.velocity.x*DecreaseSpeed,this.rigid2D.velocity.y);
           }
        }
        //左を向く
        else if(Input.GetKey(playerKeyCode.MoveLeft))
        {
            key = -1;
            if(this.rigid2D.velocity.x > 0)
            {
                this.rigid2D.velocity = new Vector2(this.rigid2D.velocity.x*DecreaseSpeed,this.rigid2D.velocity.y);
            }
        }

        //地面にいるならば速度0
        if(isJumping == false)
        {
            this.rigid2D.velocity = new Vector2(0,this.rigid2D.velocity.y);
        }
    }

    private void Jamp()
    {
        //蛙特有のジャンプの処理
        if(Input.GetKeyDown(playerKeyCode.JumpKey) && isJumping == false)
        {
            rigid2D.AddForce(new Vector2(Mathf.Sign(transform.localScale.x)*FlogJumpForce.x,FlogJumpForce.y));
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
