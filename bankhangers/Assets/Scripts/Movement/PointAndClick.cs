using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClick : PlayerMovement
{
    [SerializeField] private LayerMask groundMask;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = player.AddComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse down");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100, groundMask))
            {
                agent.SetDestination(hit.point);
            }
            else
            {
                Debug.Log("hit nothing");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (agent != null && debug)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(agent.steeringTarget, 0.5f);
        }
    }
}
