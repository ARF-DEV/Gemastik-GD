using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescUIManager : UIManager
{
    public Text itemDescriptionHeader;
    public Text itemDescriptionBody;

    public Image itemImageHolder;
    public void UpdateitemDescription(string newHeader, string newBody, Sprite _itemImage)
    {
        itemDescriptionHeader.text = newHeader; 
        itemDescriptionBody.text = newBody;
        itemImageHolder.sprite = _itemImage;
    }
    public void ShowUpdateitemDescription(string newHeader, string newBody, Sprite _itemImage)
    {
        Debug.Log("ShowUpdate");
        UpdateitemDescription(newHeader, newBody, _itemImage);
        Show();
    }
}
