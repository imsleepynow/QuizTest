using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonManager : MonoBehaviour
{
    public GameObject button4;
    public GameObject button2;
    public GameObject buttonSentsunagi;
    public LineRenderer line;
    public GameObject lineCanvas;
    private List<GameObject> buttons;
    private List<LineRenderer> lines;
    //public GameObject typing;

    bool isMultiCheck = false;
    bool useKetteiButton = false;
    bool sentsunagi = false;

    float nowTime = 0.0f;

    string answerString = "";

    // ==========================================================
    // ボタンの位置設定、内容設定
    // ==========================================================
    public void ButtonAppear4(List<string> sentakushis, bool isMulti = false, bool useKettei = false)
    {
        isMultiCheck = isMulti;
        useKetteiButton = useKettei;
        answerString = "";
        sentsunagi = false;

        if (buttons != null)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                Destroy(buttons[i]);
            }
            buttons.Clear();
        }
        if (lines != null)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Destroy(lines[i]);
            }
            lines.Clear();
        }

        buttons = new List<GameObject>();
        for (int i = 0; i < sentakushis.Count; i++)
        {
            var but = Instantiate(button4, transform);
            var butTransform = but.GetComponent<RectTransform>();
            var world = new Vector3(1280 / 2, 720 / 2 - 64 * (1 + i), 0);
            butTransform.position = transform.TransformPoint(world);
            buttons.Add(but);
            buttons[i].GetComponentInChildren<Text>().text = sentakushis[i];

            var color = buttons[i].GetComponentInChildren<Text>().color;
            color.a = 0.0f;
            buttons[i].GetComponentInChildren<Text>().color = color;

            color = buttons[i].GetComponentInChildren<Image>().color;
            color.a = 0.0f;
            buttons[i].GetComponentInChildren<Image>().color = color;
        }

        if (useKetteiButton)
        {
            float buttonSizeS = 60.0f;
            float buttonSize = buttonSizeS;
            float buttonTrueSize = 55.0f;

            int yokoMax = 12;
            int tateMax = 5;
            float hidariSpace = 0;

            var hidariue = new Vector3(1280 / 2 - buttonSize * yokoMax / 2,
                                    720 / 4 - buttonSize * ((float)tateMax / 2.0f) + 24,
                                    0);

            var retButton = Instantiate(button4, transform);
            var butTransform = retButton.GetComponent<RectTransform>();
            var world = new Vector3(hidariSpace + hidariue.x + buttonSize * 15.5f, hidariue.y + 0.25f * buttonSize, 0);
            butTransform.position = transform.TransformPoint(world);
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
    public void ButtonAppear2(List<string> sentakushis)
    {
        isMultiCheck = false;
        answerString = "";
        sentsunagi = false;

        if (buttons != null)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                Destroy(buttons[i]);
            }
            buttons.Clear();
        }
        if (lines != null)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Destroy(lines[i]);
            }
            lines.Clear();
        }

        buttons = new List<GameObject>();
        for (int i = 0; i < sentakushis.Count; i++)
        {
            var but = Instantiate(button2, transform);
            var butTransform = but.GetComponent<RectTransform>();
            var world = new Vector3(1280 / 2 + (i - 0.5f) * 400, 720 / 4, 0);
            butTransform.position = transform.TransformPoint(world);
            buttons.Add(but);
            buttons[i].GetComponentInChildren<Text>().text = sentakushis[i];

            var color = buttons[i].GetComponentInChildren<Text>().color;
            color.a = 0.0f;
            buttons[i].GetComponentInChildren<Text>().color = color;

            color = buttons[i].GetComponentInChildren<Image>().color;
            color.a = 0.0f;
            buttons[i].GetComponentInChildren<Image>().color = color;
        }
    }
    // ==========================================================
    // ボタンの位置設定、内容設定
    // ==========================================================
    public void ButtonAppearSentsunagi(List<string> left, List<string> right)
    {
        isMultiCheck = true;
        useKetteiButton = false;
        sentsunagi = true;
        answerString = "";

        if (buttons != null)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                Destroy(buttons[i]);
            }
            buttons.Clear();
        }
        if (lines != null)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Destroy(lines[i]);
            }
            lines.Clear();
        }

        buttons = new List<GameObject>();
        lines = new List<LineRenderer>();
        for (int i = 0; i < left.Count; i++)
        {
            var but = Instantiate(buttonSentsunagi, transform);
            var butTransform = but.GetComponent<RectTransform>();
            var world = new Vector3(1280 / 4, 720 / 2 - 64 * (1 + i), 0);
            butTransform.position = transform.TransformPoint(world);
            buttons.Add(but);
            buttons[i].GetComponentInChildren<Text>().text = left[i];

            var color = buttons[i].GetComponentInChildren<Text>().color;
            color.a = 0.0f;
            buttons[i].GetComponentInChildren<Text>().color = color;

            color = buttons[i].GetComponentInChildren<Image>().color;
            color.a = 0.0f;
            buttons[i].GetComponentInChildren<Image>().color = color;

            // 線のプレハブの話
            var lin = Instantiate(line, lineCanvas.transform);
            lines.Add(lin);
            lines[i].SetVertexCount(2);
            lines[i].SetWidth(0.1f, 0.1f);
        }

        for (int i = left.Count; i < left.Count + right.Count; i++)
        {
            var but = Instantiate(buttonSentsunagi, transform);
            var butTransform = but.GetComponent<RectTransform>();
            var world = new Vector3(1280 / 4 * 3, 720 / 2 - 64 * (1 + i - left.Count), 0);
            butTransform.position = transform.TransformPoint(world);
            buttons.Add(but);
            Debug.Log(i);
            buttons[i].GetComponentInChildren<Text>().text = right[i - left.Count];

            var color = buttons[i].GetComponentInChildren<Text>().color;
            color.a = 0.0f;
            buttons[i].GetComponentInChildren<Text>().color = color;

            color = buttons[i].GetComponentInChildren<Image>().color;
            color.a = 0.0f;
            buttons[i].GetComponentInChildren<Image>().color = color;
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
            var color = buttons[i].GetComponentInChildren<Image>().color;
            color.a += (1 / maxTime) * (Time.deltaTime * 60);
            buttons[i].GetComponentInChildren<Image>().color = color;
        }
    }

    // ==========================================================
    // どのボタンがクリックされたかを取得
    // ==========================================================
    public int ButtonCheck()
    {
        if (isMultiCheck) return 0;
        if (buttons == null) return 0;
        for (int i = 0; i < buttons.Count; i++)
        {
            var color = buttons[i].GetComponentInChildren<Text>().color;
            color.a = 1.0f;
            buttons[i].GetComponentInChildren<Text>().color = color;

            color = buttons[i].GetComponentInChildren<Image>().color;
            color.a = 1.0f;
            buttons[i].GetComponentInChildren<Image>().color = color;

            if (buttons[i].GetComponent<OneButton>().onClick)
            {
                buttons[i].GetComponent<OneButton>().ResetClick();
                return i + 1;
            }
        }
        return 0;
    }

    // ==========================================================
    // 順番当て用
    // ==========================================================
    public string ButtonCheck(int kazu)
    {
        if (!isMultiCheck) return "";
        if (buttons == null) return "";
        if (sentsunagi) return "";

        int max = buttons.Count;
        if (useKetteiButton) max = max - 1;

        for (int i = 0; i < max; i++)
        {
            var color = buttons[i].GetComponentInChildren<Text>().color;
            color.a = 1.0f;
            buttons[i].GetComponentInChildren<Text>().color = color;

            color = buttons[i].GetComponentInChildren<Image>().color;
            color.a = 1.0f;
            buttons[i].GetComponentInChildren<Image>().color = color;

            if (buttons[i].GetComponent<OneButton>().onClick)
            {
                string numStr = (i + 1).ToString();
                // もう入力されている場合リセット
                if (answerString.Contains(numStr))
                {
                    if (useKetteiButton)
                    {
                        buttons[i].GetComponent<OneButton>().ResetBlack();
                    }
                    else
                    {
                        buttons[i].GetComponent<OneButton>().ResetNumber();
                    }

                    answerString = answerString.Replace(numStr, "");
                }
                else
                {
                    if (useKetteiButton)
                    {
                        buttons[i].GetComponent<OneButton>().SetBlack();
                    }
                    answerString += (i + 1).ToString();
                }

                buttons[i].GetComponent<OneButton>().ResetClick();
            }
        }
        if (!useKetteiButton)
        {
            for (int i = 0; i < answerString.Length; i++)
            {
                int pos = System.Int32.Parse(answerString.Substring(i, 1));
                buttons[pos - 1].GetComponent<OneButton>().SetNumber((i + 1).ToString());
            }

            if (answerString.Length >= kazu)
                return answerString;
        }
        else
        {
            var color = buttons[max].GetComponentInChildren<Text>().color;
            color.a = 1.0f;
            buttons[max].GetComponentInChildren<Text>().color = color;

            if (buttons[max].GetComponent<OneButton>().onClick)
            {
                if (answerString.Length == 0)
                {
                    return "決定";
                }
                string as2 = "";
                for (int i = 0; i <= max; i++)
                {
                    if (answerString.IndexOf((i + 1).ToString()) >= 0)
                    {
                        as2 += (i + 1).ToString();
                    }
                }
                return as2 + "決定";
            }
        }
        return "";
    }

    // ==========================================================
    // 線つなぎ用
    // ==========================================================
    public string ButtonCheckS(int kazu)
    {
        if (!sentsunagi) return "";
        if (!isMultiCheck) return "";
        if (buttons == null) return "";
        if (useKetteiButton) return "";

        int max = buttons.Count;

        for (int i = 0; i < max; i++)
        {
            var color = buttons[i].GetComponentInChildren<Text>().color;
            color.a = 1.0f;
            buttons[i].GetComponentInChildren<Text>().color = color;

            color = buttons[i].GetComponentInChildren<Image>().color;
            color.a = 1.0f;
            buttons[i].GetComponentInChildren<Image>().color = color;

            // 今が右か左か
            bool nowButtonIsLeft = i < (max / 2);

            if (buttons[i].GetComponent<OneButton>().onClick)
            {
                string numStr = (i).ToString();

                // 何か一つを選択中かどうか
                bool oneChoose = false;
                if (answerString.Length > 0)
                    if (answerString.Length % 2 == 1)
                        oneChoose = true;

                // ひとつ選択中
                if (oneChoose)
                {
                    int lastButton = System.Int32.Parse(answerString.Substring(answerString.Length - 1)); // 最後に押したボタン番号
                    bool del = false;

                    del = (lastButton == i); // 同じだったらリセット
                    del = del | ((nowButtonIsLeft) && (lastButton < (max / 2))); // 選択してる方の物を押してたらリセット
                    del = del | ((!nowButtonIsLeft) && (lastButton >= (max / 2))); // 選択してる方の物を押してたらリセット

                    if (lastButton != i)
                    {
                        // 既にいた場合、もといたものを取っ払う
                        if (answerString.Contains(numStr))
                        {
                            int ntrIndex = answerString.IndexOf(numStr);
                            int remInd = -1;
                            if (ntrIndex % 2 == 0)
                            {
                                remInd = System.Int32.Parse(answerString.Substring(ntrIndex + 1, 1));
                                buttons[remInd].GetComponent<OneButton>().ResetNumber();
                                answerString = answerString.Remove(ntrIndex, 2);
                            }
                            else
                            {
                                remInd = System.Int32.Parse(answerString.Substring(ntrIndex - 1, 1));
                                buttons[remInd].GetComponent<OneButton>().ResetNumber();
                                answerString = answerString.Remove(ntrIndex - 1, 2);
                            }

                            if (remInd < (max / 2))
                            {
                                lines[remInd].SetPosition(0, Vector3.one * 500);
                                lines[remInd].SetPosition(1, Vector3.one * 500);
                            }
                            if (i < (max / 2))
                            {
                                lines[i].SetPosition(0, Vector3.one * 500);
                                lines[i].SetPosition(1, Vector3.one * 500);
                            }
                        }
                    }

                    if (del)
                    {
                        buttons[i].GetComponent<OneButton>().ResetNumber();
                        answerString = answerString.Substring(0, answerString.Length - 1);
                        buttons[lastButton].GetComponent<OneButton>().ResetBlack();

                        if (lastButton != i)
                        {
                            answerString += numStr;
                            buttons[i].GetComponent<OneButton>().SetBlack();
                        }
                    }
                    else
                    {
                        // 左右セット成立
                        answerString += (i).ToString();
                        int left = -1;
                        int right = -1;
                        if (lastButton < i)
                        {
                            left = lastButton;
                            right = i;
                        }
                        else
                        {
                            left = i;
                            right = lastButton;
                        }
                        // 線とか引く笑
                        {
                            buttons[left].GetComponent<OneButton>().SetNumber((right - (max / 2) + 1).ToString());
                            buttons[right].GetComponent<OneButton>().SetBlack();


                            float buttonSize = 3.0f;

                            var lPos = buttons[left].transform.position;
                            lPos.x = lPos.x + buttonSize;
                            lines[left].SetPosition(0, lPos);

                            var rPos = buttons[right].transform.position;
                            rPos.x = rPos.x - buttonSize;
                            lines[left].SetPosition(1, rPos);
                        }
                    }
                }
                else
                {
                    // 既にいた場合、もといたものを取っ払う
                    if (answerString.Contains(numStr))
                    {
                        int ntrIndex = answerString.IndexOf(numStr);
                        int remInd = -1;
                        if (ntrIndex % 2 == 0)
                        {
                            remInd = System.Int32.Parse(answerString.Substring(ntrIndex + 1, 1));
                            answerString = answerString.Remove(ntrIndex, 2);
                        }
                        else
                        {
                            remInd = System.Int32.Parse(answerString.Substring(ntrIndex - 1, 1));
                            answerString = answerString.Remove(ntrIndex - 1, 2);
                        }
                        buttons[remInd].GetComponent<OneButton>().ResetNumber();
                        buttons[remInd].GetComponent<OneButton>().ResetBlack();

                        if (remInd < (max / 2))
                        {
                            lines[remInd].SetPosition(0, Vector3.one * 500);
                            lines[remInd].SetPosition(1, Vector3.one * 500);
                        }
                        if (i < (max / 2))
                        {
                            lines[i].SetPosition(0, Vector3.one * 500);
                            lines[i].SetPosition(1, Vector3.one * 500);
                        }
                    }

                    

                    answerString += (i).ToString();
                    buttons[i].GetComponent<OneButton>().SetBlack();
                }

                buttons[i].GetComponent<OneButton>().ResetClick();
            }
        }
        if (!useKetteiButton)
        {
            for (int i = 0; i < answerString.Length; i++)
            {
                int pos = System.Int32.Parse(answerString.Substring(i, 1));
            }

            if (answerString.Length >= kazu)
                return answerString;
        }
        return "";
    }

    // ==========================================================
    // ボタンを消滅させる
    // ==========================================================
    public void ButtonClear()
    {
        isMultiCheck = false;
        answerString = "";
        if (buttons != null)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                Destroy(buttons[i]);
            }
            buttons.Clear();
        }
        if (lines != null)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Destroy(lines[i]);
            }
            lines.Clear();
        }
    }
}
