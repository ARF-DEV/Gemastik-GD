using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : Unit
{
    Vector2 curDir = Vector2.zero;
    public AudioSource audioSource;
    public AudioClip moveSound;
    public Sprite[] sprites; //UP, DOWN, LEFT, RIGHT
    private SpriteRenderer spriteRenderer;
    

    protected override void Start()
    {
        base.Start();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[1];
        
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public override bool CanMove(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, moveSpeed, raycastableLayers);
        if (hit)
        {
            //makes all object that inherate from Unit Class moveable
            Unit unit = hit.transform.GetComponent<Unit>();
            if (unit != null && unit.moveable && unit.CanMove(dir))
            {
                unit.Move(dir);
                return true;
            }
            return false;
        }
        
        return true;
    }
    
    
    public void PlayerMove()
    {

        Vector2 dir = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.W))
        {
            dir = Vector2.up;
            spriteRenderer.sprite = sprites[0];
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dir = Vector2.down;
            spriteRenderer.sprite = sprites[1];
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dir = Vector2.left;
            spriteRenderer.sprite = sprites[2];
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dir = Vector2.right;
            spriteRenderer.sprite = sprites[3];
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, curDir, 1f, raycastableLayers);
            Debug.Log(curDir);
            Debug.Log(hit.collider);
            if (hit)
            {
                Iinteractable interactable = hit.collider.GetComponent<Iinteractable>();
                interactable.Interact();
            }
        }
        if (dir != Vector2.zero)
        {
            curDir = dir;
            if (CanMove(dir)) 
            {
                audioSource.PlayOneShot(moveSound);
                Move(dir);
            }
        }
    }
    
}
