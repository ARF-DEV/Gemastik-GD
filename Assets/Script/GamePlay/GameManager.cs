using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState {
    NormalMode,
    GameOver,
    HackMode,
    Win,
    Pause,
    Quiz
}
public class GameManager : MonoBehaviour
{
    public LayerMask wallMask;
    public int fieldWidth;
    public int fieldHeight;
    public Camera gameplayCam;
    public Camera hackModeCam;
    public static GameState gameState;
    public static GameState stateBefore;
    public static GameManager instance;
    public GameObject Wall;
    public GameObject camContainer;

    public PlayerController player;

    public UIManager pauseUI;
    public UIManager gameOverUI;
    public UIManager LevelCompleteUI;
    public ItemDescUIManager itemDescUI;
    public QuizUIManager QuizUI;
    public int unlockedLevel;
    private GoalScript goal;
    private TurretScript[] turrets;
    private GameObject ICanConnectSelected;
    private GameObject connectabelSelected;
    void Start()
    {
        instance = this;
        turrets = GameObject.FindObjectsOfType<TurretScript>();
        player = GameObject.FindObjectOfType<PlayerController>();
        goal = GameObject.FindObjectOfType<GoalScript>();
        gameState = GameState.NormalMode;
        
        camContainer.transform.position = new Vector3(fieldWidth/2, fieldHeight/2, -10);
        for (int i = 1; i <= fieldHeight - 1; i++)
        {
            Instantiate(Wall.gameObject, Vector3.zero + new Vector3(0, i, 2), Quaternion.identity, this.transform);
            Instantiate(Wall.gameObject, Vector3.zero + new Vector3(fieldWidth, i, 2), Quaternion.identity, this.transform);
        }

        for (int i = 0; i <= fieldWidth; i++)
        {
            Instantiate(Wall.gameObject, Vector3.zero + new Vector3(i,0, 2), Quaternion.identity, this.transform);
            Instantiate(Wall.gameObject, Vector3.zero + new Vector3(i, fieldHeight, 2), Quaternion.identity, this.transform);
        }


        
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = (float)fieldWidth / (float)fieldHeight;
        float finalSize;
       
        float differenceInSize = targetRatio / screenRatio;
        finalSize = (float)fieldHeight / 2f * differenceInSize;
        hackModeCam.orthographicSize = finalSize;
        gameplayCam.orthographicSize = finalSize;

    }

    void Update()
    {
        gameState = (goal.playerInside) ? GameState.Win : gameState;
        gameState = (player == null) ? GameState.GameOver : gameState;
        
        if (gameState == GameState.NormalMode)
        {
            
            gameplayCam.enabled = true;
            hackModeCam.enabled = false;
            player.PlayerMove();
            if(Input.GetKeyDown(KeyCode.Q))
            {
                GameManager.gameState = GameState.HackMode;
            }
            foreach (TurretScript turret in turrets)
            {
                turret.TurretAction();
            }
            CheckPause();
        }
        else if (gameState == GameState.HackMode)
        {
            HackMode();
            CheckPause();
        }
        else if (gameState == GameState.Win)
        {
            LevelCompleteUI.Show();
            PlayerPrefs.SetInt("levelReach", unlockedLevel);
        }
        else if (gameState == GameState.GameOver)
        {
            gameOverUI.Show();
        }
        else if (gameState == GameState.Pause)
        {
            pauseUI.Show();
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                gameState = stateBefore;
                pauseUI.Hide();
            }
        }
        else if (gameState == GameState.Quiz)
        {
            QuizUI.Show();
        }
    }

    void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stateBefore = gameState;
            gameState = GameState.Pause;
        }
    }
    public void HackMode()
    {
        gameplayCam.enabled = false;
        hackModeCam.enabled = true;
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = hackModeCam.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] cols = Physics2D.OverlapCircleAll(mousePos, 0.1f);
            
            foreach (Collider2D col in cols)
            {
                IConnectabel iConnectable = col.GetComponent<IConnectabel>();
                ICanConnect iCanConnect = col.GetComponent<ICanConnect>();
                if (iCanConnect != null && iCanConnect.CanDisconnect())
                {
                    ICanConnectSelected = col.gameObject;
                    if (iCanConnect.isConnected())
                    {
                        iCanConnect.Disconnect();
                    }
                    break;
                }
                if (iConnectable != null && iConnectable.CanDisconnect())
                {
                    connectabelSelected = col.gameObject;
                    if (iConnectable.isConnected())
                    {
                        iConnectable.Disconnect();
                    }
                    break;
                }

            }

            foreach (Collider2D col in cols)
            {
                // Debug.Log(col.gameObject);
                IintercatableHackMode interactable = col.GetComponent<IintercatableHackMode>();
                Debug.Log(interactable);
                if (interactable != null) 
                {
                    // Debug.Log("P");
                    interactable.Interact();
                }
            }
        }
        
        if (ICanConnectSelected != null)
        {

        }
        
        if (ICanConnectSelected != null && connectabelSelected != null)
        {
            ICanConnectSelected.GetComponent<ICanConnect>().Connect(connectabelSelected);
            connectabelSelected.GetComponent<IConnectabel>().Connect(ICanConnectSelected);

            ICanConnectSelected = null;
            connectabelSelected = null;
        }
        if(Input.GetKeyDown(KeyCode.Q) && !itemDescUI.GetComponent<Canvas>().enabled)
        {
            GameManager.gameState = GameState.NormalMode;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }
}
