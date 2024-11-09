using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float CameraMoveSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //横軸は強制スクロール，縦軸はプレイヤーに合わせて
        transform.Translate(new Vector3(CameraMoveSpeed, 0, 0));
    }
}
