using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource bgm;
    public static float musicTime;

    private void Awake()
    {
        bgm = GetComponent<AudioSource>();
        bgm.time = musicTime;
    }    
    
    private void Update()
    {
        musicTime = bgm.time;
    }
}
