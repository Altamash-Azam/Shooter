using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    [SerializeField] private string currentState;

    public GameObject Player {get => player;}

    public NavMeshAgent Agent { get=> agent;}

    [Header("Sight values")]
    public float sightDistance = 20f;
    public float fieldOfview = 85f;
    public float eyeHeight;

    [Header("Weapon Values")]
    [SerializeField]public Transform gunBarrel;
    [Range(0.1f,10)]
    public float fireRate;

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
