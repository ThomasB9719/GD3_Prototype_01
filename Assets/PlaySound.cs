using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    private AudioSource _soundSource;

    public void PlayAudio(AudioClip clip)
    {
        _soundSource.PlayOneShot(clip);
    }

    public void StopCurrentAudio()
    {
        _soundSource.Stop();
    }
}
