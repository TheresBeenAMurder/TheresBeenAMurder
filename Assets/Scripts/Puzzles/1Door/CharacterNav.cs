using UnityEngine;
using UnityEngine.AI;

public class CharacterNav : MonoBehaviour
{
    public Transform endPos;

    private NavMeshAgent agent;

    public void Move()
    {
        agent.SetDestination(endPos.position);
    }

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();	
	}
}
