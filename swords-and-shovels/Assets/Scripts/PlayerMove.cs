using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public static readonly int hashSpeed = Animator.StringToHash("Speed");

    private NavMeshAgent agent;
    private Animator animator;

    private bool isMoveEvent = false;

    public Transform door1;
    public Transform door2;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // 테스트 코드
        if(Input.GetKeyDown(KeyCode.M))
        {
            transform.position = new Vector3(-0.8f, -0.8f, 18f);
        }
        //

        animator.SetFloat(hashSpeed, agent.velocity.magnitude);

        if (isMoveEvent)
            return;

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

                if(hit.collider.CompareTag(Tag.Doorway))
                {
                    float dist1 = Vector3.Distance(transform.position, door1.position);
                    float dist2 = Vector3.Distance(transform.position, door2.position);
                    Transform fartherDoor = (dist1 > dist2) ? door1 : door2;
                    agent.SetDestination(fartherDoor.position);
                    return;
                }

                agent.SetDestination(hit.point);
            }
        }
    }
}