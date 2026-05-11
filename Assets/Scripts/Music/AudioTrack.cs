using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioTrack", menuName = "CreatedDataBase/AudioTrack", order = 1)]
public class AudioTrack : ScriptableObject
{ 
    public enum LoopType
    {
        None,           // ループしない
        LastTrack,      // 配列最後のトラックをループさす
        IntroRandom,    // 最初のトラックをイントロとし、以降うしろの物をランダムで矢継ぎ早に
        AllRandom,      // 全部ランダム
    }
    public LoopType loopType;
    public List<AudioClip> audioClipList;
    public List<float> isLoopPointList;     // 次のトラックにいく時間

    private int nowClip = 0;
    private int nextClip = -1;


    public AudioClip GetStartClip()
    {
        if (loopType == LoopType.AllRandom)
        {
            return RandomClip();
        }
        else
        {
            nowClip = 0;
            nextClip = (nowClip + 1) % audioClipList.Count;
            return audioClipList[nowClip];
        }
    }

    public AudioClip GetClip()
    {
        if ((loopType == LoopType.AllRandom) || (loopType == LoopType.IntroRandom))
        {
            return RandomClip();
        }
        else
        {
            nowClip = nextClip;
            if ((loopType == LoopType.LastTrack) && (audioClipList.Count - 1 == nowClip))
            {
                return audioClipList[nowClip];
            }
            else
            {
                nextClip = nowClip + 1;
                return audioClipList[nowClip];
            }
        }
    }

    public AudioClip RandomClip()
    {
        int randMax = audioClipList.Count;
        if (loopType == LoopType.IntroRandom)
        {
            randMax = randMax - 1;
        }

        if (nextClip == -1)
        {
            nowClip = Random.Range(0, randMax);
        }
        else
        {
            while (nextClip == nowClip)
            {
                nextClip = Random.Range(0, randMax);
            }
            nowClip = nextClip;
        }

        if (loopType == LoopType.IntroRandom)
        {
            nowClip = nowClip + 1;
        }

        nextClip = nowClip;
        return audioClipList[nowClip];
    }

    public void RandomNext()
    {
        nextClip = nowClip;
        while (nextClip == nowClip)
        {
            nextClip = Random.Range(0, audioClipList.Count);
        }
    }

    public float GetNextPoint()
    {
        if (isLoopPointList.Count == 0)
            return 0.0f;
        return isLoopPointList[nowClip];
    }
}
