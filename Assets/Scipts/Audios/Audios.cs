using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Audios : MonoBehaviour
{
    public Clips[] sfxClips, soundClips;
    public AudioSource sfxSource, soundSource;
    public static Audios audios;
    public Button sfxbuttonON, soundbuttonON;
    private bool sfxtrue, soundtrue;

    private void Awake()
    {
        if (audios == null)
        {
            audios = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void sfxButtonPlay(string clip_Name)
    {
        Clips clips = Array.Find(sfxClips, x => x.clips == clip_Name);
        if (clips.Equals(null)) print("Not find sfx clip.");
        else sfxSource.PlayOneShot(clips.audios);
    }
    public void sfxSound()
    {
        if (sfxtrue)
        {
            FindAnyObjectByType<GameManager>().btnAnimate[2].Play("sound0");
            sfxbuttonON.image.color = Color.gray;
            sfxtrue = false;
            sfxSource.mute = true;
        }
        else
        {
            FindAnyObjectByType<GameManager>().btnAnimate[2].Play("sound");
            sfxbuttonON.image.color = new Color(255, 255, 255, 255);
            sfxtrue = true;
            sfxSource.mute = false;
        }
    }

    public void soundClipPlay(string clip_Name)
    {
        Clips clips = Array.Find(soundClips, x => x.clips == clip_Name);
        if (clips.Equals(null)) print("Not find sound clip.");
        else
        {
            soundSource.clip = clips.audios;
            soundSource.Play();
        }
    }

    public void clipSound()
    {
        if (soundtrue)
        {
            FindAnyObjectByType<GameManager>().btnAnimate[3].Play("music0");
            soundbuttonON.image.color = Color.gray;
            soundtrue = false;
            soundSource.mute = true;
        }
        else
        {
            FindAnyObjectByType<GameManager>().btnAnimate[3].Play("music");
            soundbuttonON.image.color = new Color(255, 255, 255, 255);
            soundtrue = true;
            soundSource.mute = false;
        }
    }

}
