using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    [SerializeField] FormationSquard[] squads;
    [SerializeField] List<Transform> movePoints;
    int picker;
    int previousPick;

    void Start()
    {
        if(squads.Length <= movePoints.Count)
        {
            for (int i = 0; i < squads.Length; i++)
            {
                picker = Random.Range(0, 100);
                if(picker < 35)
                    squads[i].meAttack = Enemies.Dog;
                else if(picker > 35 && picker < 80)
                    squads[i].meAttack = Enemies.Cat;
                else if(picker > 80)
                    squads[i].meAttack = Enemies.Man;
            }
            
            for (int i = 0; i < squads.Length; i++)
            {
                picker = Random.Range(0, movePoints.Count); 
                squads[i].seekPos = movePoints[picker].position;
                movePoints.Remove(movePoints[picker]);
            }
            
            for (int i = 0; i < squads.Length; i++)
                squads[i].enabled = true;
        }
        else
            Debug.Log("<color=red> THE NUMBER OF SQUADS ARE MORE THAN POINTS TO MOVE </color>");
    }
}
