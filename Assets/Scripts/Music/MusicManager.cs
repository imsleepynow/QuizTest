using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public List<AudioTrack> audioTrackList;
    public AudioSource audioSource;

    private int nowChooseClip = 0;
    private bool isNowPlaying = false;

    float nowTime = 0;
    float medStartTime = 0;
    float nextPoint = -1;

    // Start is called before the first frame update
    void Start()
    {
        SetVolume(0.1f);
        ChooseMusic(0);
        StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
        nowTime += Time.deltaTime;
        if (nextPoint > 0)
        {
            if (isNowPlaying)
            {
                AutoNext();
            }
        }
    }

    public void ChooseMusic(int musicId)
    {
        nowChooseClip = musicId;
    }

    public int GetNowChooseClip()
    {
        return nowChooseClip;
    }

    public void PlayMusic()
    {
        if (isNowPlaying)
            audioSource.clip = audioTrackList[nowChooseClip].GetClip();
        else
            audioSource.clip = audioTrackList[nowChooseClip].GetStartClip();

        medStartTime = nowTime;
        SetVolume(0.2f);
        audioSource.Play();
        isNowPlaying = true;

        nextPoint = audioTrackList[nowChooseClip].GetNextPoint();
    }

    public void StopMusic()
    {
        audioSource.Stop();
        isNowPlaying = false;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void PlayNextMusic()
    {
        nowChooseClip += 1;
        nowChooseClip %= audioTrackList.Count;
        ChooseMusic(nowChooseClip);
        PlayMusic();
    }

    private void PlayMusic(int clipId)
    {
        ChooseMusic(clipId);
        PlayMusic();
    }

    private void AutoNext()
    {
        if (nowTime >= medStartTime + nextPoint)
        {
            PlayMusic();
        }
    }

    public bool IsNowPlaying()
    {
        return isNowPlaying;
    }
}
