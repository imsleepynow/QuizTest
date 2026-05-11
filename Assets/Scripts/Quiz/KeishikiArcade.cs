using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeishikiArcade : MonoBehaviour
{
    public QuizManager quizManager;

    public string kName = "アーケードモード";


    public int maxMondai = 30;
    int nowMondai = 0;
    public SeikaiMoji mojiImage;

    public float allStartTime = 300;
    public float allResultTime = 300;
    float nowTime = 0;

    public GameObject maruBatsu;
    public GameObject seigo;
    List<MaruBatsu> maruBatsuList = new List<MaruBatsu>();

    int point = 0;
    int nextPoint = 0;
    public Text pointText;

    Keishiki sheetId = Keishiki.None;

    public SoundManager soundManager;

    enum Timing
    {
        Wait,
        AllStart,
        Quizing,
        AllResultWait,
        AllEnd
    }

    Timing nowTiming = Timing.Wait;

    public void GetStartTiming ()
    {
        nowTiming = Timing.AllStart;
        mojiImage.GetComponentsInChildren<Text>()[1].text = "";
    }

    public bool Update2(bool hasData)
    {
        // ゲーム終了時にfalseを返す
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // デバッグ用ページ戻り　最後に消すべし
            soundManager.StopMusic();
            return false;
        }
        // ゲーム終了時にfalseを返す
        if (Input.GetKeyDown(KeyCode.V))
        {
            nowTiming = Timing.AllEnd;
        }

        if (nowTiming == Timing.AllStart)
        {
            nowTime += Time.deltaTime;
            //quizManager.buttonManager.ButtonClear
            // クイズスタートの合図
            AllStart(); 
        }
        else if (nowTiming == Timing.Quizing)
        {
            if (quizManager.IsFree())
            {
                if (nowMondai >= maxMondai)
                {
                    nowTiming = Timing.AllResultWait;
                }
                else if (hasData)
                {
                    sheetId = (Keishiki)UnityEngine.Random.Range(0, SpreadSheetCheck.mondaiIdList.Length);
                    quizManager.addRecordData.keishiki = sheetId;
                    if (sheetId == Keishiki.Typing && SpreadSheetCheck.readedTypingData != null)
                    {
                        var list = SpreadSheetCheck.readedTypingData;
                        //if (list.Count > 0)
                        //{
                        //    list.Clear();
                        //}
                        int index = UnityEngine.Random.Range(0, list.Count);
                        quizManager.StartQuizTyping(list[index].mondaibun, list[index].answer, list[index].type, list[index].nanido);

                        quizManager.addRecordData.id = index;
                        quizManager.addRecordData.series = list[index].series;
                        quizManager.addRecordData.nanido = list[index].nanido;
                        quizManager.addRecordData.looked = false;
                        quizManager.addRecordData.firstCollect = false;

                        nowMondai++;
                    }
                    else if (sheetId == Keishiki.Yontaku && SpreadSheetCheck.readedYontakuData != null)
                    {
                        var list = SpreadSheetCheck.readedYontakuData;
                        //if (list.Count > 0)
                        //{
                        //    list.Clear();
                        //}
                        int index = UnityEngine.Random.Range(0, list.Count);
                        quizManager.StartQuizYontaku(list[index].mondaibun, list[index].answer, list[index].dummy, list[index].nanido);

                        quizManager.addRecordData.id = index;
                        quizManager.addRecordData.series = list[index].series;
                        quizManager.addRecordData.nanido = list[index].nanido;
                        quizManager.addRecordData.looked = false;
                        quizManager.addRecordData.firstCollect = false;

                        nowMondai++;
                    }
                    else if (sheetId == Keishiki.Rensou4 && SpreadSheetCheck.readedRensou4Data != null)
                    {
                        var list = SpreadSheetCheck.readedRensou4Data;
                        int index = UnityEngine.Random.Range(0, list.Count);
                        quizManager.StartQuizRensou4(list[index].mondaibun, list[index].answer, list[index].dummy, list[index].nanido);

                        quizManager.addRecordData.id = index;
                        quizManager.addRecordData.series = list[index].series;
                        quizManager.addRecordData.nanido = list[index].nanido;
                        quizManager.addRecordData.looked = false;
                        quizManager.addRecordData.firstCollect = false;

                        nowMondai++;
                    }
                    else if (sheetId == Keishiki.Marubatsu && SpreadSheetCheck.readedMarubatsuData != null)
                    {
                        var list = SpreadSheetCheck.readedMarubatsuData;
                        int index = UnityEngine.Random.Range(0, list.Count);
                        quizManager.StartQuizMarubatsu(list[index].mondaibun, list[index].answer, list[index].nanido);

                        quizManager.addRecordData.id = index;
                        quizManager.addRecordData.series = list[index].series;
                        quizManager.addRecordData.nanido = list[index].nanido;
                        quizManager.addRecordData.looked = false;
                        quizManager.addRecordData.firstCollect = false;

                        nowMondai++;
                    }
                    else if (sheetId == Keishiki.Nitaku && SpreadSheetCheck.readedNitakuData != null)
                    {
                        var list = SpreadSheetCheck.readedNitakuData;
                        int index = UnityEngine.Random.Range(0, list.Count);
                        quizManager.StartQuizNitaku(list[index].mondaibun, list[index].answer, list[index].dummy, list[index].nanido);

                        quizManager.addRecordData.id = index;
                        quizManager.addRecordData.series = list[index].series;
                        quizManager.addRecordData.nanido = list[index].nanido;
                        quizManager.addRecordData.looked = false;
                        quizManager.addRecordData.firstCollect = false;

                        nowMondai++;
                    }
                }
                else
                {
                    Debug.Log("問題を入れ込んでねえよい");
                }
            }
            else
            {
                // 正誤判定
                var answer = quizManager.GetAnwserType();
                if ((answer == QuizManager.AnswerType.Answer) ||
                    (answer == QuizManager.AnswerType.NanmonSeikai))
                {
                    SetCollect(true);
                    nextPoint = (int)quizManager.GetPoint();
                    point += nextPoint;
                }
                else if ((answer == QuizManager.AnswerType.Fuseikai) ||
                        (answer == QuizManager.AnswerType.Jikangire))
                {
                    SetCollect(false);
                }

                // 得点表示
                if (nextPoint != 0)
                {
                    float textPoint = float.Parse(pointText.text);
                    textPoint += (int)(nextPoint / 90.0f);
                    if (textPoint > point)
                    {
                        textPoint = point;
                        nextPoint = 0;
                    }
                    pointText.text = textPoint.ToString();
                }
            }
        }
        else if (nowTiming == Timing.AllResultWait)
        {
            nowTime += Time.deltaTime;
            AllResult();
        }
        else if (nowTiming == Timing.AllEnd)
        {
            if (Input.GetMouseButton(0))
            {
                soundManager.StopMusic();
                return false;
            }
        }
        return true;
    }

    void AllStart()
    {
        if (nowTime >= 1)
        {
            if (!soundManager.IsNowPlaying())
            {
                soundManager.PlayMusic(2);
            }
        }

        mojiImage.MojiAppear(kName, new Color(1, 0.9f, 0.5f, 1), true);
        mojiImage.MojiMove(nowTime, allStartTime / 60.0f);

        if (nowTime * 60.0f >= allStartTime)
        {
            nowTiming = Timing.Quizing;
            nowTime = 0.0f;
            mojiImage.MojiDisappear();
        }

        if (nowTime == 0)
        {
            float size = maruBatsu.GetComponent<RectTransform>().rect.width;
            for (int i = 0; i < maxMondai; i++)
            {
                var pos = seigo.transform.position;
                pos.x += -size * (maxMondai / 2) + size * i;
                var obj = Instantiate(maruBatsu, pos, Quaternion.identity);
                obj.GetComponent<MaruBatsu>().Clear();
                obj.transform.SetParent(seigo.transform);
                maruBatsuList.Add(obj.GetComponent<MaruBatsu>());
            }
        }
    }

    void AllResult()
    {
        // ボタン非表示
        quizManager.buttonManager.ButtonClear();
        quizManager.typeButtonManager.ButtonClear();

        // 最後の問題の正解ポイントを表示したマックスポイントに更新
        quizManager.MaxPointUpdate();

        int rank = GetRank();
        string bigMoji = "";
        string smallMoji = "";
        Color mojiColor = new Color(0, 0, 0, 1);

        switch (rank)
        {
            case 0:
                bigMoji = "ERROR 0";
                smallMoji = "何か内部でミスがあるようです";
                break;
            case 1:
                bigMoji = "Cランク";
                smallMoji = "まだまだですね";
                break;
            case 2:
                bigMoji = "Bランク";
                smallMoji = "くわしいですね！";
                break;
            case 3:
                bigMoji = "Aランク";
                smallMoji = "相当手広くやりこんでますね！";
                break;
            case 4:
                bigMoji = "Sランク";
                smallMoji = "賢人現る！とんでもない把握力だ！！";
                break;
            case 5:
                bigMoji = "SSランク";
                smallMoji = "ありえんありえんありえん\n何者！？連絡ください";
                break;
            default:
                bigMoji = "ERROR 1";
                smallMoji = "何か内部でミスがあるようです";
                break;
        }

        mojiImage.MojiAppear(bigMoji, new Color(0,0,0, 1), false);
        mojiImage.MojiMove(nowTime, allStartTime / 60.0f);
        mojiImage.GetComponentsInChildren<Text>()[1].text = smallMoji;
        mojiImage.GetComponentsInChildren<Text>()[1].color = mojiColor;
        if (nowTime * 60.0f >= allResultTime)
        {
            nowTiming = Timing.AllEnd;
            nowTime = 0.0f;
        }
    }

    private int GetRank()
    {
        string maxPointStr = SpreadSheetCheck.readedRecordData.maxAcademyMode;
        int maxPoint = 0;
        int retLank = 1; // 0:エラー 1～5 = C,B,A,S,SS

        if (maxPointStr == "")
            return 0;
        else
            maxPoint = System.Int32.Parse(maxPointStr);

        if (maxPoint > 6000)
            retLank = 5;
        else if (maxPoint > 4000)
            retLank = 4;
        else if (maxPoint > 3000)
            retLank = 3;
        else if (maxPoint > 2000)
            retLank = 2;

        return retLank;
    }

    public void SetCollect(bool collect)
    {
        maruBatsuList[nowMondai - 1].IsCollect(collect);
    }

    public int GetPoint()
    {
        return point;
    }
}
