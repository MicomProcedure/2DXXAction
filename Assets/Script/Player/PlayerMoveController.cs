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
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
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
            this.rb.velocity = new Vector3(MoveSpeed,this.rb.velocity.y,0);
        }
        //左に動く
        if(Input.GetKey(playerKeyCode.MoveLeft))
        {
            this.rb.velocity = new Vector3(-MoveSpeed,this.rb.velocity.y,0);
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(playerKeyCode.JumpKey) && this.Grounded)
        {
            //上向きに力を加える
            rb.AddForce(JumpForce,ForceMode.Impulse);
            //空中にいる判定にする
            this.Grounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            this.Grounded = true;
        }
    }
}
