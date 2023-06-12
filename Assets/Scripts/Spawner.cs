using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] stageObj;

    public void StageActive()
    {
        for(int i=0; i < stageObj.Length; i++)
            stageObj[i].SetActive(false);
        switch (GameManager.stage)
        {
            case GameManager.Stage.Stage1:
                stageObj[0].SetActive(true);
                break;
            case GameManager.Stage.Stage2:
                stageObj[1].SetActive(true);
                break;
            case GameManager.Stage.Stage3:
                stageObj[2].SetActive(true);
                break;
            case GameManager.Stage.Stage4:
                stageObj[3].SetActive(true);
                break;
        }
    }
}
