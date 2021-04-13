﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LB_EndGame : MonoBehaviour
{
    public static LB_EndGame instance;
    [SerializeField] AudioClip buttonSE;
    [SerializeField] Text endScore;
    [SerializeField] GameObject endPanel;
    [SerializeField] GameObject enterName;
    [SerializeField] GameObject endButtons;
    [SerializeField] GameObject leaderBoard;

    private void Awake() {
        instance = this;
    }

    public void TriggerEnd() {
        endScore.text = $"You Scored: {LB_GameManager.instance.score}";
        endPanel.SetActive(true);
    }

    public void SubmitToLeaderBoard(InputField field) {
        LB_AudioKing.instance.PlayOneShot(buttonSE);
        string username = field.text;
        Debug.Log(username);
        enterName.SetActive(false);
        endButtons.SetActive(true);
    }

    public void BackToMenu() {
        LB_AudioKing.instance.PlayOneShot(buttonSE);
        SceneManager.LoadScene(0);
    }

    public void ShowLeaderboard() {
        LB_AudioKing.instance.PlayOneShot(buttonSE);
        leaderBoard.SetActive(true);
    }

    public void HideLeaderboard() {
        LB_AudioKing.instance.PlayOneShot(buttonSE);
        leaderBoard.SetActive(false);
    }
}
