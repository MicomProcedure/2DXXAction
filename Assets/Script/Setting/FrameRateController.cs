using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateController : MonoBehaviour
{
    public int Framerate = 60;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = Framerate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
