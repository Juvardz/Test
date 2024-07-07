using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Sound
{
  [SerializeField]
  string name;

  [SerializeField]
  AudioClip audio;

  public string GetName()
  {
    return name;
  }

  public AudioClip GetAudio()
  {
    return audio;
  }
}