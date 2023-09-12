using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{

    public enum GuardState
    {
        Patrol,
        Chase
    }

    public GuardState state;

    NavMeshAgent agent;
    Transform player;
    public Transform waypoints;

    Transform currentTarget;

    public float maxViewDistance = 5;
    public float maxViewAngle = 45;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        state = GuardState.Patrol;
        SelectWaypoint();
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case GuardState.Patrol:

                if(IsPlayerVisible()) {
                    state = GuardState.Chase;
                }

                break;
            case GuardState.Chase:
                FollowPlayer();

                if(!IsPlayerVisible()) {
                    state = GuardState.Patrol;
                    SelectWaypoint();
                }

                break;
        }

    }

    bool IsPlayerVisible()
    {
        if (Vector3.Distance(transform.position, player.position) <= maxViewDistance)
        {
            if (Vector3.Angle(transform.forward, player.position - transform.position) <= maxViewAngle)
            {

                RaycastHit hit;
                Vector3 origin = transform.position + Vector3.up;

                if (Physics.Raycast(origin, player.position - transform.position, out hit, maxViewDistance))
                {
                    Debug.Log($"{hit.collider.tag} - {hit.collider.name} - {hit.distance}");
                    if (hit.collider.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    void FollowPlayer()
    {
        // Va vers la position du joueur, navmeshagent
        agent.SetDestination(player.position);
    }

    void SelectWaypoint()
    {
        int index = Random.Range(0, waypoints.childCount);

        while (waypoints.GetChild(index) == currentTarget)
        {
            index = Random.Range(0, waypoints.childCount);
        }

        currentTarget = waypoints.GetChild(index);
        agent.SetDestination(currentTarget.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == currentTarget && state == GuardState.Patrol)
        {
            SelectWaypoint();
        }
    }

}
