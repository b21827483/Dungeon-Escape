using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Spider : Enemy, IDamagable
{
    public GameObject _acidPrefab; 
    
    public int Health { get; set; }
    public override void Init()
    {
        base.Init();
        Health = base.hp;
    }

    public void Damage()
    {
        Health--;
        if (Health < 1)
        {
            isDead = true;
            animator.SetTrigger("Death");
        }
    }

    public override void Movement()
    {
        
    }

    public void Attack()
    {
        Instantiate(_acidPrefab, transform.position, Quaternion.identity);
    }
}
