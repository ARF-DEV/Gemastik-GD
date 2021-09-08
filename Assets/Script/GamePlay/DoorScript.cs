using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : Unit, IConnectabel
{

    bool isOpen = false;
    public GameObject con;
    protected override void Start()
    {
        base.Start();
        moveable = false;
        if (con != null)
            con.GetComponent<ICanConnect>().Connect(this.gameObject);
    }
    void Update()
    {
        if (isOpen)
        {
            SpriteRenderer SR =  this.GetComponent<SpriteRenderer>();
            BoxCollider2D col = this.GetComponent<BoxCollider2D>();
            SR.enabled = false;
            col.enabled = false;
        }
        else
        {
            SpriteRenderer SR =  this.GetComponent<SpriteRenderer>();
            BoxCollider2D col = this.GetComponent<BoxCollider2D>();
            SR.enabled = true;
            col.enabled = true;
        }
    }
    public void On()
    {
        isOpen = true;
    }
    public void Off()
    {
        isOpen = false;
    }
    public void Connect(GameObject ICanConnect)
    {
        con = ICanConnect;
    }
    public void Disconnect()
    {
        if (con == null) return;
        GameObject tempCon = con;
        con = null;

        tempCon.GetComponent<ICanConnect>().Disconnect();
    }
    public bool isConnected()
    {
        return con != null;
    }
    public bool CanDisconnect()
    {
        if (con == null) return true;
        
        return con.GetComponent<ICanConnect>().CanDisconnect();
    }
}
