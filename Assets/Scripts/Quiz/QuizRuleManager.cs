using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizRuleManager : MonoBehaviour
{
    public enum QuizKeishiki
    {
        None,
        All,
        Arcade,
    }

    public static QuizKeishiki keishiki = QuizKeishiki.None;


    public SpreadSheetCheck spreadSheetCheck;

    public Text uiText;

    //  問題形式
    public KeishikiParent kAll;


    // Start is called before the first frame update
    void Start()
    {
        if (SpreadSheetCheck.readedRecordData == null)
        {
            uiText.text = "プレイヤーデータが読み込めていないようだ";
        }
        else if (SpreadSheetCheck.readedRecordData.name.Replace(" ", "").Replace("　", "") == "")
        {
            uiText.text = "無記名（ランキングには反映されません）";
        }
        else
        {
            uiText.text = SpreadSheetCheck.readedRecordData.name;
        }

        // 全問やる
        if (keishiki == QuizKeishiki.None)
        {
            uiText.text = uiText.text + "\nすいませんこちらのミスです　クイズ出来ません　バクスペで前の画面へ";
        }
        else
        {
            for (int i = 0; i < SpreadSheetCheck.mondaiIdList.Length; i++)
            {
                StartCoroutine(spreadSheetCheck.ReadSheet(i));
            }
        }

        if (keishiki == QuizKeishiki.All) kAll.GetStartTiming();
        if (keishiki == QuizKeishiki.Arcade) kAll.GetStartTiming();
    }

    void Update()
    {
        bool gameEnd = false;

        if (keishiki != QuizKeishiki.None)
        {
            //if (spreadSheetCheck.hasData)
            {
                if (keishiki == QuizKeishiki.All)
                {
                    gameEnd = !kAll.Update2(spreadSheetCheck.hasData, keishiki);
                }
                if (keishiki == QuizKeishiki.Arcade)
                {
                    gameEnd = !kAll.Update2(spreadSheetCheck.hasData, keishiki);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                gameEnd = true;
            }
        }

        // ゲームを終わる
        if (gameEnd)
        {
            SceneManager.LoadScene("MenuScene");
        }

        if (spreadSheetCheck.hasData)
        {
            spreadSheetCheck.RecordInitialize();
        }
    }
}
