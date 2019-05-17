using UnityEngine;
using UnityEngine.AI;

public class CharacterNav : MonoBehaviour
{
    public Transform endPos;
    public NavMeshAgent agent;
    public NPCAnimator animator;

    [HideInInspector]
    bool walkStarted = false;
    public bool walkEnded = false;
    public float distance = 0.1f;
    public bool isMavis = false;

    public void Move()
    {
       // agent.updatePosition = false;
        agent.SetDestination(endPos.position);
        animator.changeState(NPCAnimator.CHARACTERSTATE.WALKFORWARD);
        walkStarted = true;
    }

    private void Update()
    {
        if(walkStarted)
        {
            agent.nextPosition = transform.position;
           // transform.rotation = agent.transform.rotation;

            if (Vector3.Distance(animator.transform.position, endPos.position) < distance)
            {
                agent.speed = 0;
                walkStarted = false;
                animator.changeState(NPCAnimator.CHARACTERSTATE.IDLE);
            }

            walkEnded = true;
        }
    }

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
	}
}
