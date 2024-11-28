using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlogController : MonoBehaviour
{
    //アニメーション
    Animator animator;
    
    //Rigidbody
    Rigidbody2D rigid2D;

    //飛んでいるか
    private bool isJumping;

    //監督
    public PlayerMoveController playerMoveController;


    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMoveController.Grounded == false)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
        // ジャンプ中かどうかのフラグをAnimatorに渡す
        animator.SetBool("isJumping", isJumping);
    }
}
