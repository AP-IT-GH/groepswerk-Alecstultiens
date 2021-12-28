using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    int score = 0;
    public int ScoreToWin;
    public GameWonScript GameWonScript;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        scoreText.text = "SCORE: " + score.ToString() + "/" + ScoreToWin.ToString();

    }

    public void AddPoint()
    {
        score += 1;
        if (score >= ScoreToWin)
        {
            scoreText.text = "";
            GameWonScript.Setup(score);
        } else
        {
            scoreText.text = "SCORE: " + score.ToString() + "/" + ScoreToWin.ToString();
        }
        
    }

}
