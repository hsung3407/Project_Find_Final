using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;
    
    [SerializeField] private AudioSource[] bgm;

    private void Awake()
    {
        Instance = this;
    }

    public void ChoiceMusic()
    {
        if (GameManager.stage == GameManager.Stage.BossStage) bgm[1].Play();
        else bgm[0].Play();
    }

    public void MusicInit()
    {
        bgm[0].Stop();
        bgm[1].Stop();
    }
}
