using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMagic : MonoBehaviour
{
    private SpriteRenderer ren;
    private bool playerIn;
    private bool hit;

    private BoxCollider2D col;
    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }
    

    void OnEnable()
    {
        if (!GameManager.gameStart) return;
        Invoke("Attack", 0.5f);
        Invoke("End", 1f);    
    }

    public void Delay()
    {
        Destroy(gameObject);
    }


    public void Attack()
    {
        col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            col.enabled = false;
            PlayerController.Instance.Hit(30);
        }
    }

    public void End()
    {
        col.enabled = false;
        ObjectPool.ReturnObject(gameObject);
        CancelInvoke();
    }
    
}
