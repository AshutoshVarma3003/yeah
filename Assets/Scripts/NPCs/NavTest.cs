using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    public float radius = 8f;
    public Vector2 waitTimeRange = new Vector2(5f, 10f);
    float currentWaitTime;

    public float turnSpeed = 360f;

    NavMeshAgent agent;
    float timer;

    bool isTurning;
    Quaternion targetRotation;
    Vector3 targetDestination;

    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.updateRotation = false;   // we control rotation
        agent.angularSpeed = 0f;

        currentWaitTime = Random.Range(waitTimeRange.x, waitTimeRange.y);
        timer = currentWaitTime;

    }

    void Update()
    {
        timer += Time.deltaTime;

        // PICK NEW DESTINATION
        if (!isTurning && !agent.isStopped && timer >= currentWaitTime && agent.remainingDistance <= agent.stoppingDistance)
        {
            targetDestination = RandomNavPoint(transform.position, radius);

            Vector3 dir = (targetDestination - transform.position);
            dir.y = 0;

            if (dir.sqrMagnitude > 0.01f)
            {
                targetRotation = Quaternion.LookRotation(dir.normalized);
                isTurning = true;
                agent.isStopped = true;
            }

            timer = 0f;
            currentWaitTime = Random.Range(waitTimeRange.x, waitTimeRange.y);
        }

        // TURN IN PLACE
        if (isTurning)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                transform.rotation = targetRotation;
                isTurning = false;
                agent.isStopped = false;
                agent.SetDestination(targetDestination);
            }
        }

        // ANIMATION
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    Vector3 RandomNavPoint(Vector3 center, float range)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 random = Random.insideUnitSphere * range + center;
            if (NavMesh.SamplePosition(random, out NavMeshHit hit, range, NavMesh.AllAreas))
                return hit.position;
        }

        return center;
    }
}
