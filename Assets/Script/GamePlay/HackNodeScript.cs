using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackNodeScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public SpriteRenderer spriteRenderer;
    void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
}
