using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneButton : MonoBehaviour
{
    public GameObject number;
    public GameObject choose;
    public bool onClick = false;
    private bool isJunban = false;

    public void SetJunbanFlag(bool flag=true)
    {
        isJunban = flag;
    }

    public void OnClick()
    {
        onClick = true;
    }

    public void ResetClick()
    {
        onClick = false;
    }

    public void SetNumber(int num)
    {
        if (number != null)
        {
            number.active = true;
            var text = number.GetComponentInChildren<Text>();
            text.text = num.ToString();
        }
        if (choose != null)
        {
            choose.active = false;
        }
    }

    public void SetNumber(string num)
    {
        if (number != null)
        {
            number.active = true;
            var text = number.GetComponentInChildren<Text>();
            text.text = num;
        }
        if (choose != null)
        {
            choose.active = false;
        }
    }

    public void ResetNumber()
    {
        if (number != null)
        {
            number.active = false;
            number.gameObject.active = false;
        }
        if (choose != null)
        {
            choose.active = false;
            choose.gameObject.active = false;
        }
    }

    public void SetActive(bool active)
    {
        if (number != null)
            number.active = active;
        if (choose != null)
            choose.active = false;
    }

    public void SetBlack()
    {
        if (choose != null)
        {
            choose.active = true;
            choose.gameObject.active = true;
        }
        if (number != null)
            number.active = false;
    }

    public void ResetBlack()
    {
        if (choose != null)
        {
            choose.active = false;
            choose.gameObject.active = false;
        }
        if (number != null)
            number.active = false;
    }
}
