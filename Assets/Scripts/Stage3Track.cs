using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Track : MonoBehaviour
{
    [SerializeField] private GameObject[] cam;
    [SerializeField] private Transform PlayerTr;

    void OnEnable()
    {
        for (int i = 0; i < cam.Length; i++)
        {
            cam[i].SetActive(false);
        }
    }

    void Update()
    {
        if(PlayerTr.position.x > 115 && PlayerTr.position.y > 194)
        {
            cam[1].SetActive(true);
            cam[0].SetActive(false);
        }
        else
        {
            cam[0].SetActive(true);
            cam[1].SetActive(false);
        }
    }
}
