using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatusManager : MonoBehaviour
{
    public Text name;

    public Text maxPoint;
    public Text ranking;
    public Text seikairitsu;
    public Text tokui;
    public Text nigate;

    public SpreadSheetCheck spreadSheetCheck;

    bool appended = false;

    List<string> seriesName = new List<string>
    {
        "マリオシリーズ",
        "ドンキーコング",
        "パンチアウト！！",
        "ゼルダの伝説",
        "メトロイド",
        "パルテナの鏡",
        "任天堂製テトリス",
        "MOTHERシリーズ",
        "ファイアーエムブレム",
        "星のカービィ",
        "スターフォックス",
        "ワリオシリーズ",
        "ヨッシーシリーズ",
        "ポケモンシリーズ",
        "バンジョーとカズーイ",
        "スマブラシリーズ",
        "どうぶつの森",
        "ピクミンシリーズ",
        "脳トレ系ゲーム",
        "リズム天国",
        "ゼノブレイドシリーズ",
        "スプラトゥーン",
        "その他のシリーズ"
        /*"不具合！出たら教えてね",
        "ディズニー関連ゲーム",
        "光線銃シリーズ",
        "役満シリーズ",
        "アソビ大全他テーブルゲーム",
        "スポーツ題材ゲーム",
        "エキサイト○○",
        "バルーンファイト",
        "ふぁみこんむかし話とか",
        "ファミコン探偵倶楽部",
        "ファミコンウォーズ",
        "ハル研製 単体作品群",
        "パイロットウイングス",
        "任天堂製シムシティー",
        "X -エックス-",
        "ウエーブレース / テン・エイティ",
        "ピクロスシリーズ",
        "パネルでポン",
        "レア社製 単体作品群",
        "カスタムロボ",
        "とっとこハム太郎",
        "罪と罰シリーズ",
        "カードヒーロー",
        "くるくるくるりん",
        "黄金の太陽",
        "マジカルバケーション",
        "伝説のスタフィー",
        "ラブデリック系作品群",
        "大合奏！バンドブラザーズ",
        "ミッチェル製パズル作品",
        "アナザーコード / ウィッシュルーム",
        "nintendogsシリーズ",
        "ちびロボ！シリーズ",
        "押忍！闘え！応援団",
        "週刊少年ジャンプ関連製品",
        "bit generations / Art Styleシリーズ",
        "Wiiスポーツ＆スイッチスポーツ",
        "フォーエバーブルー",
        "Wii Fit＆リングフィット",
        "カセキホリダー",
        "任天堂発売の零シリーズ",
        "マリオシリーズ",
        "マリオシリーズ",
        "マリオシリーズ",*/
    };

    // Update is called once per frame
    void Start()
    {
        if (SpreadSheetCheck.readedRecordData == null)
        {
            name.text = "プレイヤーデータが読み込めていないようだ";
        }
        else if (SpreadSheetCheck.readedRecordData.name.Replace(" ", "").Replace("　", "") == "")
        {
            name.text = "無記名（ランキングには反映されません）";
        }
        else
        {
            name.text = SpreadSheetCheck.readedRecordData.name;
        }

        if (!spreadSheetCheck.hasData)
        {
            maxPoint.enabled = false;
            ranking.enabled = false;
            seikairitsu.enabled = false;
            tokui.enabled = false;
            nigate.enabled = false;

            for (int i = 0; i < SpreadSheetCheck.mondaiIdList.Length; i++)
            {
                StartCoroutine(spreadSheetCheck.ReadSheet(i));
            }
            StartCoroutine(spreadSheetCheck.ReadDataSheet(SpreadSheetCheck.userId));
        }
    }

    private void Update()
    {
        if (spreadSheetCheck.hasData)
        {
            spreadSheetCheck.RecordInitialize();
        }
        if (spreadSheetCheck.hasRecordData)
        {
            if (!appended)
            {
                SetStatus();
                appended = true;
            }
        }
    }

    void SetStatus()
    {
        //Series
        List<int> lookedCount = new List<int>();
        List<int> collectCount = new List<int>();
        int lookedCountAll = 0;
        int collectCountAll = 0;

        for (int i = 0; i < (int)Series.MAX; i++)
        {
            lookedCount.Add(0);
            collectCount.Add(0);
        }
        for (int i = 0; i < SpreadSheetCheck.recordDataBase.Count; i++)
        {
            for (int j = 0; j < SpreadSheetCheck.recordDataBase[i].Count; j++)
            {
                var one = SpreadSheetCheck.recordDataBase[i][j];
                if ((int)one.product != 9999)
                {
                    int series = SpreadSheetCheck.readedSeriesData[(int)one.product - 1];
                    if (one.looked)
                    {
                        lookedCount[series]++;
                        lookedCountAll++;
                    }
                    if (one.firstCollect)
                    {
                        collectCount[series]++;
                        collectCountAll++;
                    }
                }
            }
        }

        int maxIndex = 0;
        int minIndex = 0;
        int lookSeries = 0;
        for (int i = 1; i < (int)Series.MAX; i++)
        {
            float n = (float)collectCount[i] / (float)lookedCount[i];
            float nMax = 0;
            float nMin = 99999; 
            if (lookedCount[maxIndex] > 0) nMax = (float)collectCount[maxIndex] / (float)lookedCount[maxIndex];
            if (lookedCount[minIndex] > 0) nMin = (float)collectCount[minIndex] / (float)lookedCount[minIndex];

            int plus = i;
            if (plus >= seriesName.Count - 1)
                plus = seriesName.Count - 1;

            if (n > nMax)
            {
                maxIndex = plus;
            }
            if (lookedCount[i] > 0)
            {
                if (nMin > n)
                {
                    minIndex = plus;
                }
            }

            if ((lookedCount[i] > 0) && (i < seriesName.Count))
            {
                lookSeries++;
            }
        }


        //if (lookSeries >= seriesName.Count / 2)
        {
            if ((float)lookedCount[maxIndex] / (float)collectCount[maxIndex] != 0)
            {
                tokui.text = seriesName[maxIndex];
                tokui.enabled = true;
            }
            if ((float)lookedCount[minIndex] / (float)collectCount[minIndex] != 0)
            {
                nigate.text = seriesName[minIndex];
                nigate.enabled = true;
            }
        }
        
        if (SpreadSheetCheck.readedRecordData.maxAcademyMode != "")
        {
            maxPoint.text = SpreadSheetCheck.readedRecordData.maxAcademyMode + "点";
            maxPoint.enabled = true;
        }
        if (SpreadSheetCheck.readedRecordData.rank > 0)
        {
            ranking.text = SpreadSheetCheck.readedRecordData.rank.ToString() + "位";
            ranking.enabled = true;
        }
        if ((collectCountAll != 0) && (lookedCountAll != 0))
        {
            seikairitsu.text = ((int)((float)collectCountAll / (float)lookedCountAll * 100)).ToString() + "％";
            seikairitsu.enabled = true;
        }
    }
    public void PushBackBefore()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
