using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public GameObject loadingImg;

    public SpreadSheetCheck spreadSheetCheck;
    static Record record;
    public InputField nameInput;

    public GameObject warning;
    public GameObject letsgo;

    public Text text;

    bool isFirst = true;

    enum Scene
    {
        LOADING,
        TITLE,
        ALART,
        MOVEOK,
        MOVING,
        NONE
    }

    Scene scene = Scene.LOADING;

    void Start()
    {
        isFirst = true;
        if (SpreadSheetCheck.readedRecordData.id != -1)
        {
            isFirst = false;
            warning.SetActive(false);
            loadingImg.SetActive(false);
            nameInput.text = record.name;

            scene = Scene.TITLE;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (scene)
        {
            case Scene.LOADING:
                Loading();
                break;
            case Scene.TITLE:
                break;
            case Scene.ALART:
                Alart();
                break;
            case Scene.MOVEOK:
                MoveOK();
                break;
            case Scene.MOVING:
                break;
            default:
                break;
        }
    }

    void Loading()
    {
        if (isFirst == true)
        {
            string getId = spreadSheetCheck.GetOrCreateUserId();
            StartCoroutine(spreadSheetCheck.ReadDataSheet(getId));// UnityEngine.SystemInfo.deviceUniqueIdentifier));
            StartCoroutine(spreadSheetCheck.ReadLibrary());
            isFirst = false;
        }
        record = SpreadSheetCheck.readedRecordData;


        if ((loadingImg.activeSelf) && (record.address != ""))
        {
            loadingImg.SetActive(false);
            nameInput.text = record.name;

            scene = Scene.TITLE;
        }
    }

    public void MoveNextScene()
    {
        scene = Scene.ALART;
        AlartFirst();
    }

    void AlartFirst()
    {
        warning.SetActive(true);

        if (SpreadSheetCheck.readedRecordData.id == -1)
        {
            int zukaiFirstOpen = Random.Range(0, SpreadSheetCheck.readedLibraryData.Count);
            spreadSheetCheck.ZukanOpenFromAll(zukaiFirstOpen);
            spreadSheetCheck.ZukanToRecord();
        }
        StartCoroutine(spreadSheetCheck.PostDataData(SpreadSheetCheck.userId, nameInput.text));
    }

    void Alart()
    {
        warning.SetActive(true);

        if (spreadSheetCheck.postedData)
        {
            scene = Scene.MOVEOK;
        }
    }

    void MoveOK()
    {
        letsgo.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
