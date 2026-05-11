using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeishikiParent : MonoBehaviour
{
    public QuizManager quizManager;

    private string kName = "大任天堂クイズ";
    public List<string> kNameList;

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
    int collectCount = 0;
    public Text normaText;

    Keishiki sheetId = Keishiki.None;

    //public SoundManager soundManager;

    private QuizRuleManager.QuizKeishiki nowRule = QuizRuleManager.QuizKeishiki.None;
    public int zankiMax = 3;
    private int nowZanki = 3;
    public List<int> arcadeNorma;
    private int arcadeLevel = 1;

    private List<int> kishutsuMondai = new List<int>();

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

    public bool Update2(bool hasData, QuizRuleManager.QuizKeishiki rule)
    {
        nowRule = rule;

        // ゲーム終了時にfalseを返す
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // デバッグ用ページ戻り　最後に消すべし
            //soundManager.StopMusic();
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
            switch (nowRule)
            {
                case QuizRuleManager.QuizKeishiki.All:
                    kName = kNameList[0];
                    normaText.text = "最高得点";
                    break;
                case QuizRuleManager.QuizKeishiki.Arcade:
                    kName = kNameList[1];
                    quizManager.NormaUpdate(arcadeNorma[arcadeLevel - 1].ToString());
                    normaText.text = "レベル" + (arcadeLevel).ToString() + " ノルマ";
                    break;
                default:
                    break;
            }
            AllStart(); 
        }
        else if (nowTiming == Timing.Quizing)
        {
            if (quizManager.IsFree())
            {
                SetQuiz(hasData);
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
                switch (nowRule)
                {
                    case QuizRuleManager.QuizKeishiki.All:
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
                        break;
                    case QuizRuleManager.QuizKeishiki.Arcade:
                        pointText.text = collectCount.ToString();
                        break;
                    default:
                        break;
                }
            }
        }
        else if (nowTiming == Timing.AllResultWait)
        {
            nowTime += Time.deltaTime;
            switch (nowRule)
            {
                case QuizRuleManager.QuizKeishiki.All:
                    AllResult();
                    break;
                case QuizRuleManager.QuizKeishiki.Arcade:
                    ArcadeResult();
                    break;
                default:
                    AllResult();
                    break;
            }
        }
        else if (nowTiming == Timing.AllEnd)
        {
            if (Input.GetMouseButton(0))
            {
                //soundManager.StopMusic();
                return false;
            }
        }
        return true;
    }

    void AllStart()
    {
        //if (nowTime >= 1)
        //{
        //    if (!soundManager.IsNowPlaying())
        //    {
        //        soundManager.PlayMusic(2);
        //    }
        //}

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
            if (nowRule == QuizRuleManager.QuizKeishiki.All)
            {
                float size = maruBatsu.GetComponent<MaruBatsu>().GetMaruBatsuSize();
                for (int i = 0; i < maxMondai; i++)
                {
                    var pos = seigo.transform.position;
                    pos.x += -size * (maxMondai / 2 - 0.5f) + size * i;
                    var pos2 = seigo.transform.TransformPoint(pos);
                    var obj = Instantiate(maruBatsu, pos2, Quaternion.identity);
                    obj.GetComponent<MaruBatsu>().Clear();
                    obj.transform.SetParent(seigo.transform);
                    obj.transform.localScale = Vector3.one;
                    maruBatsuList.Add(obj.GetComponent<MaruBatsu>());
                }
            }
            else if (nowRule == QuizRuleManager.QuizKeishiki.Arcade)
            {
                float size = maruBatsu.GetComponent<MaruBatsu>().GetHeartSize();
                for (int i = 0; i < zankiMax; i++)
                {
                    var pos = seigo.transform.position;
                    pos.x += -size * (zankiMax / 2) + size * i;
                    pos.y += size / 4;
                    var pos2 = seigo.transform.TransformPoint(pos);
                    var obj = Instantiate(maruBatsu, pos2, Quaternion.identity);
                    obj.GetComponent<MaruBatsu>().SetHeart();
                    obj.transform.SetParent(seigo.transform);
                    obj.transform.localScale = Vector3.one;
                    maruBatsuList.Add(obj.GetComponent<MaruBatsu>());
                }

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

        mojiImage.MojiAppear(bigMoji, new Color(0, 0, 0, 1), false);
        mojiImage.MojiMove(nowTime, allStartTime / 60.0f);
        mojiImage.GetComponentsInChildren<Text>()[1].enabled = true;
        mojiImage.GetComponentsInChildren<Text>()[1].text = smallMoji;
        mojiImage.GetComponentsInChildren<Text>()[1].color = mojiColor;
        if (nowTime * 60.0f >= allResultTime)
        {
            nowTiming = Timing.AllEnd;
            nowTime = 0.0f;
        }
    }

    void ArcadeResult()
    {
        bool isEnd = true;
        // ボタン非表示
        quizManager.buttonManager.ButtonClear();
        quizManager.typeButtonManager.ButtonClear();

        string bigMoji = "GAME OVER";
        string smallMoji = "ここの文章も正解数で変えることは可能…";
        Color mojiColor = new Color(0, 0, 0, 1);

        if (collectCount >= arcadeNorma[arcadeLevel - 1])
        {
            if (arcadeLevel >= 5)
            {
                if (arcadeLevel >= 6)
                {
                    bigMoji = "すごすぎ！";
                    smallMoji = "さすがにおお";
                    mojiColor = new Color(1, 0.8f, 0.2f, 1);
                }
                else if (nowZanki >= 3)
                {
                    // まだ続く場合

                    bigMoji = "LEVEL MAX!";
                    smallMoji = "つぎのノルマも" + arcadeNorma[arcadeLevel].ToString() + "問だ！";
                    mojiColor = new Color(0.9f, 0.3f, 0.1f, 1);

                    isEnd = false;
                }
                else
                {
                    bigMoji = "CLEARED";
                    smallMoji = "おお";
                    mojiColor = new Color(1, 0.8f, 0.2f, 1);
                }
            }
            else
            {
                // まだ続く場合

                bigMoji = "LEVEL UP!";
                smallMoji = "つぎのノルマは" + arcadeNorma[arcadeLevel].ToString() + "問だ！";
                mojiColor = new Color(1, 0.8f, 0.2f, 1);

                isEnd = false;
            }
        }
        else
        {
            if (arcadeLevel >= 6)
            {
                bigMoji = "NICE!!";
                smallMoji = "ここまで来るだけでも凄すぎる";
            }
            else
            {
                bigMoji = "GAME OVER";
                smallMoji = "がんばろう";
            }
        }

        mojiImage.MojiAppear(bigMoji, mojiColor, false);
        mojiImage.MojiMove(nowTime, allStartTime / 60.0f);
        mojiImage.GetComponentsInChildren<Text>()[1].enabled = true;
        mojiImage.GetComponentsInChildren<Text>()[1].text = smallMoji;
        mojiImage.GetComponentsInChildren<Text>()[1].color = mojiColor;
        if (nowTime * 60.0f >= allResultTime)

        {
            if (isEnd)
            {
                nowTiming = Timing.AllEnd;
                nowTime = 0.0f;
            }
            else
            {
                nowTiming = Timing.Quizing;
                arcadeLevel++;
                nowTime = 0.0f;
                mojiImage.MojiDisappear();
                quizManager.NormaUpdate(arcadeNorma[arcadeLevel - 1].ToString());
                normaText.text = "レベル" + (arcadeLevel).ToString() + " ノルマ";
                collectCount = 0;
            }
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
        if (collect)
            collectCount++;

        if (nowRule == QuizRuleManager.QuizKeishiki.All)
            maruBatsuList[nowMondai - 1].IsCollect(collect);
        else if (nowRule == QuizRuleManager.QuizKeishiki.Arcade)
        {
            if (!collect)
            {
                nowZanki--;
                maruBatsuList[nowZanki].Clear();
            }
        }
    }

    public int GetPoint()
    {
        return point;
    }

    void SetQuiz(bool hasData)
    {
        SetQuiz(hasData, 0, Keishiki.None);
    }

    void SetQuiz(bool hasData, int nanido = 0, Keishiki keishiki = Keishiki.None)
    {
        if ((nowRule == QuizRuleManager.QuizKeishiki.All) && (nowMondai >= maxMondai))
        {
            nowTiming = Timing.AllResultWait;
        }
        else if ((nowRule == QuizRuleManager.QuizKeishiki.Arcade) && (nowZanki == 0))
        {
            nowTiming = Timing.AllResultWait;
        }
        else if ((nowRule == QuizRuleManager.QuizKeishiki.Arcade) && (collectCount >= arcadeNorma[arcadeLevel - 1]))
        {
            nowTiming = Timing.AllResultWait;
        }
        else if (hasData)
        {

            sheetId = keishiki;
            RANDOMRESET:
            if (keishiki == Keishiki.None)
                sheetId = (Keishiki)UnityEngine.Random.Range(0, SpreadSheetCheck.mondaiIdList.Length);
            quizManager.addRecordData.keishiki = sheetId;
            if (sheetId == Keishiki.Typing && SpreadSheetCheck.readedTypingData != null)
            {
                var list = SpreadSheetCheck.readedTypingData;
                int index = UnityEngine.Random.Range(0, list.Count);

                if (nowRule == QuizRuleManager.QuizKeishiki.Arcade)
                {
                    int a = (int)(list[index].nanido / 10);
                    bool notResetRand = false;

                    if (arcadeLevel == 1)
                    {
                        notResetRand = ((list[index].nanido == arcadeLevel) || (list[index].nanido - a * 10 + a == arcadeLevel));
                    }
                    else if (arcadeLevel >= 2)
                    {
                        notResetRand = (((list[index].nanido >= arcadeLevel - 1) && (list[index].nanido <= arcadeLevel)) ||
                                        ((list[index].nanido - a * 10 + a >= arcadeLevel - 1) && (list[index].nanido - a * 10 + a <= arcadeLevel)));
                    }

                    if (!notResetRand)
                    {
                        goto RANDOMRESET;
                    }
                }

                quizManager.StartQuizTyping(list[index].mondaibun, list[index].answer, list[index].type, list[index].nanido);

                quizManager.addRecordData.id = index;
                quizManager.addRecordData.series = list[index].series;
                quizManager.addRecordData.nanido = list[index].nanido;
                quizManager.addRecordData.looked = false;
                quizManager.addRecordData.firstCollect = false;

                quizManager.ZukanOpen(System.Int32.Parse(list[index].sakuhin));

                nowMondai++;
            }
            else if (sheetId == Keishiki.Yontaku && SpreadSheetCheck.readedYontakuData != null)
            {
                var list = SpreadSheetCheck.readedYontakuData;
                int index = UnityEngine.Random.Range(0, list.Count);

                if (nowRule == QuizRuleManager.QuizKeishiki.Arcade)
                {
                    int a = (int)(list[index].nanido / 10);
                    bool notResetRand = false;

                    if (arcadeLevel == 1)
                    {
                        notResetRand = ((list[index].nanido == arcadeLevel) || (list[index].nanido - a * 10 + a == arcadeLevel));
                    }
                    else if (arcadeLevel >= 2)
                    {
                        notResetRand = (((list[index].nanido >= arcadeLevel - 1) && (list[index].nanido <= arcadeLevel)) ||
                                        ((list[index].nanido - a * 10 + a >= arcadeLevel - 1) && (list[index].nanido - a * 10 + a <= arcadeLevel)));
                    }

                    if (!notResetRand)
                    {
                        goto RANDOMRESET;
                    }
                }

                quizManager.StartQuizYontaku(list[index].mondaibun, list[index].answer, list[index].dummy, list[index].nanido);

                quizManager.addRecordData.id = index;
                quizManager.addRecordData.series = list[index].series;
                quizManager.addRecordData.nanido = list[index].nanido;
                quizManager.addRecordData.looked = false;
                quizManager.addRecordData.firstCollect = false;

                quizManager.ZukanOpen(System.Int32.Parse(list[index].sakuhin));

                nowMondai++;
            }
            else if (sheetId == Keishiki.Rensou4 && SpreadSheetCheck.readedRensou4Data != null)
            {
                var list = SpreadSheetCheck.readedRensou4Data;
                int index = UnityEngine.Random.Range(0, list.Count);

                if (nowRule == QuizRuleManager.QuizKeishiki.Arcade)
                {
                    int a = (int)(list[index].nanido / 10);
                    bool notResetRand = false;

                    if (arcadeLevel == 1)
                    {
                        notResetRand = ((list[index].nanido == arcadeLevel) || (list[index].nanido - a * 10 + a == arcadeLevel));
                    }
                    else if (arcadeLevel >= 2)
                    {
                        notResetRand = (((list[index].nanido >= arcadeLevel - 1) && (list[index].nanido <= arcadeLevel)) ||
                                        ((list[index].nanido - a * 10 + a >= arcadeLevel - 1) && (list[index].nanido - a * 10 + a <= arcadeLevel)));
                    }

                    if (!notResetRand)
                    {
                        goto RANDOMRESET;
                    }
                }

                quizManager.StartQuizRensou4(list[index].mondaibun, list[index].answer, list[index].dummy, list[index].nanido);

                quizManager.addRecordData.id = index;
                quizManager.addRecordData.series = list[index].series;
                quizManager.addRecordData.nanido = list[index].nanido;
                quizManager.addRecordData.looked = false;
                quizManager.addRecordData.firstCollect = false;

                quizManager.ZukanOpen(System.Int32.Parse(list[index].sakuhin));

                nowMondai++;
            }
            else if (sheetId == Keishiki.Marubatsu && SpreadSheetCheck.readedMarubatsuData != null)
            {
                var list = SpreadSheetCheck.readedMarubatsuData;
                int index = UnityEngine.Random.Range(0, list.Count);

                if (nowRule == QuizRuleManager.QuizKeishiki.Arcade)
                {
                    int a = (int)(list[index].nanido / 10);
                    bool notResetRand = false;

                    if (arcadeLevel == 1)
                    {
                        notResetRand = ((list[index].nanido == arcadeLevel) || (list[index].nanido - a * 10 + a == arcadeLevel));
                    }
                    else if (arcadeLevel >= 2)
                    {
                        notResetRand = (((list[index].nanido >= arcadeLevel - 1) && (list[index].nanido <= arcadeLevel)) ||
                                        ((list[index].nanido - a * 10 + a >= arcadeLevel - 1) && (list[index].nanido - a * 10 + a <= arcadeLevel)));
                    }

                    if (!notResetRand)
                    {
                        goto RANDOMRESET;
                    }
                }

                quizManager.StartQuizMarubatsu(list[index].mondaibun, list[index].answer, list[index].nanido);

                quizManager.addRecordData.id = index;
                quizManager.addRecordData.series = list[index].series;
                quizManager.addRecordData.nanido = list[index].nanido;
                quizManager.addRecordData.looked = false;
                quizManager.addRecordData.firstCollect = false;

                quizManager.ZukanOpen(System.Int32.Parse(list[index].sakuhin));

                nowMondai++;
            }
            else if (sheetId == Keishiki.Nitaku && SpreadSheetCheck.readedNitakuData != null)
            {
                var list = SpreadSheetCheck.readedNitakuData;
                int index = UnityEngine.Random.Range(0, list.Count);

                if (nowRule == QuizRuleManager.QuizKeishiki.Arcade)
                {
                    int a = (int)(list[index].nanido / 10);
                    bool notResetRand = false;

                    if (arcadeLevel == 1)
                    {
                        notResetRand = ((list[index].nanido == arcadeLevel) || (list[index].nanido - a * 10 + a == arcadeLevel));
                    }
                    else if (arcadeLevel >= 2)
                    {
                        notResetRand = (((list[index].nanido >= arcadeLevel - 1) && (list[index].nanido <= arcadeLevel)) ||
                                        ((list[index].nanido - a * 10 + a >= arcadeLevel - 1) && (list[index].nanido - a * 10 + a <= arcadeLevel)));
                    }

                    if (!notResetRand)
                    {
                        goto RANDOMRESET;
                    }
                }

                quizManager.StartQuizNitaku(list[index].mondaibun, list[index].answer, list[index].dummy, list[index].nanido);

                quizManager.addRecordData.id = index;
                quizManager.addRecordData.series = list[index].series;
                quizManager.addRecordData.nanido = list[index].nanido;
                quizManager.addRecordData.looked = false;
                quizManager.addRecordData.firstCollect = false;

                quizManager.ZukanOpen(System.Int32.Parse(list[index].sakuhin));

                nowMondai++;
            }
            else if (sheetId == Keishiki.Junban && SpreadSheetCheck.readedJunbanData != null)
            {
                var list = SpreadSheetCheck.readedJunbanData;
                int index = UnityEngine.Random.Range(0, list.Count);

                if (nowRule == QuizRuleManager.QuizKeishiki.Arcade)
                {
                    int a = (int)(list[index].nanido / 10);
                    bool notResetRand = false;

                    if (arcadeLevel == 1)
                    {
                        notResetRand = ((list[index].nanido == arcadeLevel) || (list[index].nanido - a * 10 + a == arcadeLevel));
                    }
                    else if (arcadeLevel >= 2)
                    {
                        notResetRand = (((list[index].nanido >= arcadeLevel - 1) && (list[index].nanido <= arcadeLevel)) ||
                                        ((list[index].nanido - a * 10 + a >= arcadeLevel - 1) && (list[index].nanido - a * 10 + a <= arcadeLevel)));
                    }

                    if (!notResetRand)
                    {
                        goto RANDOMRESET;
                    }
                }

                quizManager.StartQuizJunban(list[index].mondaibun, list[index].sentakushi, list[index].kazu, list[index].nanido);

                quizManager.addRecordData.id = index;
                quizManager.addRecordData.series = list[index].series;
                quizManager.addRecordData.nanido = list[index].nanido;
                quizManager.addRecordData.looked = false;
                quizManager.addRecordData.firstCollect = false;

                quizManager.ZukanOpen(System.Int32.Parse(list[index].sakuhin));

                nowMondai++;
            }
            else if (sheetId == Keishiki.Tato && SpreadSheetCheck.readedTatoData != null)
            {
                var list = SpreadSheetCheck.readedTatoData;

                int index = UnityEngine.Random.Range(0, list.Count);

                if (nowRule == QuizRuleManager.QuizKeishiki.Arcade)
                {
                    int a = (int)(list[index].nanido / 10);
                    bool notResetRand = false;

                    if (arcadeLevel == 1)
                    {
                        notResetRand = ((list[index].nanido == arcadeLevel) || (list[index].nanido - a * 10 + a == arcadeLevel));
                    }
                    else if (arcadeLevel >= 2)
                    {
                        notResetRand = (((list[index].nanido >= arcadeLevel - 1) && (list[index].nanido <= arcadeLevel)) ||
                                        ((list[index].nanido - a * 10 + a >= arcadeLevel - 1) && (list[index].nanido - a * 10 + a <= arcadeLevel)));
                    }

                    if (!notResetRand)
                    {
                        goto RANDOMRESET;
                    }
                }

                quizManager.StartQuizTato(list[index].mondaibun, list[index].answer, list[index].dummy, list[index].nanido);

                quizManager.addRecordData.id = index;
                quizManager.addRecordData.series = list[index].series;
                quizManager.addRecordData.nanido = list[index].nanido;
                quizManager.addRecordData.looked = false;
                quizManager.addRecordData.firstCollect = false;

                quizManager.ZukanOpen(System.Int32.Parse(list[index].sakuhin));

                nowMondai++;
            }
            else if (sheetId == Keishiki.Sentsunagi && SpreadSheetCheck.readedSentsunagiData != null)
            {
                var list = SpreadSheetCheck.readedSentsunagiData;
                int index = UnityEngine.Random.Range(0, list.Count);

                if (nowRule == QuizRuleManager.QuizKeishiki.Arcade)
                {
                    int a = (int)(list[index].nanido / 10);
                    bool notResetRand = false;

                    if (arcadeLevel == 1)
                    {
                        notResetRand = ((list[index].nanido == arcadeLevel) || (list[index].nanido - a * 10 + a == arcadeLevel));
                    }
                    else if (arcadeLevel >= 2)
                    {
                        notResetRand = (((list[index].nanido >= arcadeLevel - 1) && (list[index].nanido <= arcadeLevel)) ||
                                        ((list[index].nanido - a * 10 + a >= arcadeLevel - 1) && (list[index].nanido - a * 10 + a <= arcadeLevel)));
                    }

                    if (!notResetRand)
                    {
                        goto RANDOMRESET;
                    }
                }

                quizManager.StartQuizSentsunagi(list[index].mondaibun, list[index].leftAnswer, list[index].rightAnswer, list[index].kazu, list[index].nanido);

                quizManager.addRecordData.id = index;
                quizManager.addRecordData.series = list[index].series;
                quizManager.addRecordData.nanido = list[index].nanido;
                quizManager.addRecordData.looked = false;
                quizManager.addRecordData.firstCollect = false;

                quizManager.ZukanOpen(System.Int32.Parse(list[index].sakuhin));

                nowMondai++;
            }
        }
        else
        {
            Debug.Log("問題を入れ込んでねえよい");
        }
    }
}
