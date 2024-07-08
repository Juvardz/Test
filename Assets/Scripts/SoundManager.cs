using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  [SerializeField]
  Sound[] sounds;

  AudioSource _musicSource;
  AudioSource _sfxSource;

  private static SoundManager _instance;

  private void Start()
  {
    Playmusic("Theme");
  }
  private void Awake()
  {
    _instance = this;

    _musicSource = gameObject.AddComponent<AudioSource>();
    _musicSource.volume = 1.0F;

    _sfxSource = gameObject.AddComponent<AudioSource>();
    _sfxSource.volume = 1.0F;

  }

  private Sound FindSound(string name)
  {
    return Array.Find(sounds,s => s.GetName().Equals(name, StringComparison.OrdinalIgnoreCase));
  }

  public static SoundManager Instance
  {
    get{return _instance;}
  }

  public void Playmusic(string name)
  {
    Sound music = FindSound(name);
    if(music == null)
    {
        return;
    }

    _musicSource.loop = true;
    _musicSource.clip = music.GetAudio();
    _musicSource.Play();
  }

  public void PlaySFX(string name, bool loop = false)
  {
    Sound sfx =FindSound(name);
    if(sfx == null)
    {
        return;
    }

    if(loop)
    {
        _sfxSource.loop = true;
        _sfxSource.clip = sfx.GetAudio();
        _sfxSource.Play();
    }
    else
    {
        _sfxSource.PlayOneShot(sfx.GetAudio());
    }
  }

  public void StopMusic()
  {
    _musicSource.Stop();
  }

  public void StopSFX()
  {
    _sfxSource.Stop();
  }
}
