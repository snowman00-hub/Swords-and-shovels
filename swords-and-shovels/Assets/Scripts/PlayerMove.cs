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
        // ���콺 ���� Ŭ�� ��
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // �ٴ�(�Ǵ� Collider)�� �¾��� ��
            if (Physics.Raycast(ray, out hit))
            {
                // �̵� ��ǥ ��ġ ����
                agent.SetDestination(hit.point);
            }
        }
    }
}