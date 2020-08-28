using UnityEngine.Audio;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public Sound[] music;
    public AudioClip[] clips;

    void Awake()
    {
        foreach (Sound s in music)
        {
          s.source = gameObject.AddComponent<AudioSource>();
          s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
 
        }
    }
    private void Start()
    {
        PlayRandom();
    }
    public void Play (string name)
    {
       Sound s = Array.Find(music, Sound => Sound.name == name);
        s.source.Play();
    }
    public void Stop (string name)
    {
        Sound s = Array.Find(music, Sound => Sound.name == name);
        s.source.Stop();
    }

    public void PlayRandom()
    {
        Sound s = music[Random.Range(0, 5)];
        s.source.Play();
    }

    public void Switch (string name)
    {

    }
}
