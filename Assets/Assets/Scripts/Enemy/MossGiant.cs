using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamagable
{
    public int Health { get; set; }
    public override void Init()
    {
        base.Init();
        Health = base.hp;
    }

    public override void Movement()
    {
        base.Movement();
        
        Vector3 direction = player.transform.localPosition - transform.localPosition;
        
        if (direction.x > 0 && animator.GetBool("inCombat") == true)
        {
            sprite.flipX = false;
        }

        else if (direction.x < 0 && animator.GetBool("inCombat") == true)
        {
            sprite.flipX = true;
        }
    }
    
    public void Damage()
    {
        Health--;
        animator.SetTrigger("Hit");
        isHit = true;
        animator.SetBool("inCombat", true);   
        
        if (Health < 1)
        {
            isDead = true;
            animator.SetTrigger("Death");
        }
    }
}
