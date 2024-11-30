using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    Animator animator;
    public PlayerChangeController playerChangeController;
    public float detectRange = 10f; // 敵が反応する範囲

    private bool isPlayerNear = false; // プレイヤーが近いかどうか

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject nowPlayer = playerChangeController.PlayerObjectList[playerChangeController.NowCharacterIndex];
         // プレイヤーとの距離を計算
        float distanceToPlayer = Vector2.Distance(transform.position, nowPlayer.transform.position);

        // 範囲内にプレイヤーがいる場合
        if (distanceToPlayer <= detectRange)
        {
            if (!isPlayerNear)
            {
                isPlayerNear = true;
                OnPlayerApproach(); // プレイヤーが近づいたときの処理
            }
        }
    }

    // プレイヤーが近づいたときの処理
    void OnPlayerApproach()
    {
        this.animator.SetTrigger("Attack");
    }
}
