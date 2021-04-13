using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_LB : MonoBehaviour
{
    [SerializeField] AudioClip buttonPressed;
    public void StartGame() {
        LB_AudioKing.instance.PlayOneShot(buttonPressed);
        SceneManager.LoadScene(1);
    }
}
