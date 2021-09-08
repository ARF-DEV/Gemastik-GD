using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    
    public Button[] buttons;
    public Sprite finishLevelBtnSprite;
    int levelReach;
    void Start()
    {   
        levelReach = PlayerPrefs.GetInt("levelReach", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i + 1 > levelReach) 
            {
                buttons[i].interactable = false;
            }
            if (i + 1 < levelReach)
            {
                Image image = buttons[i].GetComponent<Image>();
                image.sprite = finishLevelBtnSprite;
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
