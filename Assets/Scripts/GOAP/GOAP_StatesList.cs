﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class GOAP_StatesList
{
    public List<GOAP_States> states;

    public GOAP_StatesList() {
        states = new List<GOAP_States>();
    }

    public GOAP_StatesList(GOAP_StatesList copy) {
        states = new List<GOAP_States>(copy.states);
    }

    public int CompareState(GOAP_StatesList subSet) {
        int differance = 0;
        for (int i = 0; i < subSet.states.Count; i++) {
            if(!states.Contains(subSet.states[i])) 
                differance++;
        }

        return differance;
    }

    public void AddStates(GOAP_StatesList statesToAdd) {
        for (int i = 0; i < statesToAdd.states.Count; i++) {
            if(!states.Contains(statesToAdd.states[i]))
                states.Add(statesToAdd.states[i]);
        }
    }

    public void AddStates(GOAP_States stateToAdd) {
        if(!states.Contains(stateToAdd))
            states.Add(stateToAdd);
    }

    public void RemoveState(GOAP_StatesList statesToRemove) {
        for (int i = 0; i < statesToRemove.states.Count; i++) {
            if(states.Contains(statesToRemove.states[i]))
                states.Remove(statesToRemove.states[i]);
        }
    }

    public void RemoveState(GOAP_States stateToRemove) {
        if(states.Contains(stateToRemove))
            states.Remove(stateToRemove);
    }
}
