using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public bool isChangingState = false;
    public bool hideInStart = true;
    public virtual void Start()
    {
        if (hideInStart)
            gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void exitGame()
    {
        Application.Quit();
    }
    public void Show()
    {
        Debug.Log("Show");
        InputField inputField = GetComponentInChildren<InputField>();
        if (inputField != null)
        {
            inputField.interactable = true;
        }
        
        
        gameObject.GetComponent<Canvas>().enabled = true;
    }
    public void Hide()
    {
        if (isChangingState)
            GameManager.gameState = GameManager.stateBefore;
        
        InputField inputField = GetComponentInChildren<InputField>();
        if (inputField != null)
        {
            inputField.interactable = false;
        }
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void ChangeUI(UIManager UI)
    {
        UI.Show();
        Hide();
    }
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
