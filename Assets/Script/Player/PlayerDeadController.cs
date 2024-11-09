using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadController : MonoBehaviour
{
    //復活するy座標
    public float RevivalY = 3.0f;
    [SerializeField] PlayerChangeController PlayerChangeController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが落ちた時の処理
        if(PlayerChangeController.PlayerObjectList[PlayerChangeController.NowCharacterIndex].transform.position.y < -15)
        {
            PlayerChangeController.PlayerObjectList[PlayerChangeController.NowCharacterIndex].transform.position = new Vector2(Camera.main.transform.position.x,RevivalY);
        }
    }
}
