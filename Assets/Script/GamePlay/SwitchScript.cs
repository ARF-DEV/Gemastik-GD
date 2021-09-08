using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public GameObject con;
    public bool canDisconnect = true;

    public void Interact()
    {
        con.GetComponent<IConnectabel>().On();
    }
    // public void SwitchOff()
    // {
    //     con.GetComponent<IConnectabel>().Disconnect();
    // }
}
