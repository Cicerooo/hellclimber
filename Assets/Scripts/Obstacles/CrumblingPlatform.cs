using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    public GameObject GroundParticles;
    private BoxCollider2D _collider2D;
    private Animator _animator;
    private bool crumbleState = false;
    private bool isCrumbling = false;
    public float crumbleTime = 0.5f;
    private void OnCollisionStay2D(Collision2D other)
    {
        if (isCrumbling==false)
        {
            StartCoroutine("StartPlatform");
        }
    }
    
    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    public IEnumerator StartPlatform()
    {
        isCrumbling = true;
        yield return new WaitForSeconds(crumbleTime);
        StartCoroutine("TogglePlatform");
        yield return 0;
    }
    public IEnumerator TogglePlatform()
    {
        crumbleState = !crumbleState;
        _animator.SetTrigger("Crumble");
        _collider2D.enabled = false;
        yield return new WaitForSeconds(4.5f);
        isCrumbling = false;
        _collider2D.enabled = true;
        yield return 0;
    }
    public void AllowCrumbling()
    {
        if (isCrumbling)
        {
            isCrumbling = false;
        }
    }
    public void DisableCollider()
    {
        _collider2D.enabled = false;
    }
    
    public void EnableCollider()
    {
        _collider2D.enabled = true;
    }
}
