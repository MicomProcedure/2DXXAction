using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OjisanController : MonoBehaviour
{
    //アニメーション
    Animator animator;

    //Rigidbody
    Rigidbody2D rigid2D;
    [Header("おじさんの操作キーになりうるキー")]
    public string OjisanKey = "QAZWSXEDCRFV";
    [Header("おじさんの操作キーになった文字を表示")]
    public TextMeshProUGUI HowToMoveOjiText;
    //監督
    public PlayerMoveController PlayerMoveController;
    public PlayerKeyCode playerKeyCode;
    

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        KeyCode[] RandomOjisanKey = PickTwoUniqueCharacters();
        //おじさんの操作方法を与える
        playerKeyCode.MoveLeft = RandomOjisanKey[0];
        playerKeyCode.MoveRight = RandomOjisanKey[1];

        //画面に表示
        HowToMoveOjiText.text = "Left:" + RandomOjisanKey[0].ToString() + "\nRight:" + RandomOjisanKey[1].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //アニメの速度
        AnimationSpeed();
    }

    private void AnimationSpeed()
    {
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);
        if(this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = (speedx)/ 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }
    }

    //おじさんの操作方法をランダムで決める
    //同じ文字にならないように抽選
    KeyCode[] PickTwoUniqueCharacters()
    {
        // 入力文字列が2文字未満なら処理できない
        if (OjisanKey.Length < 2) return null;

        // 1つ目のランダム文字を選ぶ
        int firstIndex = Random.Range(0, OjisanKey.Length);
        string firststring = OjisanKey[firstIndex].ToString();

        // 2つ目のランダム文字を選ぶ（重複を避けるためループ）
        string secondstring;
        do
        {
            int secondIndex = Random.Range(0, OjisanKey.Length);
            secondstring = OjisanKey[secondIndex].ToString();
        } while (secondstring == firststring);

        //return new char[] { firststring, secondstring };
        return new KeyCode[] {(KeyCode)System.Enum.Parse(typeof(KeyCode), firststring),
        (KeyCode)System.Enum.Parse(typeof(KeyCode), secondstring)};
    }
}
