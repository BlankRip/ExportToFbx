using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheScript : MonoBehaviour
{

    public State startState;
    Stack<State> statestack;
    void Start()
    {
        statestack = new Stack<State>();
        //On start setting the state by pushing it into the stack
        statestack.Push(startState);                  //Pushing the state to be set to at the movement
        statestack.Peek().Initialize();               //Runing the start functino for the current state
    }

    // Update is called once per frame
    void Update()
    {
        statestack.Peek().Exicute(this);           //Running Update for current state
        
        //Pop the state from stack and show previous panel
        if (Input.GetKeyDown(KeyCode.Escape) && statestack.Count > 1)
        {
            PopState();
        }
    }

    public void SetState(State state)
    {
        statestack.Peek().Exit();                //Ruuning the Exit function for current state
        statestack.Push(state);                  //Pushing the state to be set to at the movement
        statestack.Peek().Initialize();               //Runing the start functino for the current state
    }

    void PopState()
    {
        statestack.Peek().Exit();                //Ruuning the Exit function for current state
        statestack.Pop();                        //Poping the state out of the stack inturn setting the prevous state to current
        statestack.Peek().Initialize();               //Runing the start functino for the current state
    }
}
