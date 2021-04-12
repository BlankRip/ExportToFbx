using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LB_GameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;

    public int score;

    private void Start() {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void AddScore() {
        score++;
        scoreText.text = score.ToString();
    }
}
