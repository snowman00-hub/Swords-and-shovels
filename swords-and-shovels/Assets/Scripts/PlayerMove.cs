using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 마우스 왼쪽 클릭 시
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 바닥(또는 Collider)에 맞았을 때
            if (Physics.Raycast(ray, out hit))
            {
                // 이동 목표 위치 지정
                agent.SetDestination(hit.point);
            }
        }
    }
}