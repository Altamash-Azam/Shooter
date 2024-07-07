using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get=> agent;}

    private GameObject player;
    public float sightDistance = 20f;
    public float fieldOfview = 85f;
    public float eyeHeight;

    [SerializeField] private string currentState;
    public Path path;

    private void Start() {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer(){
        if(player != null){
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance){
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if(angleToPlayer >= -fieldOfview && angleToPlayer <= fieldOfview){
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight),targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray,out hitInfo, sightDistance)){
                        if(hitInfo.transform.gameObject == player){
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
