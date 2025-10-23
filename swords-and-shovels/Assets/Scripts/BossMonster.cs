using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMonster : MonoBehaviour
{
    public float damage = 30f;

    private Animator animator;
    private NavMeshAgent agent;
    private Transform player;
    public float moveStartDistance = 5f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = GameObject.FindWithTag(Tag.Player).transform;
    }

    private bool isMoveStart = false;

    public float stopDistance = 2f;

    private void Update()
    {
        var dist = Vector3.Distance(transform.position, player.position);
        if (!isMoveStart)
        {
            if(dist < moveStartDistance)
                MoveStartAsync().Forget();

            return;
        }

        TryAct(dist);
        agent.SetDestination(player.position);
    }

    private async UniTask MoveStartAsync()
    {
        animator.SetTrigger("MoveStart");
        await UniTask.Delay(2200);
        isMoveStart = true;
    }

    public float attackInterval = 2f;
    private float timer = 0f;
    private void TryAct(float dist)
    {
        agent.isStopped = dist < stopDistance;
        timer += Time.deltaTime;
        if(agent.isStopped && timer > attackInterval)
        {
            timer = 0f;
            animator.SetTrigger("Attack");
            var playerhealth = player.GetComponent<PlayerHealth>();
            playerhealth.Ondamage(damage, player.position);
        }
    }
}