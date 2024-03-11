using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int hp;
    
    [SerializeField]
    protected int speed;
    
    protected int gems;

    [SerializeField]
    protected Transform pointA, pointB;

    protected SpriteRenderer sprite;
    protected Animator animator;
    protected Vector3 target;

    protected Player player;
    protected bool isHit = false;
    protected bool isDead = false;

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    public void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && animator.GetBool("inCombat") == false)
        {
            return;
        }

        if (isDead == false)
        {
            
            Movement();
        }
    }
    
    public virtual void Movement()
    {
        if (target == pointA.position)
        {
            sprite.flipX = true;
        }

        else
        {
            sprite.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            target = pointB.position;
            animator.SetTrigger("Idle");
            
        }

        else if (transform.position == pointB.position)
        {
            target = pointA.position;
            animator.SetTrigger("Idle");
        }
        

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        
        float distance = Vector3.Distance(player.transform.localPosition, transform.localPosition);

        if (distance > 2.0f)
        {
            isHit = false;
            animator.SetBool("inCombat", false);
        }
    }

    
}
