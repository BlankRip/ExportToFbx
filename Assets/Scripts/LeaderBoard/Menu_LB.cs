using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_LB : MonoBehaviour
{
    [SerializeField] AudioClip buttonPressed;
    [SerializeField] GameObject lbObj;
    public void StartGame() {
        LB_AudioKing.instance.PlayOneShot(buttonPressed);
        SceneManager.LoadScene(1);
    }

    public void ShowLeaderboard() {
        lbObj.SetActive(true);
    }

    public void HideLeaderboard() {
        lbObj.SetActive(false);
    }
}
