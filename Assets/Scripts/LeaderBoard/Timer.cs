﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] AudioClip readySetClip;
    [SerializeField] GameObject startPanel;
    [SerializeField] Text startText;
    [SerializeField] Text secLeftText;
    private bool timerActive;
    private float seconds;

    private void Start() {
        StartCoroutine(StartSequence());
    }

    private void Update() {
        if(timerActive) {
            if(seconds > 0) {
                seconds -= Time.deltaTime;
            } else {
                timerActive = false;
                LB_EndGame.instance.TriggerEnd();
            }
            secLeftText.text = ((int)seconds).ToString();
        }
    }

    private IEnumerator StartSequence() {
        startPanel.SetActive(true);
        LB_AudioKing.instance.PlayOneShot(readySetClip);
        int x = 3;
        while(x > 0) {
            switch (x) {
                case 3:
                    startText.text = "Ready";
                    break;
                case 2:
                    startText.text = "Set";
                    break;
                case 1:
                    startText.text = "GO!!";
                    break;
            }
            yield return new WaitForSeconds(1f);
            x--;
        }

        seconds = 60;
        timerActive = true;
        startPanel.SetActive(false);
    }
}
