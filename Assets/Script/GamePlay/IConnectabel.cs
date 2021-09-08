using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConnectabel
{
    void Connect(GameObject ICanConnect);
    void Disconnect();
    bool isConnected();
    bool CanDisconnect();
    void On();
    void Off();
}

public interface ICanConnect
{
    void Connect(GameObject IConnactable);
    void Disconnect();

    bool isConnected();
    bool CanDisconnect();
}

public interface Iinteractable
{
    void Interact();
}


public interface IintercatableHackMode
{
    void Interact();
}

