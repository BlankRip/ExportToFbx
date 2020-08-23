﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMeele : GOAP_Action
{
    private void Start() {
        if(requiredStates.states.Count == 0) {
            requiredStates.states.Add(GOAP_States.Awake);
            requiredStates.states.Add(GOAP_States.MeeleInSite);
        }

        if(resultStates.states.Count == 0) {
            resultStates.states.Add(GOAP_States.HasMeele);
        }
    }

    public override void InitializeAction(GOAP_Agent agent) {
        Debug.Log("<color=red> Pick up Meele </color>");
        agent.meeleWeapon.transform.parent = agent.meelePostion;
        agent.meeleWeapon.transform.localPosition = Vector3.zero;
        agent.worldState.AddStates(GOAP_States.HasMeele);
        agent.PopAction();
    }
}
