using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAcidEvent : MonoBehaviour
{
    private Spider _spider;

    private void Start()
    {
        _spider = transform.GetComponent<Spider>();
    }

    public void Fire()
    {
        Debug.Log("Fire");
        _spider.Attack();
    }
}
