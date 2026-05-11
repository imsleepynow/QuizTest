using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private int nowChooseClip = 0;
    private bool isNowPlaying = false;

    public GameObject musicManagerPrefav;
    private MusicManager musicManager = null;

    // Start is called before the first frame update
    void Start()
    {
        var music = Instantiate(musicManagerPrefav, transform);
        musicManager = music.GetComponent<MusicManager>();
        musicManager.ChooseMusic(0);
        musicManager.SetVolume(0.1f);
        musicManager.StopMusic();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayMusic(int clipId)
    {
        musicManager.ChooseMusic(clipId);
        musicManager.PlayMusic();
    }

    public void StopMusic()
    {
        musicManager.StopMusic();
    }

    public void DeleteMusic()
    {
        musicManager.StopMusic();
        //GameObject.Destroy(musicManagerPrefav);
    }

    public bool IsNowPlaying()
    {
        return musicManager.IsNowPlaying();
    }
}
