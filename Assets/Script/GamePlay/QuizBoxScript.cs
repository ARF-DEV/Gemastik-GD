using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuizBoxScript : MonoBehaviour, ICanConnect, Iinteractable
{
    
    public GameObject con;
    public bool canDisconnect = true;
    public HackNodeScript HackableNode;
    public bool isQuizCompleted = false;


    [TextArea]
    public string question;

    
    [TextArea]
    public string answer;
    public QuizUIManager QuizUI;
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
            }
            else
            {
                HackableNode.lineRenderer.positionCount = 2;
                HackableNode.lineRenderer.SetPosition(0, HackableNode.transform.position);
                HackableNode.lineRenderer.SetPosition(1, con.transform.position);   
                if (isQuizCompleted)
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

    public void ShowQuiz()
    {
        GameManager.gameState = GameState.Quiz;
        QuizUI.UpdateUI(question, answer, this);
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

    public void Interact()
    {
        if (isQuizCompleted) return;
        ShowQuiz();
    }
}
