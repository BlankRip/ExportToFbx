using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LB_ListItem : MonoBehaviour
{
    [SerializeField] Text lbRank;
    [SerializeField] Text username;
    [SerializeField] Text score;

    public void UpdateData(string rank, string name, string playerScore) {
        lbRank.text = $"{rank}.";
        username.text = name;
        score.text = playerScore;
    }
}
