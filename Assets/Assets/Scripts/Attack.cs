using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canHit = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);

        IDamagable hit = other.GetComponent<IDamagable>();

        if (hit != null)
        {
            if (_canHit == true)
            {
                hit.Damage();
                _canHit = false;
                StartCoroutine(ResetHit());
            }
        }
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(0.5f);
        _canHit = true;
    }
}