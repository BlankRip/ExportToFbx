﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMeeleRange : GOAP_Action
{
    private void Start() {
        if(requiredStates.states.Count == 0) {
            requiredStates.states.Add(GOAP_States.Awake);
            requiredStates.states.Add(GOAP_States.PlayerInSite);
            requiredStates.states.Add(GOAP_States.HasMeele);
        }

        if(resultStates.states.Count == 0) {
            resultStates.states.Add(GOAP_States.InMeeleRange);
        }
    }


    public override void ExicuitAction(GOAP_Agent agent) {
        Debug.Log("<color=red> Move To Meele </color>");

        if((agent.transform.position - agent.player.transform.position).sqrMagnitude >= 1f * 1f)
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, agent.player.transform.position, agent.moveSpeed * Time.deltaTime);
        else
            agent.PopAction();
    }
}
