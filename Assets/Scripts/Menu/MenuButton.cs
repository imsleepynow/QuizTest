using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public string naviText;
    public Text naviField;

    public void OnPointerEnter()
    {
        if (naviField != null)
            naviField.text = naviText.Replace("\\n","\n");
    }

    public void OnPointerExit()
    {
        if (naviField != null)
            naviField.text = "";
    }

}
