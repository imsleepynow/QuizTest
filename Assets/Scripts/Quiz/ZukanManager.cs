using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZukanManager : MonoBehaviour
{
    public Text title;
    //public GameObject shuttenObject;
    //public Text shutten;
    public Text genre;
    public Text start;
    public Text startLabel;
    public GameObject endObject;
    public Text end;
    public Text endLabel;
    public GameObject hardObject;
    public Text hard;
    public Text hardLabel;
    public Text bun;

    public SpriteRenderer test;

    // Start is called before the first frame update
    void Start()
    {
        // var tex1 = SpreadSheetCheck.readedTextureData[0];
        // var sprite = Sprite.Create((Texture2D)tex1, new Rect(0, 0, tex1.width, tex1.height), new Vector2(0.5f, 0.5f));
        // test.sprite = sprite;
    }

    void Update()
    {
    }

    public void UpdateLibrary(LibraryData data)
    {
        title.text = data.name;
        //shutten.text = data.sakuhin;
        genre.text = data.genre;

        start.text = data.start;

        end.text = data.end;
        endObject.active = (end.text != "");

        if (genre.text == "人物")
        {
            startLabel.text = "誕生";
            endLabel.text = "逝去";
        }
        else if (genre.text == "会社")
        {
            startLabel.text = "創立";
            endLabel.text = "解散";
        }
        else if (genre.text == "アニメ")
        {
            startLabel.text = "放映開始";
            endLabel.text = "放映終了";
        }
        else if ((genre.text == "動画") || (genre.text == "映画"))
        {
            startLabel.text = "公開";
            endLabel.text = "公開終了";
        }
        else if ((genre.text == "イベント"))
        {
            if (end.text != "")
            {
                startLabel.text = "公開開始";
                endLabel.text = "公開終了";
            }
            else
            {
                startLabel.text = "公開";
                endLabel.text = "公開終了";
            }
        }
        else if ((genre.text == "ファンゲーム"))
        {
            startLabel.text = "提供開始";
            endLabel.text = "提供終了";
        }
        else if ((genre.text == "ゲーム"))
        {
            if (end.text != "")
            {
                startLabel.text = "発売開始";
                endLabel.text = "発売終了";
            }
            else
            {
                startLabel.text = "発売";
                endLabel.text = "";
            }
        }
        else
        {
            startLabel.text = "デビュー";
            endLabel.text = "出番終了";
        }

        //shuttenObject.active = 
        //    ((genre.text == "キャラクター") || (genre.text == "アイテム") || (genre.text == "モード") || (genre.text == "地名") || (genre.text == "楽曲") || (genre.text == "楽曲"));
        hardObject.active =
            !((genre.text == "人物") || (genre.text == "会社") || (genre.text == "玩具") || (genre.text == "イベント"));

        hard.text = data.hard;
        bun.text = data.bun;

        //shuttenObject.active = false;
    }



    public void UpdateTestQuiz(ToRecordData data, string mondai, string gameTitle)
    {
        title.text = gameTitle;
        //shutten.text = data.sakuhin;
        genre.text = data.keishiki.ToString();

        start.text = data.looked.ToString();

        end.text = data.firstCollect.ToString();

        startLabel.text = "遭遇済み";
        endLabel.text = "正誤";

        hard.text = data.nanido.ToString();
        hardLabel.text = "難易度";
        bun.text = mondai;

        //shuttenObject.active = false;
    }
}
