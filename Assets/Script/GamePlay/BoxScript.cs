using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : Unit
{
    public AudioSource audioSource;
    public AudioClip deleteAudio;
    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }
    public void Delete()
    {
        
        audioSource.PlayOneShot(deleteAudio);
        if (transform.parent == null)
        {
            StartCoroutine(DestroyCoroutine());
        }
        else
        {
            StartCoroutine(DestroyNSpawnCoroutine());
        }
    }
    IEnumerator DestroyCoroutine()
    {
        SpriteRenderer SR =  this.GetComponent<SpriteRenderer>();
        BoxCollider2D col = this.GetComponent<BoxCollider2D>();
        SR.enabled = false;
        col.enabled = false;
        audioSource.PlayOneShot(deleteAudio);
        yield return new WaitUntil(() => !audioSource.isPlaying);
        Destroy(this.gameObject);
    }
    IEnumerator DestroyNSpawnCoroutine()
    {
        SpriteRenderer SR =  this.GetComponent<SpriteRenderer>();
        BoxCollider2D col = this.GetComponent<BoxCollider2D>();
        SR.enabled = false;
        col.enabled = false;
        this.transform.position = transform.parent.position;
        
        yield return new WaitForSeconds(0.1f);
        
        SR.enabled = true;
        col.enabled = true;
    }
}
