using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeikaiMoji : MonoBehaviour
{
    public Text mojiText;
    public RectTransform rt;

    bool answer = false;

    public void MojiDisappear()
    {
        var textList = this.GetComponentsInChildren<Text>();
        for (int i = 0; i < textList.Length; i++)
        {
            this.GetComponentsInChildren<Text>()[i].enabled = false;
            this.GetComponentsInChildren<Text>()[i].text = "";
        }
        rt.localScale = Vector3.one;
    }

    public void MojiAppear(QuizManager.AnswerType answerType)
    {
        if (mojiText.enabled == false)
        {
            mojiText.enabled = true;
            if (answerType == QuizManager.AnswerType.Answer)
            {
                mojiText.text = "ê≥â";
                mojiText.color = new Color(1, 0, 0, 1);
                answer = true;
            }
            else if (answerType == QuizManager.AnswerType.Fuseikai)
            {
                mojiText.text = "ïsê≥â";
                mojiText.color = new Color(0, 0, 1, 1);
                answer = false;
            }
            else if (answerType == QuizManager.AnswerType.NanmonSeikai)
            {
                mojiText.text = "ìÔñ‚ê≥â";
                mojiText.color = new Color(1, 0, 0, 1);
                answer = true;
            }
            else if (answerType == QuizManager.AnswerType.Jikangire)
            {
                mojiText.text = "éûä‘êÿÇÍ";
                mojiText.color = new Color(0, 0, 1, 1);
                answer = false;
            }
        }
    }

    public void MojiAppear(string text, Color color, bool move)
    {
        mojiText.enabled = true;
        mojiText.text = text;
        mojiText.color = color;
        answer = move;
    }

    public void MojiMove(float time, float maxTime)
    {
        if (answer)
        {
            float sizeUp = (maxTime / 2 - time) * 1.5f;
            if (sizeUp < 0.0f) sizeUp = 0.0f;
            else sizeUp = sizeUp * sizeUp * sizeUp * sizeUp * sizeUp / 10.0f;
            rt.localScale = Vector3.one * (1.0f + (sizeUp));

            float alpha = 1.0f - sizeUp;
            if (alpha < 0.0f) alpha = 0.0f;
            Color motoColor = mojiText.color;
            mojiText.color = new Color(motoColor.r, motoColor.g, motoColor.b, alpha);
        }
    }
}
