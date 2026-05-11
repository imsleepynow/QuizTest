using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaruBatsu : MonoBehaviour
{
    public GameObject maru;
    public GameObject batsu;
    public GameObject heart;

    public void Clear()
    {
        maru.active = false;
        batsu.active = false;
        heart.active = false;
    }

    public void IsCollect(bool collect)
    {
        maru.active = collect;
        batsu.active = !collect;
        heart.active = false;
    }
    public void SetHeart()
    {
        heart.active = true;
        maru.active = false;
        batsu.active = false;
    }

    public float GetMaruBatsuSize()
    {
        return 25;// maru.GetComponent<RectTransform>().rect.width;
    }
    public float GetHeartSize()
    {
        return 80.0f;// heart.GetComponent<RectTransform>().rect.width;
    }
}
