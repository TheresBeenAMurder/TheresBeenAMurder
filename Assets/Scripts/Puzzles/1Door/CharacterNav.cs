using UnityEngine;
using UnityEngine.AI;

public class CharacterNav : MonoBehaviour
{
    public Transform endPos;

    private NavMeshAgent agent;

    public NPCAnimator animator;

    bool walkStarted = false;
    public float distance = 0.1f;

    public void Move()
    {
        agent.SetDestination(endPos.position);
        animator.changeState(NPCAnimator.CHARACTERSTATE.WALKFORWARD);
        walkStarted = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Move();
        }

        if(walkStarted)
        {
            if(Vector3.Distance(transform.position, endPos.position) < distance)
            {
                walkStarted = false;
                animator.changeState(NPCAnimator.CHARACTERSTATE.IDLE);
            }
        }
    }

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();	
	}
}
