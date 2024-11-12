using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    public Transform StartPos;
    public Transform SecondPos;
    [Header("カメラが下側に動く速さ")]
    private float CameraDownSpeed = -0.01f;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = StartPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        //カメラ移動
        if(SecondPos.position.x <= Camera.main.transform.position.x)
        {
            if(SecondPos.position.y <= Camera.main.transform.position.y)
            {
                Camera.main.transform.Translate(0,CameraDownSpeed,0);
            }
            
        }
    }
}
