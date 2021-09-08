using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawnerScript : MonoBehaviour
{
    public BoxScript box;
    public GameObject curBox;

    

    void Start()
    {
        if (curBox == null)
            curBox = Instantiate(box.gameObject, transform.position, Quaternion.identity, this.transform);
        else
            curBox.transform.parent = this.transform;
    }

}
