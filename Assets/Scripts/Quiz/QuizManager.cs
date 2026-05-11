using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    public Text mondaibun;
    public float maxTime = 600;

    public float startTime = 120;
    public float answerEndTime = 120;
    public float endTime = 240;

    public float bunKankaku = 3;
    public float hintKankaku = 150;

    bool hintWait = false;
    float hintWaitTime = 0.0f;
    int nowHintMass = 0;

    public ButtonManager buttonManager;
    public TypingButtonManager typeButtonManager;

    public GameObject blackImage;
    public GameObject cantTouchImage;
    public SeikaiMoji mojiImage;

    float nowTime = 0.0f;
    bool nowTimeZero = false;

    public Text timeText;
    public RectTransform timeGauge;

    string mondaibunBase = "";
    int numAnswer = 0;
    List<string> textSentakushi = null;
    string textAnswer = "";
    int buttonKazu = 0;

    public SpreadSheetCheck spreadSheetCheck;
    Keishiki nowMondai = Keishiki.None;

    public ToRecordData addRecordData = new ToRecordData();

    public Text maxPointText;
    private int nowProduct = -1;

    enum Timing
    {
        Wait,
        StartWait,
        Quizing,
        AnswerWait,
        ResultWait,
    }

    public enum QuizType
    {
        Typing,
        Yontaku,
        Nitaku,
    }

    Timing nowTiming = Timing.Wait;

    public enum AnswerType
    {
        None,
        Answer,
        Fuseikai,
        NanmonSeikai,
        Jikangire
    }

    AnswerType answerType = AnswerType.None;

    public bool StartQuiz(string bun, QuizType type)
    {
        nowTimeZero = true;
        nowHintMass = 0;
        hintWaitTime = 0.0f;
        nowTiming = Timing.StartWait;

        buttonManager.ButtonAppear4(new List<string> { "あああ", "いいい" });

        buttonManager.ButtonAppear2(new List<string> { "あああ", "いいい" });

        typeButtonManager.TypingButtonAppear("かな");

        mondaibunBase = bun;
        mojiImage.MojiDisappear();
        return false;
    }

    public List<int> ShuffleList(List<int> list)
    {
        List<int> ret = list;

        for (int i = ret.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1); // ランダムで要素番号を１つ選ぶ（ランダム要素）
            var temp = ret[i]; // 一番最後の要素を仮確保（temp）にいれる
            ret[i] = ret[j]; // ランダム要素を一番最後にいれる
            ret[j] = temp; // 仮確保を元ランダム要素に上書き
        }
        return ret;
    }

    public List<string> ShuffleList(List<string> list)
    {
        List<string> ret = list;

        for (int i = ret.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1); // ランダムで要素番号を１つ選ぶ（ランダム要素）
            var temp = ret[i]; // 一番最後の要素を仮確保（temp）にいれる
            ret[i] = ret[j]; // ランダム要素を一番最後にいれる
            ret[j] = temp; // 仮確保を元ランダム要素に上書き
        }
        return ret;
    }

    float seikairitsu = 1.0f;
    int nanido = 0;

    public bool StartQuizTyping(string bun, string answer, string type, int nanido)
    {
        nowTimeZero = true;
        nowTiming = Timing.StartWait;
        typeButtonManager.TypingButtonAppear(type);
        buttonManager.ButtonClear();
        mondaibunBase = bun;

        textAnswer = answer;
        this.nanido = nanido;
        nowMondai = Keishiki.Typing;
        mondaibun.alignment = TextAnchor.UpperLeft;

        buttonKazu = 0;
        return true;
    }

    public bool StartQuizYontaku(string bun, string answer, string dummy, int nanido)
    {
        List<string> sentakushi = spreadSheetCheck.KaigyoFix(answer + "\n" + dummy);
        sentakushi = ShuffleList(sentakushi);

        for (int i=0;i< sentakushi.Count;i++)
        {
            if (sentakushi[i].Contains("｜"))
            {
                sentakushi[i] = sentakushi[i].Split('｜')[Random.Range(0, sentakushi[i].Length - sentakushi[i].Replace("｜", "").Length)];
            }
        }

        nowTimeZero = true;
        nowTiming = Timing.StartWait;
        buttonManager.ButtonAppear4(sentakushi);
        typeButtonManager.ButtonClear();
        mondaibunBase = bun;
        this.numAnswer = sentakushi.IndexOf(answer) + 1;

        this.nanido = nanido;
        nowMondai = Keishiki.Yontaku;
        mondaibun.alignment = TextAnchor.UpperLeft;

        buttonKazu = 4;
        return true;
    }

    public bool StartQuizNitaku(string bun, string answer, string wrong, int nanido)
    {
        List<string> sentakushi = new List<string>() { answer, wrong };
        sentakushi = ShuffleList(sentakushi);

        nowTimeZero = true;
        nowTiming = Timing.StartWait;
        buttonManager.ButtonAppear2(sentakushi);
        typeButtonManager.ButtonClear();
        mondaibunBase = bun;
        this.numAnswer = sentakushi.IndexOf(answer) + 1;

        this.nanido = nanido;
        nowMondai = Keishiki.Nitaku;
        mondaibun.alignment = TextAnchor.UpperLeft;

        buttonKazu = 2;
        return true;
    }
    public bool StartQuizMarubatsu(string bun, bool answer, int nanido)
    {
        List<string> sentakushi = new List<string>() {"〇", "×"};
        //sentakushi = ShuffleList(sentakushi);

        nowTimeZero = true;
        nowTiming = Timing.StartWait;
        buttonManager.ButtonAppear2(sentakushi);
        typeButtonManager.ButtonClear();
        mondaibunBase = bun;

        int sei = 2;
        if (answer) sei = 1;

        this.numAnswer = sei;
        this.nanido = nanido;
        nowMondai = Keishiki.Marubatsu;
        mondaibun.alignment = TextAnchor.UpperLeft;

        buttonKazu = 2;
        return true;
    }

    public bool StartQuizRensou4(string bun, string answer, string dummy, int nanido)
    {
        List<string> sentakushi = spreadSheetCheck.KaigyoFix(answer + "\n" + dummy);
        sentakushi = ShuffleList(sentakushi);

        for (int i = 0; i < sentakushi.Count; i++)
        {
            if (sentakushi[i].Contains("｜"))
            {
                sentakushi[i] = sentakushi[i].Split('｜')[Random.Range(0, sentakushi[i].Length - sentakushi[i].Replace("｜", "").Length)];
            }
        }

        nowTimeZero = true;
        hintWaitTime = 0.0f;
        nowHintMass = 0;
        nowTiming = Timing.StartWait;
        buttonManager.ButtonAppear4(sentakushi);
        typeButtonManager.ButtonClear();
        mondaibunBase = bun;
        this.numAnswer = sentakushi.IndexOf(answer) + 1;

        this.nanido = nanido;
        nowMondai = Keishiki.Rensou4;
        mondaibun.alignment = TextAnchor.UpperCenter;

        buttonKazu = 4;
        return true;
    }

    // 順番当て
    public bool StartQuizJunban(string bun, string sentakushi_b, int kazu, int nanido)
    {
        List<string> sentakushi = spreadSheetCheck.KaigyoFix(sentakushi_b);
        int hazureMax = sentakushi.Count - kazu;
        
        if (hazureMax > 0)
        {
            while(sentakushi.Count > kazu)
            {
                int removeIndex = Random.Range(0, sentakushi.Count);
                sentakushi.RemoveAt(removeIndex);
            }
        }

        for (int i = 0; i < sentakushi.Count; i++)
        {
            if (sentakushi[i].Contains("｜"))
            {
                sentakushi[i] = sentakushi[i].Split('｜')[Random.Range(0, sentakushi[i].Length - sentakushi[i].Replace("｜", "").Length)];
            }
        }

        List<string> sentakushi_c = new List<string>(sentakushi);
        List<string> useSentakushi = ShuffleList(sentakushi_c);

        string ansStr = "";
        for (int i = 0;i< sentakushi.Count;i++)
        {
            ansStr += sentakushi[i];
        }

        nowTimeZero = true;
        nowTiming = Timing.StartWait;
        buttonManager.ButtonAppear4(useSentakushi, true);
        typeButtonManager.ButtonClear();
        mondaibunBase = bun;
        textAnswer = ansStr;
        textSentakushi = useSentakushi;

        this.nanido = nanido;
        nowMondai = Keishiki.Junban;
        mondaibun.alignment = TextAnchor.UpperLeft;

        buttonKazu = kazu;
        return true;
    }

    // 一問多答
    public bool StartQuizTato(string bun, string answer, string dummy, int nanido)
    {
        List<string> answerSentakushi = spreadSheetCheck.KaigyoFix(answer);


        if (dummy == "スマブラ全キャラ")
        {
            dummy = "マリオ\nドンキーコング\nリンク\nサムス\nダークサムス\nヨッシー\nカービィ\nフォックス\nピカチュウ\nルイージ\nネス\nキャプテン・ファルコン\nプリン\nピーチ\nデイジー\nクッパ\nアイスクライマー\nシーク\nゼルダ\nドクターマリオ\nピチュー\nファルコ\nマルス\nルキナ\nこどもリンク\nガノンドロフ\nミュウツー\nロイ\nクロム\nMr.ゲーム＆ウォッチ\nメタナイト\nピット\nブラックピット\nゼロスーツサムス\nワリオ\nスネーク\nアイク\nポケモントレーナー\nゼニガメ\nフシギソウ\nリザードン\nディディーコング\nリュカ\nソニック\nデデデ\nピクミン＆オリマー\nルカリオ\nロボット\nトゥーンリンク\nウルフ\nむらびと\nロックマン\nWii Fit トレーナー\nロゼッタ＆チコ\nリトル・マック\nゲッコウガ\nMiiファイター\nパルテナ\nパックマン\nルフレ\nシュルク\nクッパJr.\nダックハント\nリュウ\nケン\nクラウド\nカムイ\nベヨネッタ\nインクリング\nリドリー\nシモン\nリヒター\nキングクルール\nしずえ\nガオガエン\nパックンフラワー\nジョーカー\n勇者\nバンジョー＆カズーイ\nテリー\nベレト\nミェンミェン\nスティーブ\nセフィロス\nホムラ\nヒカリ\nカズヤ\nソラ";
        }

        List<string> dummySentakushi = new List<string>();
        if (dummy != "")
        {
            dummySentakushi = spreadSheetCheck.KaigyoFix(dummy);
        }

        int kazu = 5;
        int atariMax = answerSentakushi.Count;
        if (answerSentakushi.Count + dummySentakushi.Count > kazu)
        {
            atariMax = Random.Range(1, Mathf.Min(answerSentakushi.Count, kazu));
            if (kazu - dummySentakushi.Count > atariMax)
            {
                atariMax = kazu - dummySentakushi.Count;
            }
        }
        else kazu = answerSentakushi.Count + dummySentakushi.Count;

        Debug.Log(bun);
        if (atariMax > 0)
        {
            while (answerSentakushi.Count > atariMax)
            {
                int removeIndex = Random.Range(0, answerSentakushi.Count);
                answerSentakushi.RemoveAt(removeIndex);
            }
        }

        List<string> useSentakushi = new List<string>(answerSentakushi);

        for (int i = answerSentakushi.Count; i < kazu; i++)
        {
            int addIndex = Random.Range(0, dummySentakushi.Count);
            while (useSentakushi.Contains(dummySentakushi[addIndex]))
            {
                addIndex = Random.Range(0, dummySentakushi.Count);
            }
            useSentakushi.Add(dummySentakushi[addIndex]);
        }

        useSentakushi = ShuffleList(useSentakushi);

        for (int i = 0; i < useSentakushi.Count; i++)
        {
            if (useSentakushi[i].Contains("｜"))
            {
                useSentakushi[i] = useSentakushi[i].Split('｜')[Random.Range(0, useSentakushi[i].Length - useSentakushi[i].Replace("｜", "").Length)];
            }
        }

        string ansStr = "";
        for (int i = 0; i < useSentakushi.Count; i++)
        {
            if (answerSentakushi.Contains(useSentakushi[i]))
            {
                ansStr += (i + 1).ToString();
            }
        }

        nowTimeZero = true;
        nowTiming = Timing.StartWait;
        buttonManager.ButtonAppear4(useSentakushi, true, true);
        typeButtonManager.ButtonClear();
        mondaibunBase = bun;
        textAnswer = ansStr;

        this.nanido = nanido;
        nowMondai = Keishiki.Tato;
        mondaibun.alignment = TextAnchor.UpperLeft;

        buttonKazu = kazu;
        return true;
    }

    //  線つなぎ
    public bool StartQuizSentsunagi(string bun, List<string> leftAnswer, List<string> rightAnswer, int kazu, int nanido)
    {
        int sabun = leftAnswer.Count - kazu;

        List<string> leftAnswerList = new List<string>(leftAnswer);
        List<string> rightAnswerList = new List<string>(rightAnswer);

        Debug.Log(bun);
        if (sabun > 0)
        {
            while (leftAnswerList.Count > kazu)
            {
                int removeIndex = Random.Range(0, leftAnswerList.Count);
                leftAnswerList.RemoveAt(removeIndex);
                rightAnswerList.RemoveAt(removeIndex);
            }
        }
        List<string> leftSentakushi = new List<string>(leftAnswerList);
        List<string> rightSentakushi = new List<string>(rightAnswerList);

        for (int i = 0; i < leftSentakushi.Count; i++)
        {
            if (leftSentakushi[i].Contains("｜"))
            {
                leftSentakushi[i] = leftSentakushi[i].Split('｜')[Random.Range(0, leftSentakushi[i].Length - leftSentakushi[i].Replace("｜", "").Length)];
            }
            if (rightSentakushi[i].Contains("｜"))
            {
                rightSentakushi[i] = rightSentakushi[i].Split('｜')[Random.Range(0, rightSentakushi[i].Length - rightSentakushi[i].Replace("｜", "").Length)];
            }
        }

        List<string> leftSentakushi_c = new List<string>(leftSentakushi);
        List<string> useSentakushiL = ShuffleList(leftSentakushi_c);
        List<string> rightSentakushi_c = new List<string>(rightSentakushi);
        List<string> useSentakushiR = ShuffleList(rightSentakushi_c);

        string ansStr = "ABCDEFGHIJKLMNOP";

        for (int i = 0; i < kazu; i++)
        {
            ansStr = ansStr.Replace(ansStr.Substring(useSentakushiL.IndexOf(leftSentakushi[i]), 1), useSentakushiR.IndexOf(rightSentakushi[i]).ToString());
        }
        ansStr = ansStr.Substring(0, kazu);

        nowTimeZero = true;
        nowTiming = Timing.StartWait;
        buttonManager.ButtonAppearSentsunagi(useSentakushiL, useSentakushiR);
        typeButtonManager.ButtonClear();
        mondaibunBase = bun;
        textAnswer = ansStr;

        this.nanido = nanido;
        nowMondai = Keishiki.Sentsunagi;
        mondaibun.alignment = TextAnchor.UpperLeft;

        buttonKazu = kazu * 2;
        return true;
    }

    // 画面上の最高得点表示を更新
    public void MaxPointUpdate()
    {
        string maxPoint = SpreadSheetCheck.readedRecordData.maxAcademyMode;
        if (maxPoint == "")
            maxPointText.text = "0";
        else
            maxPointText.text = maxPoint;
    }

    public void NormaUpdate(string normaText)
    {
        maxPointText.text = normaText;
    }

    private void QuizStart()
    {
        blackImage.SetActive(false);
        cantTouchImage.SetActive(true);
        mojiImage.MojiDisappear();
        hintWait = false;

        buttonManager.ButtonStarting(startTime);
        typeButtonManager.ButtonStarting(startTime);

        float max = startTime / 60.0f;
        timeText.text = string.Format("{0:0.00}", maxTime / 60.0f).ToString();
        timeGauge.localScale = new Vector3((nowTime / max), 1, 1);

        if (nowTime * 60.0f >= startTime)
        {
            cantTouchImage.SetActive(false);
            nowTiming = Timing.Quizing;
            nowTimeZero = true;
        }

        
        if (QuizRuleManager.keishiki == QuizRuleManager.QuizKeishiki.All)
            MaxPointUpdate();
        mondaibun.text = "";
    }

    private void Quizing()
    {
        float max = maxTime / 60.0f;
        timeText.text = string.Format("{0:0.00}", max - nowTime).ToString();
        timeGauge.localScale = new Vector3(1.0f - (nowTime / max), 1,1);

        if (nowTime * 60.0f >= maxTime)
        {
            nowTiming = Timing.ResultWait;
            answerType = AnswerType.Jikangire;
            nowTimeZero = true;
            blackImage.SetActive(true);
        }

        int buttonCheck = buttonManager.ButtonCheck();
        if (buttonCheck != 0)
        {
            if (numAnswer == buttonCheck) answerType = AnswerType.Answer;
            else answerType = AnswerType.Fuseikai;

            nowTiming = Timing.AnswerWait;
            nowTimeZero = true;
            blackImage.SetActive(true);
        }

        // 複数選択系の問題の正誤判定
        string junbanButtonCheck = buttonManager.ButtonCheck(buttonKazu);
        if (junbanButtonCheck != "")
        {
            string fixAnwser = "";
            if (junbanButtonCheck.Contains("決定"))
            {
                fixAnwser = junbanButtonCheck.Replace("決定", "");
            }
            else
            {
                fixAnwser = "";
                for (int i = 0; i < junbanButtonCheck.Length; i++)
                {
                    fixAnwser += textSentakushi[System.Int32.Parse(junbanButtonCheck.Substring(i, 1)) - 1];
                }
            }
            if (textAnswer == fixAnwser) answerType = AnswerType.Answer;
            else answerType = AnswerType.Fuseikai;

            nowTiming = Timing.AnswerWait;
            nowTimeZero = true;
            blackImage.SetActive(true);
        }

        // 線つなぎの判定
        string sentsunagiButtonCheck = buttonManager.ButtonCheckS(buttonKazu);
        if (sentsunagiButtonCheck != "")
        {
            string fixAnwser = "";
            
            fixAnwser = "ABCDEFGHIJKLMNOP";
            for (int i = 0; i < textAnswer.Length; i++)
            {
                int replaceIndex = System.Int32.Parse(sentsunagiButtonCheck.Substring(i * 2, 1));
                int replaceAnswer = System.Int32.Parse(sentsunagiButtonCheck.Substring(i * 2 + 1, 1));

                if (replaceIndex < replaceAnswer) 
                    fixAnwser = fixAnwser.Replace(fixAnwser.Substring(replaceIndex, 1), (replaceAnswer - textAnswer.Length).ToString());
                else 
                    fixAnwser = fixAnwser.Replace(fixAnwser.Substring(replaceAnswer, 1), (replaceIndex - textAnswer.Length).ToString());
            }
            fixAnwser = fixAnwser.Substring(0, textAnswer.Length);

            if (textAnswer == fixAnwser) answerType = AnswerType.Answer;
            else answerType = AnswerType.Fuseikai;

            nowTiming = Timing.AnswerWait;
            nowTimeZero = true;
            blackImage.SetActive(true);
        }

        string typeButtonCheck = typeButtonManager.ButtonCheck();
        if (typeButtonCheck != "")
        {
            answerType = AnswerType.Fuseikai;
            if (textAnswer.Contains("｜"))
            {
                var answerList = textAnswer.Split('｜');
                for (int i = 0; i < answerList.Length; i++)
                {
                    if (answerList[i] == typeButtonCheck) answerType = AnswerType.Answer;
                }
            }
            else if (textAnswer == typeButtonCheck) answerType = AnswerType.Answer;

            nowTiming = Timing.AnswerWait;
            nowTimeZero = true;
            blackImage.SetActive(true);
        }

        if (mondaibun.text != mondaibunBase)
        {
            if ((nowMondai == Keishiki.Typing) || (nowMondai == Keishiki.Yontaku) || (nowMondai == Keishiki.Nitaku) || (nowMondai == Keishiki.Marubatsu) || (nowMondai == Keishiki.Junban) || (nowMondai == Keishiki.Tato) || (nowMondai == Keishiki.Sentsunagi))
            {
                int bunMass = mondaibun.text.Length;
                if ((int)(nowTime * 60.0f) >= (int)(bunMass * bunKankaku))
                {
                    bunMass += 1;
                    string bun = mondaibunBase.Substring(0, bunMass);
                    if ((bun.Substring(bun.Length - 1) == "\\") || (bun.Substring(bun.Length - 1) == "\n"))
                    {
                        if (mondaibunBase.Length <= bunMass + 2)
                            bun = mondaibunBase.Substring(0, bunMass + 2);
                    }
                    mondaibun.text = bun;
                }
            }
            else if (nowMondai == Keishiki.Rensou4)
            {
                hintWaitTime += Time.deltaTime;
                if (!hintWait)
                {
                    int bunMass = mondaibun.text.Length;

                    int kaigyoMass = mondaibun.text.LastIndexOf("\n");
                    int nowGyoMass = bunMass;
                    if (kaigyoMass != -1)
                    {
                        nowGyoMass = bunMass - kaigyoMass;
                    }
                    if ((int)(nowTime * 60.0f) >= (int)(nowGyoMass * bunKankaku) + (hintKankaku * nowHintMass))
                    {
                        bunMass += 1;
                        string bun = mondaibunBase.Substring(0, bunMass);
                        if ((bun.Substring(bun.Length - 1) == "\\") || (bun.Substring(bun.Length - 1) == "\n"))

                        {
                            hintWait = true;
                            nowHintMass++;
                        }
                        mondaibun.text = bun;
                    }
                }
                else
                {
                    if ((hintWaitTime * 60.0f) >= hintKankaku)
                    {
                        hintWait = false;
                        hintWaitTime = 0.0f;
                    }
                }
            }
        }
    }

    private void AnswerWait()
    {
        if (nowTime * 60.0f >= answerEndTime)
        {
            nowTiming = Timing.ResultWait;
            nowTimeZero = true;
        }
    }

    private void ResultWait()
    {
        if (nowTime == 0)
        {
            addRecordData.firstCollect = ((answerType == AnswerType.Answer) || (answerType == AnswerType.NanmonSeikai));
            
            spreadSheetCheck.RecordDataSet(spreadSheetCheck.CopyRecordData(addRecordData), nowProduct);
            //spreadSheetCheck.RecordDataSet(addRecordData, nowProduct);

            int max = 0;
            if (maxPointText.text != "")
            {
                max = System.Convert.ToInt32(maxPointText.text);
            }
            int now = this.GetComponent<KeishikiParent>().GetPoint();
            string sendPoint = "0";
            if (now > max)
                sendPoint = now.ToString();
            else
                sendPoint = maxPointText.text;

            spreadSheetCheck.DataToRecord();
            spreadSheetCheck.SendPointAcademic(sendPoint);
            StartCoroutine(spreadSheetCheck.PostDataData(SpreadSheetCheck.userId, SpreadSheetCheck.readedRecordData.name));
        }

        mojiImage.MojiAppear(answerType);
        mojiImage.MojiMove(nowTime, endTime / 60.0f);
        if (nowTime * 60.0f >= endTime)
        {
            nowTiming = Timing.Wait;
            nowTimeZero = true;
        }
    }

    private void Update()
    {
        if (nowTiming == Timing.StartWait)
        {
            QuizStart();
            

        }
        else if (nowTiming == Timing.Quizing)
        {
            Quizing();
        }
        else if (nowTiming == Timing.AnswerWait)
        {
            AnswerWait();
        }
        else if (nowTiming == Timing.ResultWait)
        {
            ResultWait();
        }

        if (nowTiming != Timing.Wait)
        {
            nowTime += Time.deltaTime;
        }
        else
        {
            mondaibun.text = "";
            blackImage.SetActive(false);
            mojiImage.MojiDisappear();
        }

        if (nowTimeZero)
        {
            nowTime = 0.0f;
            nowTimeZero = false;
        }
    }

    public bool IsFree()
    {
        return nowTiming == Timing.Wait;
    }

    public AnswerType GetAnwserType()
    {
        if((nowTiming == Timing.ResultWait) &&
            (nowTime == 0))
        {
            return answerType;
        }
        return AnswerType.None;
    }

    public float GetPoint()
    {
        float ret = 0.0f;
        float nokoriTime = float.Parse(timeText.text);

        ret = nokoriTime * 10.0f + (((float)nanido - 1.0f) * 50.0f);

        return Mathf.Round(ret);
    }

    public void ZukanOpen(int product)
    {
        nowProduct = product;
        //spreadSheetCheck.ZukanOpen(product);
    }
}
