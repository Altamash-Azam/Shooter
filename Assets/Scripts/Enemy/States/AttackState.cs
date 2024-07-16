using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AttackState : BaseState
{
    private float movetimer; //make the enemy move after a while to make them harder to hit
    private float loosePlayerTimer;
    public float shotTimer;

    public override void Enter()
    {
        
    }

    public override void Perform(){
        if(enemy.CanSeePlayer()){
            loosePlayerTimer = 0;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if(shotTimer > enemy.fireRate){//then shoot
                Shoot();
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

    public void Shoot(){
        // get reference got gun barrel
        Transform barrel = enemy.gunBarrel;
        // instantiate bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/bullet") as GameObject, barrel.position, enemy.transform.rotation);
        // calculate direction
        Vector3 shootDirection = (enemy.Player.transform.position - barrel.transform.position).normalized;
        // add force to rigid body
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * 40;

        Debug.Log("Shoot");
        shotTimer = 0;
    }
    
}
