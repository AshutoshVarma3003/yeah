using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    public float radius = 8f;
    public float waitTime = 3f;

    NavMeshAgent agent;
    float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = waitTime;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime && !agent.pathPending)
        {
            Vector3 dest = RandomNavPoint(transform.position, radius);
            agent.SetDestination(dest);
            timer = 0f;
        }

        GetComponent<Animator>()
    .SetFloat("Speed", agent.velocity.magnitude);

    }

    Vector3 RandomNavPoint(Vector3 center, float range)
    {
        Vector3 random = Random.insideUnitSphere * range + center;

        NavMesh.SamplePosition(random, out NavMeshHit hit, range, NavMesh.AllAreas);
        return hit.position;
    }
}
