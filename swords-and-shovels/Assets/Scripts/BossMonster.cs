using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AI;

public class BossMonster : MonoBehaviour
{
    public float damage = 30f;

    private Animator animator;
    private NavMeshAgent agent;
    private Transform player;
    public float moveStartDistance = 5f;

    public GameObject fireball;
    public Transform[] firePoints;

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
    private bool isSkillUsing = false;

    public float stopDistance = 2f;

    public float fireCooldown = 5f;
    private float skillTimer = 0f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var firepoint = firePoints[UnityEngine.Random.Range(0, firePoints.Length)];
            Instantiate(fireball, firepoint.transform.position, firepoint.transform.rotation);
        }
        var dist = Vector3.Distance(transform.position, player.position);
        if (!isMoveStart)
        {
            if (dist < moveStartDistance)
                MoveStartAsync().Forget();

            return;
        }

        if (isSkillUsing)
            return;

        skillTimer += Time.deltaTime;
        if (skillTimer > fireCooldown)
        {
            skillTimer = 0f;
            FireAsync().Forget();
            return;
        }

        TryAct(dist);
        agent.SetDestination(player.position);
    }

    public int fireCount = 10;
    private async UniTask FireAsync()
    {
        isSkillUsing = true;
        agent.isStopped = true;
        animator.SetTrigger("Spell");

        for (int i = 0; i < fireCount; i++)
        {
            var firepoint = firePoints[UnityEngine.Random.Range(0, firePoints.Length)];
            Instantiate(fireball, firepoint.position, firepoint.rotation);

            await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        }

        agent.isStopped = false;
        isSkillUsing = false;
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
        if (agent.isStopped && timer > attackInterval)
        {
            timer = 0f;
            animator.SetTrigger("Attack");
            var playerhealth = player.GetComponent<PlayerHealth>();
            playerhealth.OnDamage(damage, player.position);
        }
    }

    private void OnDisable()
    {
        VictoryDefeatManager.Instance.Victory();
    }
}