using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    private float moveSpeed = 10f;
    void FixedUpdate()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    public void SetMoveSpeed(float _moveSpeed)
    {
        moveSpeed = _moveSpeed;
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        BoxScript box = col.gameObject.GetComponent<BoxScript>();
        if (box != null)
        {
            box.Delete();
        }
        PlayerController player = col.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            Destroy(player.gameObject);
        }
        Destroy(this.gameObject);
    }
}
