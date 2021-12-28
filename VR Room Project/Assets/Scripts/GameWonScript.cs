using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWonScript : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = $"You won with {score.ToString()} score";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Create_with_VR_Starter_Scene");
    }
}
