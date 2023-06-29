using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Security.Cryptography;
using System.Xml.Schema;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour, IHit
{
    public static Monster Instance;

    [SerializeField] private Animator anim;
    [SerializeField] private Transform playerTr;

    private bool attackAreaIn;

    private bool phase;

    private bool hit = false;
    private bool landing = false;
    private bool patternCountA;
    private bool patternCountM;
    [SerializeField] private int bossHealth = 1000;
    [SerializeField] private Slider hpSlider;

    private WaitForSeconds patternDelay = new WaitForSeconds(2.5f);
    private WaitForSeconds attackDelay = new WaitForSeconds(3f);
    private WaitForSeconds attackDelayEx = new WaitForSeconds(0.25f);
    private WaitForSeconds damageDelay = new WaitForSeconds(0.08f);
    private WaitForSeconds magicStartDelay = new WaitForSeconds(1f);
    private WaitForSeconds magicDelay = new WaitForSeconds(1f);
    private WaitForSeconds magicPhaseDelay = new WaitForSeconds(0.4f);
    private WaitForSeconds magicDelayEx = new WaitForSeconds(2f);

    [SerializeField] private GameObject[] attackArea = new GameObject[2];
    
    [SerializeField] private GameObject[] warningImg = new GameObject[2];

    private bool attacking;

    private void Awake()
    {
        Instance = this;
        hpSlider.value = 0;
        transform.position = new Vector3(62.8f, -5, 0);
        gameObject.SetActive(false);
    }

    public void BattleStart()
    {
        anim.SetBool("Phase", phase);
        attackArea[phase ? 1 : 0].SetActive(true);
        attackArea[phase ? 0 : 1].SetActive(false);
        StartCoroutine(Pattern());
    }


    void Death()
    {
        hpSlider.gameObject.SetActive(false);
        GameManager.Instance.StageInit();
        gameObject.SetActive(false);
    }

    public void Hit(int damage)
    {
        if (bossHealth <= 0 || !GameManager.gameStart) return;
        bossHealth -= damage;
        hpSlider.value = bossHealth;
        if (bossHealth <= 0 && phase)
        {
            Death();
            attackArea[0].SetActive(false);
            attackArea[1].SetActive(false);
        }
        else if (bossHealth <= 0 && !phase)
        {
            phase = true;
            TimelineController.Instance.BossPhaseChange();
            bossHealth = 2000;
            attackArea[0].SetActive(false);
            attackArea[1].SetActive(false);
        }
        else if (!attacking) anim.SetTrigger("Hit");
    }

    IEnumerator Pattern()
    {
        yield return new WaitUntil(() => GameManager.gameStart);
        yield return new WaitForSeconds(3f);
        while(GameManager.gameStart)
        {
            int pattern = Random.Range(0, 2);
            if (patternCountA && pattern == 0)
            {
                patternCountA = false;
                if(!patternCountM)
                {
                    pattern = Random.Range(0f, 1f) > 0.3f ? 1 : 0;
                }
            }
            else if (patternCountM && pattern == 1)
            {
                patternCountM = false;
                if(!patternCountA)
                {
                    pattern = Random.Range(0f, 1f) > 0.3f ? 0 : 1;
                }
            }
            if (!attackAreaIn) pattern = 1;
            switch (pattern)
            { 
                case 0:
                    patternCountA = true;
                    AttackWarnig();
                    yield return attackDelay;
                    anim.SetTrigger("Attack");
                    attacking = true;
                    yield return attackDelayEx;
                    if(attackAreaIn) PlayerController.Instance.Hit((phase) ? 38 : 17);
                    yield return damageDelay;
                    attacking = false;
                    if (phase) yield return patternDelay;
                    break;
                case 1:
                    patternCountM = true;
                    attacking = true;
                    anim.SetTrigger("Cast");
                    yield return magicStartDelay;
                    attacking = false;
                    int n = phase ? 10 : 3;
                    for(int i = 0; i < n; i++)
                    {
                        Magic();
                        yield return phase ? magicPhaseDelay : magicDelay;
                    }
                    yield return magicDelayEx;
                    break;
            }
            yield return patternDelay;
        }
    }
    
    void Magic()
    {
        var obj = ObjectPool.GetObject();
        obj.transform.position = playerTr.position + Vector3.up;
    }
    
    void AttackWarnig()
    {
        warningImg[(phase) ? 1 : 0].SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) attackAreaIn = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) attackAreaIn = false;
    }
}
