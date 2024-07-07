using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

    private void Update() {
        if(activeState != null){
            activeState.Perform();
        }
    }

    public void Initialise(){
        // setup defalut state
        // create a new patrol state to give input to cahnge state rather than using reference
        ChangeState(new PatrolState());
    }

    public void ChangeState(BaseState newState){
        if(activeState != null){
            // run cleanup on the new state
            activeState.Exit();
        }
        // change to new state
        activeState = newState;
        // fialsafe to check new state is not null
        if(activeState != null){
            // setup new state
            activeState.stateMachine = this;
            // assign state enemy class
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
