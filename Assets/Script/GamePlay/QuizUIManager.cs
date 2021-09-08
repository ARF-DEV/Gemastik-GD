using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUIManager : UIManager
{
    public Text questionText;
    public InputField inputField;
    [TextArea]
    public string answer;
    private QuizBoxScript quizBox;
    public void UpdateUI(string question, string _answer, QuizBoxScript _quizBox)
    {
        questionText.text = question;
        answer = _answer;
        quizBox = _quizBox;
        
    }
    public void checkAnswer()
    {
        Debug.Log("test");
        string playerAnswer = inputField.text.ToLower();
        string correctAnswer = answer.ToLower();
        if (playerAnswer == correctAnswer)
        {
            Debug.Log("Correct");
            quizBox.isQuizCompleted = true;
            
            Hide();
            GameManager.gameState = GameState.NormalMode;
        }
        else
        {
            Debug.Log("WRONg");
        }
        inputField.text = "";
    }

}
