using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectScreen : MonoBehaviour
{
    //int now_choose = 0;
    public GameObject buttonPrefab;
    public SpreadSheetCheck spreadSheetCheck;
    public GameObject content;


    public GameObject selectScreen;
    public GameObject zukanScreen;

    public ZukanManager zukanManager;
    public SoundTestManager soundTestManager;
    public GameObject sortTypeText;

    bool makeButton = false;

    public bool debug = false;

    int choosePage = 0;

    List<GameObject> buttonList = new List<GameObject>();

    public GameObject leftButton;
    public GameObject rightButton;

    bool isZukan = true;

    enum SortType
    {
        ReleaseDate,
        Series,
    }
    private SortType sortType = SortType.ReleaseDate;
    private List<int> defaultIndexList = new List<int>();
    private List<int> seriesIndexList = new List<int>();

    private List<string> testQuizList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        if ((zukanManager != null) && (soundTestManager != null))
        {
            Debug.Log("どっちもいれちゃだめでしょ～");
            return;
        }
        isZukan = (zukanManager != null);

        if (debug)
        {
            for (int i = 0; i < SpreadSheetCheck.mondaiIdList.Length; i++)
            {
                StartCoroutine(spreadSheetCheck.ReadSheet(i));
            }
        }
        else
        {
            //if (isZukan) StartCoroutine(spreadSheetCheck.ReadLibrary());
            //else StartCoroutine(spreadSheetCheck.ReadSoundTest());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spreadSheetCheck.hasData)
        {
            spreadSheetCheck.RecordInitialize();
        }

        int bunLength = 20;
        if (!makeButton)
        {
            SetZukanNum();
            if (debug)
            {
                if (spreadSheetCheck.hasData)
                {
                    if (spreadSheetCheck.hasRecordData)
                    {
                        for (int h = 0; h < SpreadSheetCheck.recordDataBase.Count; h++)
                        {
                            for (int i = 0; i < SpreadSheetCheck.recordDataBase[h].Count; i++)
                            {
                                if (!SpreadSheetCheck.recordDataBase[h][i].looked)
                                    continue;

                                var button = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, content.transform);
                                string bun = "";
                                switch ((Keishiki)h)
                                {
                                    case Keishiki.Yontaku:
                                        bun = SpreadSheetCheck.readedYontakuData[i].mondaibun;
                                        break;
                                    case Keishiki.Marubatsu:
                                        bun = SpreadSheetCheck.readedMarubatsuData[i].mondaibun;
                                        break;
                                    case Keishiki.Rensou4:
                                        bun = SpreadSheetCheck.readedRensou4Data[i].mondaibun;
                                        break;
                                    case Keishiki.Sentsunagi:
                                        bun = SpreadSheetCheck.readedSentsunagiData[i].mondaibun;
                                        break;
                                    case Keishiki.Typing:
                                        bun = SpreadSheetCheck.readedTypingData[i].mondaibun;
                                        break;
                                    case Keishiki.Tato:
                                        bun = SpreadSheetCheck.readedTatoData[i].mondaibun;
                                        break;
                                    case Keishiki.Nitaku:
                                        bun = SpreadSheetCheck.readedNitakuData[i].mondaibun;
                                        break;
                                    case Keishiki.Junban:
                                        bun = SpreadSheetCheck.readedJunbanData[i].mondaibun;
                                        break;
                                    default:
                                        break;
                                }

                                if (bun.Length < bunLength)
                                    button.GetComponentInChildren<Text>().text = bun;
                                else
                                    button.GetComponentInChildren<Text>().text = bun.Substring(0, bunLength) + "…";

                                buttonList.Add(button);
                                testQuizList.Add(h.ToString() + "," + i.ToString() + "," + bun);
                            }
                            makeButton = true;
                        }
                    }
                }
            }
            else
            {
                if ((isZukan) && (SpreadSheetCheck.hasLibraryData))
                {
                    if (sortType == SortType.ReleaseDate)
                    {
                        for (int i = 0; i < SpreadSheetCheck.readedLibraryData.Count; i++)
                        {
                            if (SpreadSheetCheck.readedLibraryData[i].open)
                            {
                                var button = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, content.transform);
                                string bun = SpreadSheetCheck.readedLibraryData[i].name;

                                //Debug.Log(bun);
                                //Debug.Log(bun.IndexOf("\\"));
                                bun = bun.Replace("\n", " ");
                                bun = bun.Replace("\\n", " ");
                                bun = bun.Replace("\\", " ");

                                if (bun.Length < bunLength)
                                    button.GetComponentInChildren<Text>().text = bun;
                                else
                                    button.GetComponentInChildren<Text>().text = bun.Substring(0, bunLength) + "…";

                                //button.GetComponentInChildren<Text>().text = bun;
                                button.GetComponent<RectTransform>().sizeDelta = new Vector2(860, 60);
                                buttonList.Add(button);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < seriesIndexList.Count; i++)
                        {
                            int index = seriesIndexList[i];
                            if (SpreadSheetCheck.readedLibraryData[index].open)
                            {
                                var button = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, content.transform);
                                string bun = SpreadSheetCheck.readedLibraryData[index].name;

                                //Debug.Log(bun);
                                //Debug.Log(bun.IndexOf("\\"));
                                bun = bun.Replace("\n", " ");
                                bun = bun.Replace("\\n", " ");
                                bun = bun.Replace("\\", " ");

                                if (bun.Length < bunLength)
                                    button.GetComponentInChildren<Text>().text = bun;
                                else
                                    button.GetComponentInChildren<Text>().text = bun.Substring(0, bunLength) + "…";

                                //button.GetComponentInChildren<Text>().text = bun;
                                button.GetComponent<RectTransform>().sizeDelta = new Vector2(860, 60);
                                buttonList.Add(button);
                            }
                        }
                    }
                    makeButton = true;
                }
                else if ((!isZukan) && (spreadSheetCheck.hasSoundTestData) && (SpreadSheetCheck.readedSoundTestData.Count == soundTestManager.GetMusicCount()))
                {
                    for (int i = 0; i < SpreadSheetCheck.readedSoundTestData.Count; i++)
                    {
                        var button = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, content.transform);
                        string bun = SpreadSheetCheck.readedSoundTestData[i].name;

                        //Debug.Log(bun);
                        //Debug.Log(bun.IndexOf("\\"));
                        bun = bun.Replace("\n", " ");
                        bun = bun.Replace("\\n", " ");
                        bun = bun.Replace("\\", " ");

                        if (bun.Length < bunLength)
                            button.GetComponentInChildren<Text>().text = bun;
                        else
                            button.GetComponentInChildren<Text>().text = bun.Substring(0, bunLength) + "…";

                        //button.GetComponentInChildren<Text>().text = bun;
                        button.GetComponent<RectTransform>().sizeDelta = new Vector2(860, 60);
                        buttonList.Add(button);
                    }
                    makeButton = true;
                }
            }
        }
        else
        {
            for (int i=0;i< buttonList.Count;i++)
            {
                if (buttonList[i].GetComponent<OneButton>().onClick)
                {
                    selectScreen.active = false;
                    zukanScreen.active = true;
                    if (sortTypeText != null)
                        sortTypeText.active = false;
                    buttonList[i].GetComponent<OneButton>().ResetClick();

                    if (debug)
                    {
                        var ids = testQuizList[i].Split(',');
                        int xh = int.Parse(ids[0]);
                        int xi = int.Parse(ids[1]);
                        ToRecordData record = SpreadSheetCheck.recordDataBase[xh][xi];
                        string title = "年表に未登録の作品から出題";
                        if ((record.product >= 0) && (record.product < SpreadSheetCheck.readedNenpyoData.Count))
                            title = SpreadSheetCheck.readedNenpyoData[record.product - 1];
                        zukanManager.UpdateTestQuiz(record, ids[2], title);
                    }
                    else
                    {
                        if (isZukan)
                        {
                            if (sortType == SortType.ReleaseDate) zukanManager.UpdateLibrary(SpreadSheetCheck.readedLibraryData[defaultIndexList[i]]);
                            else if (sortType == SortType.Series) zukanManager.UpdateLibrary(SpreadSheetCheck.readedLibraryData[seriesIndexList[i]]);
                        }
                        else soundTestManager.UpdateSoundTest(SpreadSheetCheck.readedSoundTestData[i], i);
                    }
                    choosePage = i;

                }
            }
        }

        leftButton.active = (choosePage != 0);
        if (isZukan)
            rightButton.active = (choosePage < defaultIndexList.Count - 1);
        else
            rightButton.active = (choosePage < SpreadSheetCheck.readedSoundTestData.Count - 1);
    }
    public void PushBackBefore()
    {
        if (selectScreen.active)
        {
            if (!isZukan)
            {
                soundTestManager.DeleteMusic();
            }
            SceneManager.LoadScene("MenuScene");
        }
        else
        {
            selectScreen.active = true;
            zukanScreen.active = false;

            if (sortTypeText != null)
                sortTypeText.active = true;
        }
    }

    public void SortTypeChange()
    {
        sortType = (SortType)((((int)sortType) + 1) % Enum.GetValues(typeof(SortType)).Length);
        if (sortType == SortType.ReleaseDate)
        {
            sortTypeText.GetComponentInChildren<Text>().text = "シリーズ順に\n見る";
        }
        if (sortType == SortType.Series)
        {
            if (isZukan)
            {
                sortTypeText.GetComponentInChildren<Text>().text = "発売日順に\n見る";
            }
        }
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                GameObject.Destroy(buttonList[i]);
            }
            buttonList.Clear();
            makeButton = false;
        }
    }

    public void SetZukanNum()
    {
        
            if (defaultIndexList.Count == 0)
            {
                if ((isZukan) && (SpreadSheetCheck.hasLibraryData))
                {
                    for (int i = 0; i < SpreadSheetCheck.readedLibraryData.Count; i++)
                    {
                        if (SpreadSheetCheck.readedLibraryData[i].open)
                        {
                            defaultIndexList.Add(i);
                        }
                    }
                }
            }
        if (seriesIndexList.Count == 0)
        {
            if ((isZukan) && (SpreadSheetCheck.hasLibraryData))
            {
                int libraryMax = SpreadSheetCheck.readedLibraryData.Count;
                for (int j = 0; j < Enum.GetValues(typeof(Series)).Length; j++)
                {
                    int soroSeriesCount = 0;
                    for (int i = 0; i < libraryMax; i++)
                    {
                        if (SpreadSheetCheck.readedLibraryData[i].series == (Series)j)
                        {
                            if (SpreadSheetCheck.readedLibraryData[i].open)
                            {
                                seriesIndexList.Add(i);
                                soroSeriesCount++;
                            }
                        }
                    }
                }
            }
        }
    }

    public void PushBeforePage()
    {
        if (!selectScreen.active)
        {
            if (choosePage != 0)
            {
                if (isZukan)
                {
                    choosePage--;
                    if (sortType == SortType.ReleaseDate) zukanManager.UpdateLibrary(SpreadSheetCheck.readedLibraryData[defaultIndexList[choosePage]]);
                    else if (sortType == SortType.Series) zukanManager.UpdateLibrary(SpreadSheetCheck.readedLibraryData[seriesIndexList[choosePage]]);
                }
                else
                {
                    choosePage--;
                    soundTestManager.UpdateSoundTest(SpreadSheetCheck.readedSoundTestData[choosePage], choosePage);
                }
            }
        }
    }
    public void PushNextPage()
    {
        if (!selectScreen.active)
        {
            if (isZukan)
            {
                if (choosePage < defaultIndexList.Count - 1)
                {
                    choosePage++;
                    if (sortType == SortType.ReleaseDate) zukanManager.UpdateLibrary(SpreadSheetCheck.readedLibraryData[defaultIndexList[choosePage]]);
                    else if (sortType == SortType.Series) zukanManager.UpdateLibrary(SpreadSheetCheck.readedLibraryData[seriesIndexList[choosePage]]);
                }
            }
            else
            {
                if (choosePage < SpreadSheetCheck.readedSoundTestData.Count - 1)
                {
                    choosePage++;
                    soundTestManager.UpdateSoundTest(SpreadSheetCheck.readedSoundTestData[choosePage], choosePage);
                }
            }
        }
    }
}
