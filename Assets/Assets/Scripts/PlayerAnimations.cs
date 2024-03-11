using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    private Animator _swordAnimation;
    private GameObject _swordArc;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _swordAnimation = transform.GetChild(0).GetComponent<Animator>();
    }

    public void Move(float move)
    {
        _animator.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jump)
    {
     _animator.SetBool("Jumping", jump);   
    }

    public void Attack()
    {
        Debug.Log("PLAYER IS ATTACKING");
        _animator.SetTrigger("Attack");
        _swordAnimation.SetTrigger("SwordAnimation");
    }

    
}
