using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    private AudioSource bgMusic;
    public float delay = 1;
    private void Start() {
        bgMusic = GetComponent<AudioSource>();
        Invoke(nameof(SoundOn), delay);
    }
  private void SoundOn() {
        bgMusic.Play();
    }
}
