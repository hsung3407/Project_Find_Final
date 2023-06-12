using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject toastText;
    private Animator anim;
    private BoxCollider2D col;
    public int addDam;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            col.enabled = false;
            anim.SetTrigger("Open");
            toastText.SetActive(true);
            PlayerController.Instance.DamageUp(addDam);
            Invoke("End", 2f);
        }
    }

    void End()
    {
        gameObject.SetActive(false);
    }
}
