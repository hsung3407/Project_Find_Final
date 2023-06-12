using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class Mob : MonoBehaviour, IHit
{   
    public int hp;
    public float speed;
    public float jumpForce;
    public int damage;
    public float attackDis;
    protected bool flip;
    protected bool isHit;


    public float activeDisX;
    public float activeDisY;

    protected bool isActive => playerTr.position.x - transform.position.x > -activeDisX && playerTr.position.x - transform.position.x < activeDisX && 
                               playerTr.position.y - transform.position.y < activeDisY && playerTr.position.y - transform.position.y > -activeDisY;
    
    protected int isFlip => flip ? -1 : 1;
    protected bool attackArea => Physics2D.Raycast(rb.position + Vector2.down* 0.6f, Vector2.right * isFlip, attackDis, playerLayer);

    protected bool jumpCheck =>
        Physics2D.Raycast(rb.position + Vector2.down * 0.6f, Vector2.right * isFlip, attackDis, groundLayer);
    
    protected Rigidbody2D rb;
    SpriteRenderer spriteRen;
    protected Animator anim;

    protected Transform playerTr;
    
    private WaitForSeconds attackDel = new WaitForSeconds(1f);
    private WaitForSeconds patternDel = new WaitForSeconds(0.3f);

    protected LayerMask playerLayer;
    protected LayerMask groundLayer;

    private object syncAttack = new object();

    
    

    
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerLayer = LayerMask.GetMask("Player");
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Start()
    {
        playerTr = PlayerController.Instance.transform;
    }

    void OnEnable()
    {
        StartCoroutine(Pattern());
        StartCoroutine(HitDelay());
    }

    private void Update()
    {
        if (hp <= 0) return;
        if (!isActive || attackArea)
        {
            anim.SetBool("Run", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }


    public void Hit(int dmg)
    {
        if (hp <= 0 || isHit) return;
        rb.velocity = new Vector2(0, rb.velocity.y);
        isHit = true;
        hp -= dmg;
        if (hp <= 0)
        {
            Death();
        }
        else anim.SetTrigger("Hit");
    }

    IEnumerator HitDelay()
    {
        while (true)
        {
            yield return new WaitUntil(() => isHit);
            yield return new WaitForSeconds(0.4f);
            isHit = false;
        }
    }


    public virtual void Death()
    {
        
    }

    IEnumerator Pattern()
    {
        while(true)
        {
            if (hp <= 0) yield break;
            yield return patternDel;
            if (!isActive || isHit) continue;
            if (attackArea)
                lock (syncAttack)
                {
                    Attack();
                    yield return attackDel;
                }
            else Move();
        }
    }

    void Move()
    {
        PlayerFaceing();
        anim.SetBool("Run", true);
        rb.velocity = new Vector2(speed * isFlip, rb.velocity.y);
        if(jumpCheck && rb.velocity.y-0.0001f <= 0) rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    

    void Attack()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
            case 1:
                anim.SetTrigger("Attack1");
                break;
            case 2:
                anim.SetTrigger("Attack2");
                break;
        }
    }



    private void PlayerFaceing()
    {
        if (playerTr.position.x - transform.position.x < 0)
        {
            flip = true;
        }
        else if (playerTr.position.x - transform.position.x > 0)
        {
            flip = false;
        }
        spriteRen.flipX = flip;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (hp < 0) return;
        if (col.CompareTag("Player")) PlayerController.Instance.Hit(damage);
    }
}

