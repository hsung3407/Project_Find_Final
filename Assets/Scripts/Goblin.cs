using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class Goblin : Mob
{

    void AttackCencer1()
    {
        if (Physics2D.OverlapBox(rb.position + Vector2.down * 0.2f, new Vector2(4.3f, 2.2f), 0, playerLayer)) PlayerController.Instance.Hit(damage);
    }

    void AttackCencer2()
    {
        if (Physics2D.OverlapBox(rb.position + Vector2.right * 2.1f * isFlip + Vector2.down * 0.5f,
                new Vector2(2.3f, 1.3f), 0, playerLayer)) PlayerController.Instance.Hit(damage);
    }
    
    public override void Death()
    {
        StartCoroutine(DeathDelay());
    }
    
    IEnumerator DeathDelay()
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(0.267f);
        gameObject.SetActive(false);
    }
}
