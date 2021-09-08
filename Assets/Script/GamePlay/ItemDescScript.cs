using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescScript : MonoBehaviour, IintercatableHackMode
{
    [TextArea]
    public string itemName;
    [TextArea]
    public string itemDescription;
    public Sprite itemImage;
    public ItemDescUIManager itemDescUI;

    public void Start()
    {
        // itemDescUI = GameObject.FindObjectOfType<ItemDescUIManager>();
        if (itemDescUI == null)
        {
            Debug.LogWarning("CANNOT ITEM DESCRIPTION UI IN THE SCENE");
        }
    }
    public void Interact()
    {
        Debug.Log("Interact");
        itemDescUI.ShowUpdateitemDescription(itemName, itemDescription, itemImage);
    }
}
