using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadScript : MonoBehaviour, ICanConnect
{
    public GameObject con;
    public bool canDisconnect = true;
    public bool isPadPressed = false;
    public HackNodeScript HackableNode;



    void Start()
    {
        if (con != null)
            con.GetComponent<IConnectabel>().Connect(this.gameObject);
    }
    void Update()
    {
        if (con != null)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, con.transform.position, GameManager.instance.wallMask);
            
            if (hit)
            {
                HackableNode.lineRenderer.positionCount = 2;
                HackableNode.lineRenderer.SetPosition(0, HackableNode.transform.position);
                HackableNode.lineRenderer.SetPosition(1, hit.point);
                Debug.Log(hit.collider.gameObject);
            }
            else
            {
                HackableNode.lineRenderer.positionCount = 2;
                HackableNode.lineRenderer.SetPosition(0, HackableNode.transform.position);
                HackableNode.lineRenderer.SetPosition(1, con.transform.position);   
                if (isPadPressed)
                    con.GetComponent<IConnectabel>().On();
                else 
                    con.GetComponent<IConnectabel>().Off();
            }
            
            
        }
        if (con == null)
        {
            HackableNode.lineRenderer.positionCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Unit unit = col.GetComponent<Unit>();
        if (unit != null)
        {
            isPadPressed = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        Unit unit = col.GetComponent<Unit>();
        if (unit != null)
        {
            isPadPressed = false;
            
        }
    }
    public void Connect(GameObject IConnectable)
    {
        con = IConnectable;
    }
    public void Disconnect()
    {
        if (con == null) return;

        GameObject tempCon = con;
        con = null;
        tempCon.GetComponent<IConnectabel>().Disconnect();
        tempCon.GetComponent<IConnectabel>().Off();
        
    }

    public bool isConnected()
    {
        return con != null;
    }
    public bool CanDisconnect()
    {
        return canDisconnect;
    }
    
}
