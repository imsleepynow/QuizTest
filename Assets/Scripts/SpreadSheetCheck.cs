using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public enum Series
{
    Mario,
    Donkey,
    PunchOut,
    Zelda,
    Metroid,
    KidIcarus,
    Tetris,
    Mother,
    FireEmblem,
    FZero,
    Kirby,
    StarFox,
    Wario,
    Yoshi,
    Pokemon,
    BanjoKazooie,
    SmashBros,
    Animal,
    Pikmin,
    BrainAge,
    Rhythm,
    Xenoblade,
    Splatoon,
    Dummy1, // 区切ってる部分
    Disney,
    Zapper,
    Mahjong,
    TableGame,
    Sports,
    Excitebike,
    BallonFight,
    Mukashibanashi,
    DetectiveClub,
    FamicomWars,
    HalLab,
    Pirotwings,
    SimCity,
    X,
    Hayami,
    Picross,
    PuzzleLeague,
    RareWare,
    CustomRobo,
    Hamtaro,
    SinAndP,
    CardHero,
    Kururin,
    GoldenSun,
    MagicalVacation,
    Starfy,
    Lovederic,
    BandBros,
    Mitchell,
    Cing,
    Nintendogs,
    ChibiRobo,
    Yawaraka,
    Ouendan,
    ShonenJump,
    BitGenerations,
    WiiSports,
    ForeverBlue,
    WiiFit,
    WiiParty,
    KasekiHorider,
    Zero,
    Mii,
    GirlsMode,
    Egokoro,
    Surechigai,
    HikuOsu,
    JustDance,
    Nikki,
    Culdcept,
    TheRollingWestern,
    NesRimix,
    Bayonetta,
    WiiUIndie,
    OneTwoSwitch,
    Labo,
    Dummy2, // 区切ってる部分
    Arcade,
    FC,
    SFC,
    SatellaView,
    N64,
    GC,
    Wii,
    WiiU,
    GW,
    GB,
    VB,
    GBA,
    DS,
    Ware,
    ThreeDS,
    Smart,
    Switch1,
    Switch2,
    Dummy3, // 区切ってる部分
    Maker,
    Collab,
    OverSea,
    Movie,
    Hobby,
    Console,
    People,
    Service,
    Event,
    Yamauchi,
    Mix,
    News,
    None,

    MAX
}

public class TypingData
{
    public string mondaibun = "";                      // 問題文
    public string type = "";                           // 入力形式
    public string answer = "";                         // 解答
    public Series series = Series.None;                // 関連シリーズ
    public string sakuhin = "";                        // 図鑑作品コード
    public List<string> season = new List<string>();   // 季節イベ
    public int nanido = 0;                             // 難易度
    public string sakumonsha = "";                     // 作問者
    public string joken = "";                          // 条件
}

public class YontakuData
{
    public string mondaibun = "";                      // 問題文
    public string dummy = "";                          // ダミー選択肢
    public string answer = "";                         // 解答
    public Series series = Series.None;                // 関連シリーズ
    public string sakuhin = "";                        // 図鑑作品コード
    public List<string> season = new List<string>();   // 季節イベ
    public int nanido = 0;                             // 難易度
    public string sakumonsha = "";                     // 作問者
    public string joken = "";                          // 条件
}

public class Rensou4Data
{
    public string mondaibun = "";                      // 問題文
    public string dummy = "";                          // ダミー選択肢
    public string answer = "";                         // 解答
    public Series series = Series.None;                // 関連シリーズ
    public string sakuhin = "";                        // 図鑑作品コード
    public List<string> season = new List<string>();   // 季節イベ
    public int nanido = 0;                             // 難易度
    public string sakumonsha = "";                     // 作問者
    public string joken = "";                          // 条件
}

public class NitakuData
{
    public string mondaibun = "";                      // 問題文
    public string dummy = "";                          // ダミー選択肢
    public string answer = "";                         // 解答
    public Series series = Series.None;                // 関連シリーズ
    public string sakuhin = "";                        // 図鑑作品コード
    public List<string> season = new List<string>();   // 季節イベ
    public int nanido = 0;                             // 難易度
    public string sakumonsha = "";                     // 作問者
    public string joken = "";                          // 条件
}

public class MarubatsuData
{
    public string mondaibun = "";                      // 問題文
    public bool answer = true;                          // ダミー選択肢
    public Series series = Series.None;                // 関連シリーズ
    public string sakuhin = "";                        // 図鑑作品コード
    public List<string> season = new List<string>();   // 季節イベ
    public int nanido = 0;                             // 難易度
    public string sakumonsha = "";                     // 作問者
    public string joken = "";                          // 条件
}

public class JunbanData
{
    public string mondaibun = "";                      // 問題文
    public string sentakushi = "";                     // 選択肢
    public int kazu = 0;                               // 選択肢の数
    public Series series = Series.None;                // 関連シリーズ
    public string sakuhin = "";                        // 図鑑作品コード
    public List<string> season = new List<string>();   // 季節イベ
    public int nanido = 0;                             // 難易度
    public string sakumonsha = "";                     // 作問者
    public string joken = "";                          // 条件
}

public class TatoData
{
    public string mondaibun = "";                      // 問題文
    public string dummy = "";                          // ダミー選択肢
    public string answer = "";                         // 解答
    public int kazu = 0;                               // 選択肢の数
    public Series series = Series.None;                // 関連シリーズ
    public string sakuhin = "";                        // 図鑑作品コード
    public List<string> season = new List<string>();   // 季節イベ
    public int nanido = 0;                             // 難易度
    public string sakumonsha = "";                     // 作問者
    public string joken = "";                          // 条件
}

public class SentsunagiData
{
    public string mondaibun = "";                           // 問題文
    public List<string> leftAnswer = new List<string>();    // 左の選択肢
    public List<string> rightAnswer = new List<string>();   // 右の選択肢
    public int kazu = 0;                                    // 選択肢の数
    public Series series = Series.None;                     // 関連シリーズ
    public string sakuhin = "";                             // 図鑑作品コード
    public List<string> season = new List<string>();        // 季節イベ
    public int nanido = 0;                                  // 難易度
    public string sakumonsha = "";                          // 作問者
    public string joken = "";                               // 条件
}

public class LibraryData
{
    public string name = "";                           // 問題文
    public string furigana = "";                       // ダミー選択肢
    public Series series = Series.None;                // 関連シリーズ
    public string genre = "";                          // ジャンル
    public string start = "";                          // 発売・生誕
    public string end = "";                            // 死去・サ終
    public string hard = "";                            // ハード
    public string bun = "";                            // 解説文
    public int product = -1;                           // 作品番号
    public bool open = false;
    public int rare = -1;
}

public class SoundTestData
{
    public string name = "";                           // 曲名
    public string furigana = "";                       // ふりがな
    public Series series = Series.None;                // 関連シリーズ
    public string reference = "";                      // 出典作品名
    public string bpm = "";                            // BPM
    public string arranger = "";                       // 編曲者
    public string place = "";                          // 使用箇所
    public string bun = "";                            // 曲の文
    public string bun2 = "";                           // 編曲の文
}


public class ToRecordData
{
    public int id = -1;
    public bool looked = false;
    public bool firstCollect = false;
    public Keishiki keishiki = Keishiki.None;
    public Series series = Series.None;
    public int product = -1;
    public int nanido = 0;
}

public class Record
{
    public int id = -1;
    public string address = "";// UnityEngine.SystemInfo.deviceUniqueIdentifier;
    public string name = "";
    public string pass = "";
    public string open = "";
    public int open_num = 0;
    public string maxAcademyMode = "";
    public string maxHayaoshiMode = "";
    public string maxSeries = "";
    public string maxSeriesCollect = "";
    public string maxKeishiki = "";
    public string maxKeishikiCollect = "";
    public string maxNanido = "";
    public string maxNanidoCollect = "";
    public string zukan = "";
    public int zukan_num = 0;
    public string music = "";
    public int rank = 0;
}

public enum Keishiki
{
    Typing,
    Yontaku,
    Marubatsu,
    Rensou4,
    Nitaku,
    Junban,
    Tato,
    Sentsunagi,
    None
}

public class SpreadSheetCheck : MonoBehaviour
{
    public static string[] mondaiIdList = { "問題：タイピング", "問題：4択", "問題：マルバツ", "問題：連想4", "問題：2択", "問題：順番当て", "問題：一問多答", "問題：線つなぎ" , "検出用年表"};

    public List<string> sheetNameList;
    public List<List<string>> sheetData;

    public static List<TypingData> readedTypingData = new List<TypingData>();
    public static List<YontakuData> readedYontakuData = new List<YontakuData>();
    public static List<Rensou4Data> readedRensou4Data = new List<Rensou4Data>();
    public static List<NitakuData>  readedNitakuData  = new List<NitakuData>();
    public static List<MarubatsuData> readedMarubatsuData = new List<MarubatsuData>();
    public static List<JunbanData> readedJunbanData = new List<JunbanData>();
    public static List<TatoData> readedTatoData = new List<TatoData>();
    public static List<SentsunagiData> readedSentsunagiData = new List<SentsunagiData>();

    public static List<Texture> readedTextureData = new List<Texture>();

    public static Record readedRecordData = new Record { };
    public bool hasRecordData = false;

    public string readedData = "";

    public bool hasData = false;
    List<bool> hasDatas = new List<bool>();

    public static List<LibraryData> readedLibraryData = new List<LibraryData>();
    public static bool hasLibraryData = false;

    public static List<SoundTestData> readedSoundTestData = new List<SoundTestData>();
    public bool hasSoundTestData = false;

    public static List<string> readedNenpyoData = new List<string>();
    public static List<int> readedSeriesData = new List<int>();

    public ToRecordData CopyRecordData(ToRecordData ya)
    {
        ToRecordData copy = new ToRecordData();
        copy.id = ya.id;
        copy.keishiki = ya.keishiki;
        copy.looked = ya.looked;
        copy.nanido = ya.nanido;
        copy.product = ya.product;
        copy.series = ya.series;
        copy.firstCollect = ya.firstCollect;
        return copy;
    }

    // ==============================================================================================
    // 問題データの読み書き（書くことはないと思うよ）
    // ==============================================================================================

    private IEnumerator ReadSpreadSheet(int id)
    {
        string sheetName = mondaiIdList[id];
        //int sheetID = sheetNameList.IndexOf(sheetName);

        const string s_ID = "1fstBHnrnmhCLKH9mx-nyn99hNnGzz2-WGQuD4WXztwo";
        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + s_ID + "/gviz/tq?tqx=out:csv&sheet=" + sheetName);
        yield return request.SendWebRequest();


        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("ERROR");
            Debug.Log(request.error);
            request.Dispose();
        }
        else
        {
            Debug.Log("OK");
            // Debug.Log(request.downloadHandler.text);
            readedData = request.downloadHandler.text;

            var spl1 = readedData.Split(',');

            string[] del = { "\n\"" };
            List<string> spl2 = new List<string>();
            for (int i = 0; i < spl1.Length; i++)
            {
                var spl2Add = spl1[i].Split(del, System.StringSplitOptions.None);
                for (int j = 0; j < spl2Add.Length; j++)
                {
                    spl2.Add(spl2Add[j].Replace("\"", ""));
                }
            }
            //Debug.Log(spl2);

            if (hasDatas.Count == 0)
            {
                for (int i = 0; i < mondaiIdList.Length; i++)
                    hasDatas.Add(false);
            }
            hasDatas[id] = true;

            hasData = true;
            for (int i = 0; i < mondaiIdList.Length; i++)
                if (hasDatas[i] == false)
                    hasData = false;

            if (sheetName == "問題：タイピング")
            {
                readedTypingData.Clear();
                int yokoMax = 11;
                for (int i = 0; i < (spl2.Count / yokoMax); i++)
                {
                    if (i != 0)
                    {
                        if (spl2[i * yokoMax + 1].Contains("_")) continue;
                        if (spl2[i * yokoMax + 1].Contains("*")) continue;
                        if (spl2[i * yokoMax + 9] != "0")
                        {
                            TypingData addData = new TypingData();
                            addData.mondaibun = spl2[i * yokoMax + 1];
                            addData.answer = spl2[i * yokoMax + 2];
                            addData.type = spl2[i * yokoMax + 3];
                            addData.series = (Series)(System.Int32.Parse(spl2[i * yokoMax + 5]) - 1);
                            addData.sakuhin = spl2[i * yokoMax + 7];
                            addData.nanido = System.Int32.Parse(spl2[i * yokoMax + 8]);
                            addData.sakumonsha = "";
                            addData.joken = spl2[i * yokoMax + 10];
                            readedTypingData.Add(addData);
                        }
                    }
                }
            }
            else if (sheetName == "問題：4択")
            {
                readedYontakuData.Clear();
                int yokoMax = 11;
                for (int i = 0; i < (spl2.Count / yokoMax); i++)
                {
                    if (i != 0)
                    {
                        if (spl2[i * yokoMax + 1].Contains("_")) continue;
                        if (spl2[i * yokoMax + 1].Contains("*")) continue;
                        if (spl2[i * yokoMax + 2].Contains("*")) continue;
                        if (spl2[i * yokoMax + 9] != "0")
                        {
                            YontakuData addData = new YontakuData();
                            addData.mondaibun = spl2[i * yokoMax + 1];
                            addData.answer = spl2[i * yokoMax + 2];
                            addData.dummy = spl2[i * yokoMax + 3];
                            addData.series = (Series)(System.Int32.Parse(spl2[i * yokoMax + 5]) - 1);
                            addData.sakuhin = spl2[i * yokoMax + 7];
                            addData.nanido = System.Int32.Parse(spl2[i * yokoMax + 8]);
                            addData.sakumonsha = "";
                            addData.joken = spl2[i * yokoMax + 10];
                            readedYontakuData.Add(addData);
                        }
                    }
                }
            }
            else if(sheetName == "問題：マルバツ")
            {
                readedMarubatsuData.Clear();
                int yokoMax = 10;
                for (int i = 0; i < (spl2.Count / yokoMax); i++)
                {
                    if (i != 0)
                    {
                        if (spl2[i * yokoMax + 1].Contains("_")) continue;
                        if (spl2[i * yokoMax + 1].Contains("*")) continue;
                        if (spl2[i * yokoMax + 9] != "0")
                        {
                            MarubatsuData addData = new MarubatsuData();
                            addData.mondaibun = spl2[i * yokoMax + 1];
                            addData.answer = spl2[i * yokoMax + 2] == "o";
                            addData.series = (Series)(System.Int32.Parse(spl2[i * yokoMax + 4]) - 1);
                            addData.sakuhin = spl2[i * yokoMax + 6];
                            addData.nanido = System.Int32.Parse(spl2[i * yokoMax + 7]);
                            addData.sakumonsha = "";
                            addData.joken = spl2[i * yokoMax + 9];
                            readedMarubatsuData.Add(addData);
                        }
                    }
                }
            }
            else if (sheetName == "問題：連想4")
            {
                readedRensou4Data.Clear();
                int yokoMax = 11;
                for (int i = 0; i < (spl2.Count / yokoMax); i++)
                {
                    if (i != 0)
                    {
                        if (spl2[i * yokoMax + 1].Contains("_")) continue;
                        if (spl2[i * yokoMax + 1].Contains("*")) continue;
                        if (spl2[i * yokoMax + 2].Contains("*")) continue;
                        if (spl2[i * yokoMax + 9] != "0")
                        {
                            Rensou4Data addData = new Rensou4Data();
                            addData.mondaibun = spl2[i * yokoMax + 1];
                            addData.answer = spl2[i * yokoMax + 2];
                            addData.dummy = spl2[i * yokoMax + 3];
                            addData.series = (Series)(System.Int32.Parse(spl2[i * yokoMax + 5]) - 1);
                            addData.sakuhin = spl2[i * yokoMax + 7];
                            addData.nanido = System.Int32.Parse(spl2[i * yokoMax + 8]);
                            addData.sakumonsha = "";
                            addData.joken = spl2[i * yokoMax + 10];
                            readedRensou4Data.Add(addData);
                        }
                    }
                }
            }
            else if (sheetName == "問題：2択")
            {
                readedNitakuData.Clear();
                int yokoMax = 11;
                for (int i = 0; i < (spl2.Count / yokoMax); i++)
                {
                    if (i != 0)
                    {
                        if (spl2[i * yokoMax + 1].Contains("_")) continue;
                        if (spl2[i * yokoMax + 1].Contains("*")) continue;
                        if (spl2[i * yokoMax + 2].Contains("*")) continue;
                        if (spl2[i * yokoMax + 9] != "0")
                        {
                            NitakuData addData = new NitakuData();
                            addData.mondaibun = spl2[i * yokoMax + 1];
                            addData.answer = spl2[i * yokoMax + 2];
                            addData.dummy = spl2[i * yokoMax + 3];
                            addData.series = (Series)(System.Int32.Parse(spl2[i * yokoMax + 5]) - 1);
                            addData.sakuhin = spl2[i * yokoMax + 7];
                            addData.nanido = System.Int32.Parse(spl2[i * yokoMax + 8]);
                            addData.sakumonsha = "";
                            addData.joken = spl2[i * yokoMax + 10];
                            readedNitakuData.Add(addData);
                        }
                    }
                }
            }
            else if (sheetName == "問題：順番当て")
            {
                readedJunbanData.Clear();
                int yokoMax = 11;
                for (int i = 0; i < (spl2.Count / yokoMax); i++)
                {
                    if (i != 0)
                    {
                        if (spl2[i * yokoMax + 1].Contains("_")) continue;
                        if (spl2[i * yokoMax + 1].Contains("*")) continue;
                        if (spl2[i * yokoMax + 2].Contains("*")) continue;
                        if (spl2[i * yokoMax + 9] != "0")
                        {
                            JunbanData addData = new JunbanData();
                            addData.mondaibun = spl2[i * yokoMax + 1];
                            addData.sentakushi = spl2[i * yokoMax + 2];
                            addData.kazu = System.Int32.Parse(spl2[i * yokoMax + 3]);
                            addData.series = (Series)(System.Int32.Parse(spl2[i * yokoMax + 5]) - 1);
                            addData.sakuhin = spl2[i * yokoMax + 7];
                            addData.nanido = System.Int32.Parse(spl2[i * yokoMax + 8]);
                            addData.sakumonsha = "";
                            addData.joken = spl2[i * yokoMax + 10];
                            readedJunbanData.Add(addData);
                        }
                    }
                }
            }
            else if (sheetName == "問題：一問多答")
            {
                readedTatoData.Clear();
                int yokoMax = 11;
                for (int i = 0; i < (spl2.Count / yokoMax); i++)
                {
                    if (i != 0)
                    {
                        if (spl2[i * yokoMax + 1].Contains("_")) continue;
                        if (spl2[i * yokoMax + 1].Contains("*")) continue;
                        if (spl2[i * yokoMax + 2].Contains("*")) continue;
                        if (spl2[i * yokoMax + 9] != "0")
                        {
                            TatoData addData = new TatoData();
                            addData.mondaibun = spl2[i * yokoMax + 1];
                            addData.answer = spl2[i * yokoMax + 2];
                            addData.dummy = spl2[i * yokoMax + 3];
                            addData.kazu = 4;
                            addData.series = (Series)(System.Int32.Parse(spl2[i * yokoMax + 5]) - 1);
                            addData.sakuhin = spl2[i * yokoMax + 7];
                            addData.nanido = System.Int32.Parse(spl2[i * yokoMax + 8]);
                            addData.sakumonsha = "";
                            addData.joken = spl2[i * yokoMax + 10];
                            readedTatoData.Add(addData);
                        }
                    }
                }
            }
            else if (sheetName == "問題：線つなぎ")
            {
                int senAnswerMax = 7;
                readedSentsunagiData.Clear();
                int yokoMax = 10 + senAnswerMax;
                for (int i = 0; i < (spl2.Count / yokoMax); i++)
                {
                    if (i != 0)
                    {
                        if (spl2[i * yokoMax + 1].Contains("_")) continue;
                        if (spl2[i * yokoMax + 1].Contains("*")) continue;
                        if (spl2[i * yokoMax + 2].Contains("*")) continue;
                        if (spl2[i * yokoMax + 8 + senAnswerMax] != "0")
                        {
                            SentsunagiData addData = new SentsunagiData();
                            addData.mondaibun = spl2[i * yokoMax + 1];
                            for (int j=0;j< senAnswerMax;j++)
                            {
                                if (spl2[i * yokoMax + 2 + j] != "")
                                {
                                    List<string> getAns = KaigyoFix(spl2[i * yokoMax + 2 + j]);
                                    addData.leftAnswer.Add(getAns[0]);
                                    addData.rightAnswer.Add(getAns[1]);
                                }
                            }
                            addData.kazu = System.Int32.Parse(spl2[i * yokoMax + 2 + senAnswerMax]);
                            addData.series = (Series)(System.Int32.Parse(spl2[i * yokoMax + 4 + senAnswerMax]) - 1);
                            addData.sakuhin = spl2[i * yokoMax + 6 + senAnswerMax];
                            addData.nanido = System.Int32.Parse(spl2[i * yokoMax + 7 + senAnswerMax]);
                            addData.sakumonsha = "";
                            addData.joken = spl2[i * yokoMax + 9 + senAnswerMax];
                            readedSentsunagiData.Add(addData);
                        }
                    }
                }
            }
            else if (sheetName == "検出用年表")
            {
                readedNenpyoData.Clear();
                readedSeriesData.Clear();
                int yokoMax = 5;
                for (int i = 0; i < (spl2.Count / yokoMax); i++)
                {
                    if (i > 1)
                    {
                        readedNenpyoData.Add(spl2[i * yokoMax + 1]);
                        readedSeriesData.Add(int.Parse(spl2[i * yokoMax + 4]));
                    }
                }
            }
            request.Dispose();
        }
    }

    public List<string> KaigyoFix(string moto)
    {
        string[] kaigyo = { "\n" };
        var splited = moto.Split(kaigyo, System.StringSplitOptions.None);
        List<string> ret = new List<string>();
        for (int i=0;i<splited.Length;i++)
        {
            ret.Add(splited[i]);
        }
        return ret;
    }

    public IEnumerator ReadSheet(int id)
    {
        yield return ReadSpreadSheet(id);
    }
    /*
    public IEnumerator PostData(string address)
    {
        // https://script.google.com/macros/s/AKfycbxATKmXEEe_jFXRUh-Umo7gOdoPGh3W4O_ZbtMFUG4yfkhFM7HF00f7bM9H0skGB42o/exec



        Debug.Log("データ送信開始・・・");
        var form = new WWWForm();
        form.AddField("address", "AAA");

        //const string s_ID = "1b9Rjv1Q7vgTmW1AsR3evKMYhoBaYn2cUQeqqh_2N4gI";
        const string accesskey = "AKfycbz9QdsV66tNyOQjJh3VZSknjrBsAlotQY8k_buPOcW5dY8CSrWdD6CiktabbAN0vLtRPQ";
        //const string sheetName = "テスト";
        //var request = UnityWebRequest.Post("https://docs.google.com/spreadsheets/d/" + s_ID + "/gviz/tq?tqx=out:csv&sheet=" + sheetName, form);
        var request = UnityWebRequest.Post("https://script.google.com/macros/s/" + accesskey + "/exec", form);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.responseCode == 200)
            {
                request.Dispose();
                Debug.Log("データ送信成功！");

            }
            else
            {
                Debug.LogError("データ送信失敗" + request.responseCode);
                request.Dispose();
            }
        }
        else
        {
            Debug.Log(request.error);
            request.Dispose();
        }
    }
    */

    // ==============================================================================================
    // プレイヤーデータの読み書き
    // ==============================================================================================

    public bool postedData = false;
    public string postRequest = "NO";

    private IEnumerator ReadDataSpreadSheet(string id)
    {
        postedData = false;

        const string sheetName = "PlayerData";
        const string s_ID = "1ihJzAiiixmQHAG4q3TIM13VkTsgw9FkQQ29PU2nZTtw";
        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + s_ID + "/gviz/tq?tqx=out:csv&sheet=" + sheetName);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("ERROR");
            Debug.Log(request.error);
            request.Dispose();
        }
        else
        {
            Debug.Log("データスプシの読み込み");
            // Debug.Log(request.downloadHandler.text);
            readedData = request.downloadHandler.text;

            var spl1 = readedData.Split(',');

            string[] del = { "\n\"" };
            List<string> spl2 = new List<string>();
            for (int i = 0; i < spl1.Length; i++)
            {
                var spl2Add = spl1[i].Split(del, System.StringSplitOptions.None);
                for (int j = 0; j < spl2Add.Length; j++)
                {
                    spl2.Add(spl2Add[j].Replace("\"", ""));
                }
            }

            List<int> scoreList = new List<int>();
            int yokoMax = 16;
            for (int i = 0; i < (spl2.Count / yokoMax); i++)
            {
                if ((spl2[i * yokoMax + 5] != "") && (spl2[i * yokoMax + 5] != "MAX_ACA") && (spl2[i * yokoMax + 5] != "0"))
                    scoreList.Add(int.Parse(spl2[i * yokoMax + 5]));

                if (spl2[i * yokoMax + 0] == id)
                {
                    readedRecordData.id = i;
                    readedRecordData.address = spl2[i * yokoMax + 0];
                    readedRecordData.name = spl2[i * yokoMax + 1];
                    readedRecordData.pass = spl2[i * yokoMax + 2];
                    readedRecordData.open = spl2[i * yokoMax + 3];
                    readedRecordData.open_num = 0;
                    if (spl2[i * yokoMax + 4] != "")
                        readedRecordData.open_num = int.Parse(spl2[i * yokoMax + 4]);
                    readedRecordData.maxAcademyMode = spl2[i * yokoMax + 5];
                    readedRecordData.maxHayaoshiMode = spl2[i * yokoMax + 6];
                    readedRecordData.maxSeries = spl2[i * yokoMax + 7];
                    readedRecordData.maxSeriesCollect = spl2[i * yokoMax + 8];
                    readedRecordData.maxKeishiki = spl2[i * yokoMax + 9];
                    readedRecordData.maxKeishikiCollect = spl2[i * yokoMax + 10];
                    readedRecordData.maxNanido = spl2[i * yokoMax + 11];
                    readedRecordData.maxNanidoCollect = spl2[i * yokoMax + 12];
                    readedRecordData.zukan = spl2[i * yokoMax + 13];
                    readedRecordData.zukan_num = 0;
                    if (spl2[i * yokoMax + 14] != "")
                        readedRecordData.zukan_num = int.Parse(spl2[i * yokoMax + 14]);
                    readedRecordData.music = spl2[i * yokoMax + 15];
                }
            }
            if (readedRecordData.address != id)
            {
                readedRecordData.id = -1;
                readedRecordData.address = id;
            }

            scoreList.Sort((a, b) => b - a);
            if ((readedRecordData.maxAcademyMode != "") && (readedRecordData.maxAcademyMode != "0"))
                readedRecordData.rank = scoreList.IndexOf(int.Parse(readedRecordData.maxAcademyMode)) + 1;

            request.Dispose();
            if (hasRecordData == false)
                if (recordDataBase != null)
                    hasRecordData = true;
        }
    }

    public IEnumerator ReadDataSheet(string pcId)
    {
        yield return ReadDataSpreadSheet(pcId);
    }

    public int CountChar(string s, char c)
    {
        return s.Length - s.Replace(c.ToString(), "").Length;
    }

    public IEnumerator PostDataData(string pcId, string name)
    {
        Debug.Log("データ送信開始・・・");

        readedRecordData.address = pcId;
        readedRecordData.name = name;

        var form = new WWWForm();
        form.AddField("id", readedRecordData.id);
        form.AddField("pc", readedRecordData.address);
        form.AddField("name", readedRecordData.name);
        form.AddField("pass", readedRecordData.pass);
        form.AddField("open", "'" + readedRecordData.open);
        form.AddField("open_num", readedRecordData.open_num);
        form.AddField("max_aca", readedRecordData.maxAcademyMode);
        form.AddField("max_haya", readedRecordData.maxHayaoshiMode);
        form.AddField("series_m", readedRecordData.maxSeries);
        form.AddField("series_c", readedRecordData.maxSeriesCollect);
        form.AddField("keishiki_m", readedRecordData.maxKeishiki);
        form.AddField("keishiki_c", readedRecordData.maxKeishikiCollect);
        form.AddField("nanido_m", readedRecordData.maxNanido);
        form.AddField("nanido_c", "'" + readedRecordData.maxNanidoCollect);
        form.AddField("zukan", "'" + readedRecordData.zukan);
        form.AddField("zukan_num", readedRecordData.zukan_num.ToString());
        form.AddField("music", readedRecordData.music);

        const string accesskey = "AKfycby4na2LpuXPsHuVw3ESX3OmDBc9KnICBmgSGhUIlkf7YWnd4RuyvNN5-d-2ZZIFgr52yg";
        var request = UnityWebRequest.Post("https://script.google.com/macros/s/" + accesskey + "/exec", form);

        yield return request.SendWebRequest();

        postRequest = request.responseCode.ToString();
        if (request.result == UnityWebRequest.Result.Success)
        {
            if (request.responseCode == 200)
            {
                request.Dispose();
                Debug.Log("データ送信成功！");
                postedData = true;

            }
            else
            {
                Debug.LogError("データ送信失敗_" + request.responseCode);
                request.Dispose();
                postedData = true;
            }
        }
        else
        {
            Debug.LogError("データ送信失敗2_" + request.responseCode);
            Debug.Log(request.error);
            request.Dispose();
            postedData = true;
        }
    }

    // ==============================================================================================
    // プレイヤーデータの作成と変換
    // ==============================================================================================

    public static List<List<ToRecordData>> recordDataBase;

    public void RecordInitialize()
    {
        if (recordDataBase == null)
        {
            string juroku = readedRecordData.open;

            recordDataBase = new List<List<ToRecordData>>();
            for (int i = 0; i < mondaiIdList.Length - 1; i++)
            {
                recordDataBase.Add(new List<ToRecordData>());
                switch ((Keishiki)i)
                {
                    case Keishiki.Typing:
                        for (int j = 0; j < readedTypingData.Count; j++)
                        {
                            ToRecordData appendData = new ToRecordData();
                            appendData.id = -1;
                            appendData.looked = false;
                            appendData.firstCollect = false;
                            appendData.keishiki = Keishiki.Typing;
                            appendData.series = Series.None;
                            appendData.nanido = 0;
                            recordDataBase[i].Add(appendData);
                        }
                        break;
                    case Keishiki.Yontaku:
                        for (int j = 0; j < readedYontakuData.Count; j++)
                        {
                            ToRecordData appendData = new ToRecordData();
                            appendData.id = -1;
                            appendData.looked = false;
                            appendData.firstCollect = false;
                            appendData.keishiki = Keishiki.Yontaku;
                            appendData.series = Series.None;
                            appendData.nanido = 0;
                            recordDataBase[i].Add(appendData);
                        }
                        break;
                    case Keishiki.Marubatsu:
                        for (int j = 0; j < readedMarubatsuData.Count; j++)
                        {
                            ToRecordData appendData = new ToRecordData();
                            appendData.id = -1;
                            appendData.looked = false;
                            appendData.firstCollect = false;
                            appendData.keishiki = Keishiki.Marubatsu;
                            appendData.series = Series.None;
                            appendData.nanido = 0;
                            recordDataBase[i].Add(appendData);
                        }
                        break;
                    case Keishiki.Rensou4:
                        for (int j = 0; j < readedRensou4Data.Count; j++)
                        {
                            ToRecordData appendData = new ToRecordData();
                            appendData.id = -1;
                            appendData.looked = false;
                            appendData.firstCollect = false;
                            appendData.keishiki = Keishiki.Rensou4;
                            appendData.series = Series.None;
                            appendData.nanido = 0;
                            recordDataBase[i].Add(appendData);
                        }
                        break;
                    case Keishiki.Nitaku:
                        for (int j = 0; j < readedNitakuData.Count; j++)
                        {
                            ToRecordData appendData = new ToRecordData();
                            appendData.id = -1;
                            appendData.looked = false;
                            appendData.firstCollect = false;
                            appendData.keishiki = Keishiki.Nitaku;
                            appendData.series = Series.None;
                            appendData.nanido = 0;
                            recordDataBase[i].Add(appendData);
                        }
                        break;
                    case Keishiki.Junban:
                        for (int j = 0; j < readedJunbanData.Count; j++)
                        {
                            ToRecordData appendData = new ToRecordData();
                            appendData.id = -1;
                            appendData.looked = false;
                            appendData.firstCollect = false;
                            appendData.keishiki = Keishiki.Junban;
                            appendData.series = Series.None;
                            appendData.nanido = 0;
                            recordDataBase[i].Add(appendData);
                        }
                        break;
                    case Keishiki.Tato:
                        for (int j = 0; j < readedTatoData.Count; j++)
                        {
                            ToRecordData appendData = new ToRecordData();
                            appendData.id = -1;
                            appendData.looked = false;
                            appendData.firstCollect = false;
                            appendData.keishiki = Keishiki.Tato;
                            appendData.series = Series.None;
                            appendData.nanido = 0;
                            recordDataBase[i].Add(appendData);
                        }
                        break;
                    case Keishiki.Sentsunagi:
                        for (int j = 0; j < readedSentsunagiData.Count; j++)
                        {
                            ToRecordData appendData = new ToRecordData();
                            appendData.id = -1;
                            appendData.looked = false;
                            appendData.firstCollect = false;
                            appendData.keishiki = Keishiki.Sentsunagi;
                            appendData.series = Series.None;
                            appendData.nanido = 0;
                            recordDataBase[i].Add(appendData);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        if ((hasData) && (hasRecordData == false))
        {
            RecordToPlayerData();
            QuizDataToRecord();
            if (recordDataBase != null)
                hasRecordData = true;
            else
                hasRecordData = false;
        }
    }

    private void QuizDataToRecord()
    {
        for (int i = 0; i < recordDataBase.Count; i++)
        {
            for (int j = 0; j < recordDataBase[i].Count; j++)
            {
                switch ((Keishiki)i)
                {
                    case Keishiki.Typing:
                        recordDataBase[i][j].product = int.Parse(readedTypingData[j].sakuhin);
                        recordDataBase[i][j].nanido = readedTypingData[j].nanido;
                        break;
                    case Keishiki.Yontaku:
                        recordDataBase[i][j].product = int.Parse(readedYontakuData[j].sakuhin);
                        recordDataBase[i][j].nanido = readedYontakuData[j].nanido;
                        break;
                    case Keishiki.Marubatsu:
                        recordDataBase[i][j].product = int.Parse(readedMarubatsuData[j].sakuhin);
                        recordDataBase[i][j].nanido = readedMarubatsuData[j].nanido;
                        break;
                    case Keishiki.Rensou4:
                        recordDataBase[i][j].product = int.Parse(readedRensou4Data[j].sakuhin);
                        recordDataBase[i][j].nanido = readedRensou4Data[j].nanido;
                        break;
                    case Keishiki.Nitaku:
                        recordDataBase[i][j].product = int.Parse(readedNitakuData[j].sakuhin);
                        recordDataBase[i][j].nanido = readedNitakuData[j].nanido;
                        break;
                    case Keishiki.Junban:
                        recordDataBase[i][j].product = int.Parse(readedJunbanData[j].sakuhin);
                        recordDataBase[i][j].nanido = readedJunbanData[j].nanido;
                        break;
                    case Keishiki.Tato:
                        recordDataBase[i][j].product = int.Parse(readedTatoData[j].sakuhin);
                        recordDataBase[i][j].nanido = readedTatoData[j].nanido;
                        break;
                    case Keishiki.Sentsunagi:
                        recordDataBase[i][j].product = int.Parse(readedSentsunagiData[j].sakuhin);
                        recordDataBase[i][j].nanido = readedSentsunagiData[j].nanido;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void RecordDataSet(ToRecordData data, int nowProduct)
    {
        data.looked = true;

        if (recordDataBase[(int)data.keishiki][data.id].looked)
            data.firstCollect = recordDataBase[(int)data.keishiki][data.id].firstCollect;
        recordDataBase[(int)data.keishiki][data.id] = data;

        ZukanOpen(nowProduct);
        ZukanToRecord();
    }

    // ==============================================================================================
    // プレイヤーデータを保存用データに変換
    // ==============================================================================================
    public void DataToRecord()
    {
        int openCount = 0;
        int collectCount = 0;
        string allData = "";
        for (int i = 0; i < recordDataBase.Count; i++)
        {
            string oneData = "";
            // マルバツデータをまず2進数にする
            for (int j = 0; j < recordDataBase[i].Count; j++)
            {
                if (recordDataBase[i][j].looked)
                {
                    oneData += "1";
                    openCount++;
                }
                else oneData += "0";
                if (recordDataBase[i][j].firstCollect)
                {
                    oneData += "1";
                    collectCount++;
                }
                else oneData += "0";
            }
            // 4文字ずつ扱いたいので、リストが4の倍数じゃなかったら余分な0を入れる
            if (oneData.Length % 4 != 0)
            {
                for (int j = 0; j < (4 - (oneData.Length % 4)) + 1; j++)
                {
                    oneData += "0";
                }
            }
            // 2進数データを4文字ずつ16進数に変換する
            for (int j = 0;j< oneData.Length - 5;j+=4)
            {
                string one = oneData.Substring(j, 4);
                string two = System.Convert.ToString(System.Convert.ToInt32(one, 2), 16);
                allData += two;
            }
            // Debug.Log(allData);
            if (i != recordDataBase.Count - 1) allData += "_";
        }

        readedRecordData.open = allData;
        readedRecordData.open_num = openCount;
        Debug.Log("collectCount = " + collectCount.ToString());
    }

    // ==============================================================================================
    // 保存用データをプレイヤーデータに変換
    // ==============================================================================================
    private void RecordToPlayerData()
    {
        string openOrigin = readedRecordData.open;

        var openList = openOrigin.Split('_');
        int lookedCount = 0;
        int collectCount = 0;

        for (int h = 0; h < openList.Length; h++)
        {
            var openOne = openList[h];
            if ((openOne != "") && (openOne != "0"))
            {
                for (int i = 0; i < openOne.Length; i++)
                {
                    int bitLength = 4;
                    var zukanOne = openOne.Substring(i, 1);
                    if (zukanOne != "0")
                    {
                        var zukanHex = int.Parse(zukanOne, System.Globalization.NumberStyles.HexNumber);
                        string digital = System.Convert.ToString(zukanHex, 2).PadLeft(bitLength, '0');

                        if (digital.Length >= 4)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                int index = i * 2 + j;
                                if (index >= recordDataBase[h].Count)
                                    break;
                                int looked = int.Parse(digital.Substring(j * 2, 1));
                                lookedCount += looked;
                                recordDataBase[h][index].looked = (looked == 1);

                                int collect = int.Parse(digital.Substring(j * 2 + 1 , 1));
                                collectCount += collect;
                                recordDataBase[h][index].firstCollect = (collect == 1);
                            }
                        }
                    }
                }
            }
        }
    }

    public void SendPointAcademic(string point)
    {
        readedRecordData.maxAcademyMode = point;
    }

    // ==============================================================================================
    // 図鑑解放
    // ==============================================================================================

    public void ZukanOpen(int productID)
    {
        if (hasLibraryData)
        {
            List<int> useIDList = new List<int>();

            for (int i = 0; i < readedLibraryData.Count; i++)
            {
                if (readedLibraryData[i].open == false)
                {
                    if (readedLibraryData[i].product == productID - 1)
                    {
                        useIDList.Add(i);
                    }
                }
            }

            int openID = -1;
            Debug.Log(productID);
            if (useIDList.Count > 0)
            {
                openID = Random.Range(0, useIDList.Count);
                readedLibraryData[useIDList[openID]].open = true;
                Debug.Log(useIDList[openID]);
            }
            else
            {
                openID = Random.Range(0, readedLibraryData.Count);
                readedLibraryData[openID].open = true;
                Debug.Log(readedLibraryData[openID]);
            }
        }

        //ZukanToRecord();
        //StartCoroutine(PostDataData(readedRecordData.address, readedRecordData.name));
    }

    public void ZukanOpenFromAll(int productID)
    {
        if (hasLibraryData)
        {
            readedLibraryData[productID].open = true;
        }

        ZukanToRecord();
    }

    public void ZukanToRecord()
    {
        if (hasLibraryData)
        {
            string oneData = "";
            string allData = "";
            int zukanCount = 0;
            // マルバツデータをまず2進数にする
            for (int i = 0; i < readedLibraryData.Count; i++)
            {
                int getData = System.Convert.ToInt32(readedLibraryData[i].open);
                oneData += getData.ToString();
                zukanCount += getData;
            }
            // 4文字ずつ扱いたいので、リストが4の倍数じゃなかったら余分な0を入れる
            if (oneData.Length % 4 != 0)
            {
                while(oneData.Length % 4 != 0)
                {
                    oneData += "0";
                }
            }
            // 2進数データを4文字ずつ16進数に変換する
            for (int j = 0; j < oneData.Length - 5; j += 4)
            {
                string one = oneData.Substring(j, 4);
                string two = System.Convert.ToInt32(one,2).ToString("X");
                allData += two;
            }
            readedRecordData.zukan = allData;
            readedRecordData.zukan_num = zukanCount;
        }
    }

    // ==============================================================================================
    // 図鑑データの読み込み
    // ==============================================================================================

    private IEnumerator ReadLibrarySpreadSheet()
    {
        const string s_ID = "1fstBHnrnmhCLKH9mx-nyn99hNnGzz2-WGQuD4WXztwo";
        {
            string sheetName = "ゲームとか図鑑";
            UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + s_ID + "/gviz/tq?tqx=out:csv&sheet=" + sheetName);
            yield return request.SendWebRequest();


            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("ERROR");
                Debug.Log(request.error);
                request.Dispose();
            }
            else
            {
                readedData = request.downloadHandler.text;

                var spl1 = readedData.Split(',');

                string[] del = { "\n\"" };
                List<string> spl2 = new List<string>();
                for (int i = 0; i < spl1.Length; i++)
                {
                    var spl2Add = spl1[i].Split(del, System.StringSplitOptions.None);
                    for (int j = 0; j < spl2Add.Length; j++)
                    {
                        spl2.Add(spl2Add[j].Replace("\"", ""));
                    }
                }

                if (hasLibraryData == false)
                    hasLibraryData = true;

                readedLibraryData.Clear();
                int yokoMax = 14;
                for (int i = 0; i < (spl2.Count / yokoMax); i++)
                {
                    if (i != 0)
                    {
                        LibraryData addData = new LibraryData();
                        addData.name = spl2[i * yokoMax + 1];
                        //Debug.Log(addData.name);
                        addData.furigana = spl2[i * yokoMax + 2];
                        addData.series = (Series)(System.Int32.Parse(spl2[i * yokoMax + 4]) - 1);
                        addData.product = (System.Int32.Parse(spl2[i * yokoMax + 6]) - 1);
                        addData.genre = spl2[i * yokoMax + 7];
                        addData.start = spl2[i * yokoMax + 8];
                        addData.end = spl2[i * yokoMax + 9];
                        addData.hard = spl2[i * yokoMax + 10];
                        addData.bun = spl2[i * yokoMax + 11];
                        addData.rare = (System.Int32.Parse(spl2[i * yokoMax + 12]) - 1);
                        readedLibraryData.Add(addData);
                    }
                }
            SetZukanOpen();
            request.Dispose();
            }
        }
    }

    public IEnumerator ReadLibrary()
    {
        yield return ReadLibrarySpreadSheet();
    }

    private void SetZukanOpen()
    {
        string zukanOrigin = readedRecordData.zukan;
        Debug.Log(readedLibraryData.Count);
        if ((zukanOrigin != "") && (zukanOrigin != "0"))
        {
            Debug.Log("zukanOrigin.Length");
            Debug.Log(zukanOrigin.Length);
            for (int i=0;i< zukanOrigin.Length;i++)
            {
                int bitLength = 4;
                var zukanOne = zukanOrigin.Substring(i, 1);
                // Debug.Log(zukanOne);
                var zukanHex = int.Parse(zukanOne, System.Globalization.NumberStyles.HexNumber);
                string digital = System.Convert.ToString(zukanHex, 2).PadLeft(bitLength, '0');
                
                if (digital.Length >= 4)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        int index = i * 4 + j;
                        if (index >= readedLibraryData.Count)
                            break;
                        readedLibraryData[index].open = (digital.Substring(j, 1) == "1");
                    }
                }
            }
        }
        
    }

    //// ==============================================================================================
    //// サウンドテスト用文書データの読み込み
    //// ==============================================================================================

    //private IEnumerator ReadSoundTestSpreadSheet()
    //{
    //    const string s_ID = "1b9Rjv1Q7vgTmW1AsR3evKMYhoBaYn2cUQeqqh_2N4gI";
    //    {
    //        string sheetName = "サウンドテスト";
    //        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + s_ID + "/gviz/tq?tqx=out:csv&sheet=" + sheetName);
    //        yield return request.SendWebRequest();


    //        if (request.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log("ERROR");
    //            Debug.Log(request.error);
    //            request.Dispose();
    //        }
    //        else
    //        {
    //            readedData = request.downloadHandler.text;

    //            var spl1 = readedData.Split(',');

    //            string[] del = { "\n\"" };
    //            List<string> spl2 = new List<string>();
    //            for (int i = 0; i < spl1.Length; i++)
    //            {
    //                var spl2Add = spl1[i].Split(del, System.StringSplitOptions.None);
    //                for (int j = 0; j < spl2Add.Length; j++)
    //                {
    //                    spl2.Add(spl2Add[j].Replace("\"", ""));
    //                }
    //            }

    //            if (hasSoundTestData == false)
    //                hasSoundTestData = true;

    //            readedSoundTestData.Clear();
    //            int yokoMax = 13;
    //            for (int i = 0; i < (spl2.Count / yokoMax); i++)
    //            {
    //                if (i != 0)
    //                {

    //                    SoundTestData addData = new SoundTestData();
    //                    addData.name = spl2[i * yokoMax + 1];
    //                    addData.furigana = spl2[i * yokoMax + 2];
    //                    addData.series = (Series)(System.Int32.Parse(spl2[i * yokoMax + 4]) - 1);
    //                    addData.reference = spl2[i * yokoMax + 5];
    //                    addData.arranger = spl2[i * yokoMax + 6];
    //                    addData.bpm = spl2[i * yokoMax + 7];
    //                    addData.place = spl2[i * yokoMax + 8];
    //                    addData.bun = spl2[i * yokoMax + 10];
    //                    addData.bun2 = spl2[i * yokoMax + 11];
    //                    readedSoundTestData.Add(addData);
    //                }
    //            }

    //            request.Dispose();
    //        }
    //    }
    //}

    //public IEnumerator ReadSoundTest()
    //{
    //    yield return ReadSoundTestSpreadSheet();
    //}

    //private IEnumerator ReadImageSpreadSheet()
    //{
    //    const string s_ID = "1b9Rjv1Q7vgTmW1AsR3evKMYhoBaYn2cUQeqqh_2N4gI";
    //    {
    //        string sheetName = "テスト";
    //        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + s_ID + "/gviz/tq?tqx=out:csv&sheet=" + sheetName);
    //        yield return request.SendWebRequest();


    //        if (request.result != UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log("ERROR");
    //            Debug.Log(request.error);
    //            request.Dispose();
    //        }
    //        else
    //        {
    //            readedData = request.downloadHandler.text;
    //            // Debug.Log(readedData);
    //            var spl1 = readedData.Split(',');

    //            string[] del = { "\n\"" };
    //            //List<string> spl2 = new List<string>();
    //            for (int i = 0; i < spl1.Length; i++)
    //            {
    //                Texture texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    //                readedTextureData.Add(texture);
    //            }

    //            request.Dispose();
    //        }
    //    }
    //}

    //public IEnumerator ReadImage()
    //{
    //    yield return ReadImageSpreadSheet();
    //}


    public static string userId = "";
    private const string USER_ID_KEY = "UserID";

    public string GetOrCreateUserId()
    {
        while (userId == "")
        {
            if (!PlayerPrefs.HasKey(USER_ID_KEY))
            {
                userId = System.Guid.NewGuid().ToString();
                PlayerPrefs.SetString(USER_ID_KEY, userId);
                PlayerPrefs.Save();
            }
            else
            {
                userId = PlayerPrefs.GetString(USER_ID_KEY);
            }
        }
        return userId;
    }
}
