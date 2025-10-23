using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public readonly string playerTag = "Player";
    public readonly string Attack = "ATTACK";
    public readonly string Speed = "Speed";


    private PlayerHealth player;
    private MonsterHealth monsterHealth;
    private Animator animator;

    private float attackPower = 10;
    private float attackInterval = 1f;
    private bool isInTrigger = false;
    private bool isMovingToPlayer = false;
    private float detectedRange = 5f;
    private float moveSpeed = 5f;
    private float stopDistance = 2f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerHealth>();
        }
        if (monsterHealth == null)
        {
            monsterHealth = GetComponent<MonsterHealth>();
        }
        TrackPlayer().Forget();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isInTrigger = true;
            isMovingToPlayer = false;
            AsyncAttack().Forget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isInTrigger = false;
        }
    }
    public void OnPlayerDead()
    {
        isInTrigger = false;
        isMovingToPlayer = false;
        player = null;
    }

    public void Hit()
    {
        if (player != null && !player.isDead)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 hitPosition = transform.position;

            if (Physics.Raycast(ray, out hit))
            {
                hitPosition = hit.point;
            }
            player.OnDamage(attackPower, hitPosition);
        }
    }

    private async UniTaskVoid AsyncAttack()
    {
        while (isInTrigger && player != null && !player.isDead && monsterHealth != null && !monsterHealth.isDead)
        {
            animator.SetFloat(Speed, 0f); 
            animator.SetTrigger(Attack);
            Hit();
            await UniTask.Delay(TimeSpan.FromSeconds(attackInterval));
        }
    } 

    private async UniTaskVoid TrackPlayer()
    {
        while(monsterHealth != null && !monsterHealth.isDead)
        {
            if(player != null && !player.isDead)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if(distanceToPlayer <= detectedRange && distanceToPlayer > stopDistance && !isInTrigger)
                {
                    isMovingToPlayer = true;
                    MoveTowardsPlayer();
                }
                else if (distanceToPlayer > detectedRange)
                {
                    isMovingToPlayer = false;
                    animator.SetFloat(Speed, 0f); 
                }
            }
            else
            {
                isMovingToPlayer= false;
                animator.SetFloat(Speed, 0f); 
            }
            await UniTask.Delay(100);
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null && isMovingToPlayer && !isInTrigger)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            direction.y = 0;
            animator.SetFloat(Speed, 1f); 

            transform.position += direction * moveSpeed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }


}
