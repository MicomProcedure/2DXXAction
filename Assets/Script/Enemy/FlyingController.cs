using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 他のオブジェクトがトリガーに触れたときの処理
        if (other.gameObject.tag == "Player") // 例として、タグが「Player」の場合に反応
        {
            this.animator.SetTrigger("Attack");
        }
        Debug.Log("Flying");
    }
}
