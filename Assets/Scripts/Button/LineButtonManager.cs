using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class LineButtonManager : MonoBehaviour
{
    public GameObject button;
    private List<GameObject> buttons;

    public List<string> hiragana;
    public List<string> katakana;
    public List<string> eisuu;
    public Image answerBack;

    public Text inputText;
    public int inputMax = 12;

    float nowTime = 0.0f;

    // ==========================================================
    // ボタンの位置設定、内容設定
    // ==========================================================
    public void TypingButtonAppear(string type)
    {
        if (buttons != null)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i] == null) continue;
                Destroy(buttons[i]);
            }
            buttons.Clear();
        }

        inputText.text = "";

        List<string> nowType = hiragana;
        if (type == "カナ") nowType = katakana;
        else if (type == "英数") nowType = eisuu;

        float buttonSizeS = 60.0f;
        float buttonSize = buttonSizeS;
        float buttonTrueSize = 55.0f;

        int yokoMax = 12;
        int tateMax = 5;
        float hidariSpace = 0;

        if (type == "英数")
        {
            yokoMax = 10;
            tateMax = 4;
            buttonSize = 75.0f;
            buttonTrueSize = 70.0f;
        }
        
        var hidariue = new Vector3(1280 / 2 - buttonSize * yokoMax / 2, 
                                    720 / 4 - buttonSize * ((float)tateMax / 2.0f) + 24, 
                                    0);

        buttons = new List<GameObject>();
        for (int i = 0; i < nowType.Count; i++)
        {
            if (nowType[i] == "")
            {
                buttons.Add(null);
                continue;
            }
            var but = Instantiate(button, transform);
            var butTransform = but.GetComponent<RectTransform>();

            int tate = tateMax - (i % tateMax) - 1;
            int yoko = (i / tateMax);

            if (type == "英数")
            {
                hidariSpace = (i % tateMax) * (buttonSize * 0.5f);
            }
            butTransform.sizeDelta = new Vector2(buttonTrueSize, buttonTrueSize);

            butTransform.position = new Vector3(hidariSpace + hidariue.x + yoko * buttonSize, hidariue.y + tate * buttonSize, 0);
            buttons.Add(but);
            buttons[i].GetComponentInChildren<Text>().text = nowType[i];

            var color = buttons[i].GetComponentInChildren<Text>().color;
            color.a = 0.0f;
            buttons[i].GetComponentInChildren<Text>().color = color;

            color = buttons[i].GetComponentInChildren<Image>().color;
            color.a = 0.0f;
            buttons[i].GetComponentInChildren<Image>().color = color;

            color = answerBack.color;
            color.a = 0.0f;
            answerBack.color = color;
        }

        buttonSize = buttonSizeS;

        {
            var delButton = Instantiate(button, transform);
            var butTransform = delButton.GetComponent<RectTransform>();
            butTransform.position = new Vector3(hidariSpace + hidariue.x + buttonSize * 13, hidariue.y + 1.5f * buttonSize, 0);
            butTransform.sizeDelta = new Vector2(buttonSize * 2, buttonSize);
            delButton.GetComponentInChildren<Text>().text = "削除";

            var color = delButton.GetComponentInChildren<Text>().color;
            color.a = 0.0f;
            delButton.GetComponentInChildren<Text>().color = color;

            color = delButton.GetComponentInChildren<Image>().color;
            color.a = 0.0f;
            delButton.GetComponentInChildren<Image>().color = color;

            buttons.Add(delButton);
        }
        {
            var retButton = Instantiate(button, transform);
            var butTransform = retButton.GetComponent<RectTransform>();
            butTransform.position = new Vector3(hidariSpace + hidariue.x + buttonSize * 13, hidariue.y + 0.25f * buttonSize, 0);
            butTransform.sizeDelta = new Vector2(buttonSize * 2, buttonSize * 1.5f);
            retButton.GetComponentInChildren<Text>().text = "決定";
            // buttons.Add(retButton);

            var color = retButton.GetComponentInChildren<Text>().color;
            color.a = 0.0f;
            retButton.GetComponentInChildren<Text>().color = color;

            color = retButton.GetComponentInChildren<Image>().color;
            color.a = 0.0f;
            retButton.GetComponentInChildren<Image>().color = color;

            buttons.Add(retButton);
        }
    }

    // ==========================================================
    // ボタンのフェードイン
    // ==========================================================
    public void ButtonStarting(float maxTime)
    {
        if (buttons == null) return;
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] == null) continue;

            var color = buttons[i].GetComponentInChildren<Image>().color;
            color.a += (1 / maxTime) * (Time.deltaTime * 60);
            buttons[i].GetComponentInChildren<Image>().color = color;

            var backColor = answerBack.color;
            backColor.a = color.a * 0.4f;
            answerBack.color = backColor;
        }
    }

    // ==========================================================
    // どのボタンがクリックされたかを取得
    // ==========================================================
    public string ButtonCheck()
    {
        if (buttons == null) return "";
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] == null) continue;
            var color = buttons[i].GetComponentInChildren<Text>().color;
            color.a = 1.0f;
            buttons[i].GetComponentInChildren<Text>().color = color;

            color = buttons[i].GetComponentInChildren<Image>().color;
            color.a = 1.0f;
            buttons[i].GetComponentInChildren<Image>().color = color;

            if (buttons[i].GetComponent<OneButton>().onClick)
            {
                buttons[i].GetComponent<OneButton>().ResetClick();
                string getText = buttons[i].GetComponentInChildren<Text>().text;
                if (getText == "決定")
                {
                    if (inputText.text == "")
                        return "無回答";
                    return inputText.text;
                }
                else if (getText == "削除")
                {
                    var t = inputText.text;
                    if (t.Length > 0)
                        inputText.text = t.Substring(0, t.Length - 1);
                }
                else if (getText == "゛")
                {
                    var t = inputText.text;
                    var lastT = t[t.Length - 1].ToString();

                    //UTF-8 NFDで他の文字と結合すると濁点になるもの
                    char dakuten = '\x3099';

                    //くっつけてNFCにNormalize
                    string add = (lastT + dakuten).Normalize(NormalizationForm.FormC);
                    string remove = lastT.Normalize(NormalizationForm.FormD);

                    if (lastT.Length == add.Length)
                    {
                        if (add == "ヷ") return "";
                        if (add == "ヺ") return "";
                        // if (add == "ゔ") return "";
                        inputText.text = t.Substring(0, t.Length - 1) + add;
                    }
                    else if (lastT.Length != remove.Length)
                    {
                        inputText.text = t.Substring(0, t.Length - 1) + remove[0].ToString();
                    }
                }
                else if (getText == "゜")
                {
                    var t = inputText.text;
                    var lastT = t[t.Length - 1].ToString();

                    //UTF-8 NFDで他の文字と結合すると濁点になるもの
                    char dakuten = '\x309A';

                    //くっつけてNFCにNormalize
                    string add = (lastT + dakuten).Normalize(NormalizationForm.FormC);
                    string remove = lastT.Normalize(NormalizationForm.FormD);

                    if (lastT.Length == add.Length)
                    {
                        inputText.text = t.Substring(0, t.Length - 1) + add;
                    }
                    else if (lastT.Length != remove.Length)
                    {
                        inputText.text = t.Substring(0, t.Length - 1) + remove[0].ToString();
                    }
                }
                else
                {
                    inputText.text = inputText.text + getText;
                }
            }
        }
        return "";
    }

    // ==========================================================
    // ボタンを消滅させる
    // ==========================================================
    public void ButtonClear()
    {
        if (buttons != null)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i] == null) continue;
                Destroy(buttons[i]);
            }
            buttons.Clear();
        }
        var color = answerBack.color;
        color.a = 0;
        answerBack.color = color;

        inputText.text = "";
    }
}
