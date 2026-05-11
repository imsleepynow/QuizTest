using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    enum MenuPage
    {
        None,
        Parent,
        Main,
        Zukan,
        SoundTest,
        Status,
        Debug,
    }
    
    public Button mainModeKettei; // 一番メインのモードの選択
    public Text uiText;

    public string naviText;
    public Text naviField;

    // public soundManager;

    MenuPage menuPage = MenuPage.None;
    private bool moveScene = false;


    void Start()
    {
        menuPage = MenuPage.Parent;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (menuPage == MenuPage.Main)
        {
            
        }

        if (naviField.text == "")
        {
            naviField.text = naviText.Replace("\\n", "\n"); ;
        }

        //if ((!moveScene) && (!// soundManager.IsNowPlaying()))
        //    // soundManager.PlayMusic(1);
    }

    // 前の画面に戻る
    public void PushBackBefore()
    {
        if (menuPage == MenuPage.Parent)
        {
            QuizRuleManager.keishiki = QuizRuleManager.QuizKeishiki.None;
            //// soundManager.StopMusic();
            moveScene = true;
            SceneManager.LoadScene("TitleScene");
        }
        else
        {
            menuPage = MenuPage.Parent;
        }
    }

    // 一番メインのモードの選択
    public void PushMainMode()
    {
        mainModeKettei.enabled = false;
        menuPage = MenuPage.Main;

        QuizRuleManager.keishiki = QuizRuleManager.QuizKeishiki.All;
        // soundManager.StopMusic();
        SceneManager.LoadScene("QuizScene");
        moveScene = true;
    }

    // 一番メインのモードの選択
    public void PushArcadeMode()
    {
        mainModeKettei.enabled = false;
        menuPage = MenuPage.Main;

        //QuizRuleManager.keishiki = QuizRuleManager.QuizKeishiki.All;
        QuizRuleManager.keishiki = QuizRuleManager.QuizKeishiki.Arcade;
        // soundManager.StopMusic();
        SceneManager.LoadScene("QuizScene");
        moveScene = true;
    }

    // 図鑑モードの選択
    public void PushLibrary()
    {
        mainModeKettei.enabled = false;
        menuPage = MenuPage.Zukan;

        //QuizRuleManager.keishiki = QuizRuleManager.QuizKeishiki.All;
        // soundManager.StopMusic();
        moveScene = true;
        SceneManager.LoadScene("ZukanScene");
    }

    // サウンドテストモードの選択
    public void PushSoundTest()
    {
        mainModeKettei.enabled = false;
        menuPage = MenuPage.SoundTest;

        //QuizRuleManager.keishiki = QuizRuleManager.QuizKeishiki.All;
        // soundManager.StopMusic();
        moveScene = true;
        SceneManager.LoadScene("SoundTestScene");
    }

    // デバッグ用クイズ確認画面の選択
    public void PushTestQuiz()
    {
        mainModeKettei.enabled = false;
        menuPage = MenuPage.Debug;

        //QuizRuleManager.keishiki = QuizRuleManager.QuizKeishiki.All;
        // soundManager.StopMusic();
        moveScene = true;
        SceneManager.LoadScene("TestQuizScene");
    }

    // 結果確認画面の選択
    public void PushStatus()
    {
        mainModeKettei.enabled = false;
        menuPage = MenuPage.Status;

        //QuizRuleManager.keishiki = QuizRuleManager.QuizKeishiki.All;
        // soundManager.StopMusic();
        moveScene = true;
        SceneManager.LoadScene("StatusScene");
    }
}
