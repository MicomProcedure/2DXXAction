using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    //アニメーション
    Animator animator;

    //Rigidbody
    Rigidbody2D rigid2D;
    //監督
    public PlayerMoveController PlayerMoveController;
    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //アニメの速度
        AnimationSpeed();
        //ジャンプした時
        if(PlayerMoveController.Grounded == false)
        {
            //アニメーション
            this.animator.SetTrigger("JumpTrigger");
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
}
