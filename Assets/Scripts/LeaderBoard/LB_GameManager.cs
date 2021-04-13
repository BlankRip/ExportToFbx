using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LB_GameManager : MonoBehaviour
{
    [SerializeField] AudioClip click;
    [SerializeField] Text scoreText;

    public int score;

    private void Start() {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void AddScore() {
        LB_AudioKing.instance.PlayOneShot(click);
        score++;
        scoreText.text = score.ToString();
    }
}
