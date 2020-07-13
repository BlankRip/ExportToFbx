using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{
    [SerializeField] FormationSquard[] squads;
    [SerializeField] Transform[] movePoints;
    int picker;
    int previousPick;

    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            picker = Random.Range(0, 100);
            if(picker < 35)
                squads[i].meAttack = Enemies.Dog;
            else if(picker > 35 && picker < 80)
                squads[i].meAttack = Enemies.Cat;
            else if(picker > 80)
                squads[i].meAttack = Enemies.Man;
        }
        

        picker = Random.Range(0, movePoints.Length); 
        squads[0].seekPos = movePoints[picker].position;

        if(picker == movePoints.Length -1)
            picker--;
        else
        picker++;
        squads[1].seekPos = movePoints[picker].position;

        squads[0].enabled = true;
        squads[1].enabled = true;
    }
}
