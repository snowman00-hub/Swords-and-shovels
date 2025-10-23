using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public static readonly int hashSpeed = Animator.StringToHash("Speed");

    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        animator.SetFloat(hashSpeed, agent.velocity.magnitude);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(Tag.Obstacle))
                {
                    var obstacle = hit.collider.gameObject.GetComponent<Obstacle>();
                    obstacle.Open();
                    return;
                }

                agent.SetDestination(hit.point);
            }
        }
    }
}