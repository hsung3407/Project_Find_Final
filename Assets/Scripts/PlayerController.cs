using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.Rendering.UI;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float jumpPower;
    [SerializeField] float speed;
    [SerializeField] private float playerHP;
    [SerializeField] private int damage;

    
    [SerializeField] private Rigidbody2D playerRig2D;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private Animator anim;
    [SerializeField] private Slider playerHPBar;
    
    private float horizontal;
    private bool landing;
    
    private bool attack;
    private bool secondAttack;
    
    private bool playerHit;
    
    private bool flipX = false;

    void Start()
    {
        Init();
    }

    void Init()
    {
        playerHP = 170;
        playerHPBar.maxValue = playerHP;
        playerHPBar.value = playerHP;
        StartCoroutine(HitDelay());
    }


    public void Heal(int healHp)
    {
        playerHP += healHp;
        playerHPBar.value = playerHP;
    }

    

    private void FixedUpdate()
    {
        if (playerHit) return;
        Fall();
        if (!GameManager.gameStart) return;
        if (attack) return;
        Move();
    }

    private void Update()
    {
        if (!GameManager.gameStart) return;
        if (playerHit) return;
        Attack();
        if (attack) return;
        Jump();
    }


    void Attack()
    {
        if (Input.GetButtonUp("Fire1") && !attack && landing)
        {
            attack = true;
            anim.SetTrigger("Attack");
        }
        if (Input.GetButtonDown("Fire1") && attack && !secondAttack)
        {
            anim.SetBool("Second Attack", true);
            secondAttack = true;
        }
    }

    void AttackCencer1()
    {
        Collider2D[] enemy = Physics2D.OverlapBoxAll(transform.position - ((flipX) ? 1.4f : -1.4f) * Vector3.right + Vector3.up * 0.6f,
            new Vector2(4.3f, 2.7f), 0);
        
        foreach(Collider2D e in enemy) 
        {
            if (e.CompareTag("Monster"))
            {
                IHit m = e.GetComponent<IHit>();
                m.Hit(damage);
            }
        }
    }

   
    void AttackCencer2()
    {
        Collider2D[] enemy = Physics2D.OverlapBoxAll(transform.position - ((flipX) ? 1.6f : -1.6f) * Vector3.right + Vector3.up * 0.3f,
            new Vector2(4.3f, 2), 0);
        
        foreach(Collider2D e in enemy)
        {
            if (e.CompareTag("Monster"))
            {
                IHit m = e.GetComponent<IHit>();
                m.Hit(damage + 15);
            }
        }
    }
    
    void AttackInit1()
    {
        if (!secondAttack) attack = false;
    }

    void AttackInit2()
    {
        attack = false;
        secondAttack = false;
        anim.SetBool("Second Attack", false);
    }
    
    public void Hit(int damage)
    {
        if (playerHit || playerHP < 0) return;
        playerHit = true;
        attack = false;
        secondAttack = false;
        anim.SetBool("Second Attack", false);
        playerHP -= damage;
        playerHPBar.value = playerHP;
        if(playerHP <= 0)
        {
            anim.SetBool("Is Death", true);
            TimelineController.Instance.PlayerDead();
        }
        else
        {
            playerHit = true;
            anim.SetTrigger("Hit");
        }
    }

    
    IEnumerator HitDelay()
    {
        while (true)
        {
            yield return null;
            if (playerHit)
            {
                yield return new WaitForSeconds(0.5f);
                playerHit = false;
            }
        }
    }
    
    
    void Fall()
    {
        if (playerRig2D.velocity.y + 0.0001 < 0 && !anim.GetBool("Falling") && !landing)
        {
            anim.SetBool("Falling", true);
            anim.SetTrigger("Fall");
        }
    }

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            landing = true;
            playerRig2D.velocity = Vector2.zero;
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Ground"))
            landing = false;
    }


    void Jump()
    {
        if (Input.GetButtonDown("Jump") && landing)
        {
            anim.SetTrigger("Jump");
            anim.SetBool("Jumping", true);
            playerRig2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    void Move()
    {
        anim.SetBool("Run", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0) flipX = true;
        else if (horizontal > 0) flipX = false;
        playerRenderer.flipX = flipX;
        playerRig2D.AddForce(Vector2.right * horizontal * speed, ForceMode2D.Force);
    }

    public void DamageUp(int addDam)
    {
        damage += addDam;
    }
}
