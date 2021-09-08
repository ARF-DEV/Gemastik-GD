using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public LayerMask raycastableLayers;
    public int moveSpeed = 1;
    public bool moveable = true;
    protected virtual void Start()
    {
        Physics2D.queriesStartInColliders = false;
        SnapPos();
    }


    

    protected void SnapPos()
    {
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }

    public void Move(Vector2 dir)
    {
        transform.position = new Vector2(transform.position.x, transform.position.y) + dir * moveSpeed;
    }
    
    public virtual bool CanMove(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, moveSpeed, raycastableLayers);
        if (hit)
        {
            return false;
        }
        // Debug.Log("TRUE");
        return true;
    }
}
