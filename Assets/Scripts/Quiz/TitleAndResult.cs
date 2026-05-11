using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAndResult : MonoBehaviour
{
    public Text mojiText;
    public RectTransform rt;

    bool answer = false;

    public void MojiDisappear()
    {
        mojiText.enabled = false;
        mojiText.text = "";
        rt.localScale = Vector3.one;
    }


    public void MojiAppear(string text, Color color)
    {
        mojiText.text = text;
        mojiText.color = color;
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
