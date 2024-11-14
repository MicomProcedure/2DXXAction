using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    //Rigidbody
    Rigidbody2D rigid2D;

    [Header("ホバリングの力(速度)")]
    public float HoveringSpeedY = 5.0f;
    [Header("ホバリングできる回数")]
    public int HoveringLimit = 3;
    private int HoveringCount = 0;
    //監督
    public PlayerMoveController PlayerMoveController;
    public PlayerKeyCode playerKeyCode;
    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //空中にいるときにジャンプボタンを押したらホバリング風の挙動をする
        if(Input.GetKeyDown(playerKeyCode.JumpKey))
        {
            if(PlayerMoveController.Grounded == false && HoveringCount < HoveringLimit)
            {
                this.rigid2D.velocity = new Vector3(this.rigid2D.velocity.x,HoveringSpeedY);
                //カウント
                this.HoveringCount += 1;
            }
        }

        //地面に下りた際にホバリングのカウントをリセット
        ResetHoveringCount();
    }

    void ResetHoveringCount()
    {
        if(PlayerMoveController.Grounded)
        {
            this.HoveringCount = 0;
        }
    }
}
