using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StagePotal : MonoBehaviour
{
    [SerializeField] private Vector2[] stagePos;
    [SerializeField] private GameObject rope;
    void OnEnable()
    {
        rope.SetActive(false);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        switch (GameManager.stage)
        {
            case GameManager.Stage.Stage1:
                transform.position = stagePos[0];
                break;
            case GameManager.Stage.Stage2:
                transform.position = stagePos[1];
                break;
            case GameManager.Stage.Stage3:
                transform.position = stagePos[2];
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case GameManager.Stage.Stage4:
                transform.position = stagePos[3];
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case GameManager.Stage.BossStage:
                transform.position = stagePos[4];
                gameObject.SetActive(false);
                break;
            case GameManager.Stage.BossDead:
                transform.position = stagePos[4];
                rope.SetActive(true);
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            GameManager.Instance.StageInit();
            gameObject.SetActive(false);
        }
    }
}
