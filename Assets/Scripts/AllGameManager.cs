//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
////using Google.Apis.Auth.OAuth2;
////using Google.Apis.Sheets.v4;
////using Google.Apis.Sheets.v4.Data;
////using Google.Apis.Services;
////using Google.Apis.Util.Store;
//using System.IO;
//using System.Threading;
//using System;

//public class AllGameManager : MonoBehaviour
//{
//    //public Text mondaibun;
//    public static SpreadSheetCheck spreadSheetCheck;


//    //public ButtonManager yontaku;
//    public QuizManager quizManager;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Z))
//        {
//            Debug.Log("yahoo");
//            StartCoroutine(spreadSheetCheck.ReadSheet("問題：4択"));
//            // StartCoroutine(spreadSheetCheck.ReadSheet("問題：マルバツ"));
//        }

//        if (Input.GetKeyDown(KeyCode.X))
//        {
//            Debug.Log("ippy");
//            StartCoroutine(spreadSheetCheck.PostData("A1"));
//        }

//        if (Input.GetKeyDown(KeyCode.B))
//        {
//            Debug.Log("quiz");
//            string mondaibun_f = "テステステステス\nテステステステス\nテステステステス\nテスってなんかいいうた？";
//            quizManager.StartQuiz(mondaibun_f, QuizManager.QuizType.Typing);
//        }

//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            if (spreadSheetCheck.hasData)
//            {
//                var list = spreadSheetCheck.readedYontakuData;
//                int index = UnityEngine.Random.Range(0, list.Count);
//                quizManager.StartQuizYontaku(list[index].mondaibun, list[index].answer, list[index].dummy);
//            }
//            else
//            {
//                Debug.Log("問題を入れ込んでねえよい");
//            }
//        }
//        //if(yontaku.ButtonCheck() != 0)
//        //{
//        //    Debug.Log("nice");
//        //}


//        //mondaibun.text = spreadSheetCheck.readedData;
//    }
//}
