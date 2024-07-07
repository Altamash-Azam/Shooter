using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float movetimer; //make the enemy move after a while to make them harder to hit
    private float loosePlayerTimer;

    public override void Enter()
    {
        
    }

    public override void Perform(){
        if(enemy.CanSeePlayer()){
            loosePlayerTimer = 0;
            movetimer += Time.deltaTime;
            if(movetimer > Random.Range(3,10)){
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                movetimer = 0;
            }
        }
        else{
            loosePlayerTimer += Time.deltaTime;
            if(loosePlayerTimer > 5){
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public override void Exit()
    {
        
    }


    
}
