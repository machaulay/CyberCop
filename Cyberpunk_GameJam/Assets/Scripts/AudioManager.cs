using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake() {
        if (instance = null){
            instance = this;
        }else{
            return;
        }
        
        DontDestroyOnLoad(gameObject);        
    }

    public void Play(AudioClip clip, float volume = 1.0f, float pitch = 1.0f) {
        foreach (AudioSource source in GetComponentsInChildren<AudioSource>()) {
            if (source.isPlaying == false) {
                source.clip = clip;
                source.volume = volume;
                source.pitch = pitch;
                source.Play();
                break;
            }
        }
    }
    public void Play2(AudioClip clip, float volume = 1.0f, float pitch = 1.0f) {
        foreach (AudioSource source in GetComponentsInChildren<AudioSource>()) {
            if (source.isPlaying == false) {
                source.clip = clip;
                source.volume = volume;
                source.pitch = pitch;
                source.Play();
                break;
            }
        }
    }
}
