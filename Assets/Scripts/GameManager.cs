using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;





public class GameManager : Singleton<GameManager>
{
    public static bool gameStart;

    [SerializeField] private Slider bossHealth;
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider playerPotion;
    private WaitForSeconds healthDel = new WaitForSeconds(0.003f);
    [SerializeField] private GameObject potal;

    [SerializeField] private GameObject[] stageCam;


    public enum Stage
    {
        Stage1, Stage2, Stage3, Stage4, BossStage, BossDead, Ending
    }

    
    public static Stage stage = Stage.Stage1;
    
    
    private void Start()
    {
        gameStart = false;
        bossHealth.gameObject.SetActive(false);
        playerHealthBar.gameObject.SetActive(false);
        playerPotion.gameObject.SetActive(false);
    }

    public void StageInit()
    {
        gameStart = false;
        playerHealthBar.gameObject.SetActive(false);
        playerPotion.gameObject.SetActive(false);
        stage += 1;
        MusicController.Instance.MusicInit();
    }

    public void StageSet()
    {
        gameStart = true;
        playerHealthBar.gameObject.SetActive(true);
        playerPotion.gameObject.SetActive(true);
        potal.SetActive(true);
        if(stage == Stage.BossStage)
        {
            Monster.Instance.gameObject.SetActive(true);
            Monster.Instance.BattleStart();
        }
        MusicController.Instance.ChoiceMusic();
    }

    public void BossHealthBarSetC()
    {
        StartCoroutine(BossHealthBarSet());
    }

    public void CamInit()
    {
        for (int i = 0; i < stageCam.Length; i++)
        {
            stageCam[i].SetActive(false);
        }
        switch (stage)
        {
           case Stage.Stage1:
               stageCam[0].SetActive(true);
               break;
           case Stage.Stage2:
               stageCam[1].SetActive(true);
               break;
           case Stage.Stage3:
               stageCam[2].SetActive(true);
               break;
           case Stage.Stage4:
               stageCam[3].SetActive(true);
               break;
           case Stage.BossStage:
               stageCam[4].SetActive(true);
               break;
        }
    }

    public void Skip()
    {
        bossHealth.value += 1000;
        bossHealth.gameObject.SetActive(true);
        gameStart = true;
        playerHealthBar.gameObject.SetActive(true);
    }


    IEnumerator BossHealthBarSet()
    {
        bossHealth.value = 0;
        bossHealth.gameObject.SetActive(true);
        for (int i = 0; i < 200; i++)
        {
            bossHealth.value += 10f;
            yield return healthDel;
        }
        gameStart = true;
        bossHealth.value = 2000;
    }

    public void GameEnd()
    {
        SceneManager.LoadScene("End Title");
    }
    


    public void PhaseInit()
    {
        gameStart = false;
        bossHealth.gameObject.SetActive(false);
        playerHealthBar.gameObject.SetActive(false);
        playerPotion.gameObject.SetActive(false);
    }

    public void BossPhaseStart()
    {
        StartCoroutine(BossHealthBarSet());
        Monster.Instance.BattleStart();
        playerHealthBar.gameObject.SetActive(true);
        playerPotion.gameObject.SetActive(true);
    }
}
