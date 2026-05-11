using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundTestManager : MonoBehaviour
{
    private int nowChooseClip = 0;
    private bool isNowPlaying = false;

    public Text title;
    public Text arranger;
    public Text bpm;
    public Text place;
    public Text bun;
    public Text bun2;

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
        //bun2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateSoundTest(SoundTestData data, int musicId)
    {
        title.text = data.name;
        arranger.text = data.arranger;
        bpm.text = data.bpm;
        place.text = data.place;

        bun.text = data.bun;

        Debug.Log(bun.text);

        musicManager.ChooseMusic(musicId);
        musicManager.SetVolume(0.1f);
        // musicManager.StopMusic();

        //shuttenObject.active = false;
    }

    public void PlayMusic()
    {
        musicManager.PlayMusic();
    }

    public void StopMusic()
    {
        musicManager.StopMusic();
    }

    public void PlayNextMusic()
    {
        musicManager.PlayNextMusic();
    }

    public int GetMusicCount()
    {
        return musicManager.audioTrackList.Count;
    }

    public void DeleteMusic()
    {
        musicManager.StopMusic();
        //GameObject.Destroy(musicManagerPrefav);
    }
}
