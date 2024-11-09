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
    public bool Grounded = true;

    //移動速度
    public float MoveSpeed;

    //Rigidbody
    Rigidbody2D rigid2D;
    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //ジャンプについての処理
        Jump();
        //移動について
        Move();
    }

    private void Move()
    {
        //右に動く
        if(Input.GetKey(playerKeyCode.MoveRight))
        {
            this.rigid2D.velocity = new Vector2(MoveSpeed,this.rigid2D.velocity.y);
        }
        //左に動く
        else if(Input.GetKey(playerKeyCode.MoveLeft))
        {
            this.rigid2D.velocity = new Vector2(-MoveSpeed,this.rigid2D.velocity.y);
        }
        else
        {
            this.rigid2D.velocity = new Vector2(0,this.rigid2D.velocity.y);
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
