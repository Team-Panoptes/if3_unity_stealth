using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    
    NavMeshAgent agent;
    Transform player;
    public Transform waypoints;

    Transform currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        SelectWaypoint();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FollowPlayer() {
        // Va vers la position du joueur, navmeshagent
        agent.SetDestination(player.position);
    }

    void SelectWaypoint() {
        int index = Random.Range(0, waypoints.childCount);
        
        while(waypoints.GetChild(index) == currentTarget) {
            index = Random.Range(0, waypoints.childCount);
        }
        
        currentTarget = waypoints.GetChild(index);
        agent.SetDestination(currentTarget.position);
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("ON TRIGGER ENTER!!");
        if (other.transform == currentTarget) {
            SelectWaypoint();
        }
    }

}
