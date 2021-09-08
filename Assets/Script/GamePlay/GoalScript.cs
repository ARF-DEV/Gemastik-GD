using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public bool playerInside = false;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController player = col.GetComponent<PlayerController>();
        if (player != null)
        {
            playerInside = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        PlayerController player = col.GetComponent<PlayerController>();
        if (player != null)
        {
            playerInside = false;
        }
    }
}
