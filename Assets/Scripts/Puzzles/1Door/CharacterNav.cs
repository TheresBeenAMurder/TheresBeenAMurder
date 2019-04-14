using UnityEngine;
using UnityEngine.AI;

public class CharacterNav : MonoBehaviour
{
    public Transform endPos;

    public NavMeshAgent agent;

    public NPCAnimator animator;

    bool walkStarted = false;
    public float distance = 0.1f;
    public bool isMavis = false;

    public void Move()
    {
       // agent.updatePosition = false;
        agent.SetDestination(endPos.position);
        animator.changeState(NPCAnimator.CHARACTERSTATE.WALKFORWARD);
        walkStarted = true;
        //Debug.Log(agent.destination);
    }

    private void Update()
    {
       


        if(walkStarted)
        {

            agent.speed = animator.animator.deltaPosition.magnitude / Time.deltaTime;
            //Debug.Log(Vector3.Distance(animator.transform.position, endPos.position));
            if (Vector3.Distance(animator.transform.position, endPos.position) < distance)
            {
                agent.speed = 0;
                walkStarted = false;
                animator.changeState(NPCAnimator.CHARACTERSTATE.IDLE);
            }
        }
    }

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        //if (!isMavis)
        //{
        //    agent.updatePosition = false;
        //}
        //agent.updateUpAxis = false;
	}
}
