using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeController : MonoBehaviour
{
    // 子オブジェクトを格納するリスト
    public List<GameObject> PlayerObjectList = new List<GameObject>();
    public int NowCharacterIndex;
    //監督
    [SerializeField] MusicController MusicController;

    // Start is called before the first frame update
    void Start()
    {
        // すべての子オブジェクトを取得してリストに追加
        foreach (Transform child in transform)
        {
            PlayerObjectList.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //音楽が変わったタイミングを取得
        if(MusicController.MusicLooped)
        {
            // アクティブなオブジェクトのインデックスを取得
            int activeIndex = GetActiveObjectIndex(PlayerObjectList);
            //アクティブ状態をオフにする
            PlayerObjectList[activeIndex].SetActive(false);
            //ランダムで次のキャラクターを選ぶ
            while (true)
            {
                int randomValue = Random.Range(0,PlayerObjectList.Count);
                //今と同じキャラでなければ抽選終了
                if(randomValue != activeIndex)
                {
                    //新しいキャラクターを出す
                    PlayerObjectList[randomValue].SetActive(true);
                    NowCharacterIndex = randomValue;
                    //位置を調整
                    PlayerObjectList[NowCharacterIndex].transform.position = PlayerObjectList[activeIndex].transform.position;
                    //速度情報の受け渡し
                    PlayerObjectList[NowCharacterIndex].GetComponent<Rigidbody2D>().velocity = PlayerObjectList[activeIndex].GetComponent<Rigidbody2D>().velocity;
                    break;
                }
            }
            MusicController.MusicLooped = false;
        }
    }

    //何番目のオブジェクトがアクティブ状態かを調べる関数
    int GetActiveObjectIndex(List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].activeSelf)  // アクティブ状態かを確認
            {
                return i;  // アクティブなオブジェクトのインデックスを返す
            }
        }
        return -1;  // アクティブなオブジェクトが見つからない場合は -1 を返す
    }
}
