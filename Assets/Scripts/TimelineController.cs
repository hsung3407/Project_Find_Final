using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public static            TimelineController Instance;
    [SerializeField] private PlayableDirector[] stageIntro;
    [SerializeField] private PlayableDirector   stageEnd;
    [SerializeField] private PlayableDirector   bossCutScene;
    [SerializeField] private PlayableDirector   bossDeadScene;
    [SerializeField] private PlayableDirector   endingScene;
    [SerializeField] private PlayableDirector   playerDead;

    private GameManager.Stage currentStage;

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        currentStage = GameManager.stage;
        NextStage();
        StartCoroutine(StageChange());
    }

    IEnumerator StageChange()
    {
        while (true)
        {
            yield return new WaitUntil(() => currentStage != GameManager.stage);
            currentStage = GameManager.stage;
            if (currentStage == GameManager.Stage.Ending)
            {
                endingScene.Play();
                gameObject.SetActive(false);
                yield break;
            }

            stageEnd.Play();
        }
    }

    public void NextStage()
    {
        switch (currentStage)
        {
            case GameManager.Stage.Stage1:
                stageIntro[0].Play();
                break;
            case GameManager.Stage.Stage2:
                stageIntro[1].Play();
                break;
            case GameManager.Stage.Stage3:
                stageIntro[2].Play();
                break;
            case GameManager.Stage.Stage4:
                stageIntro[3].Play();
                break;
            case GameManager.Stage.BossStage:
                stageIntro[4].Play();
                break;
            case GameManager.Stage.BossDead:
                bossDeadScene.Play();
                break;
        }
    }


    public void BossPhaseChange()
    {
        bossCutScene.Play();
    }


    public void PlayerDead()
    {
        playerDead.Play();
    }
}