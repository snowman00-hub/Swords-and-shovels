using UnityEngine;
using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices; // UniTask 사용을 위한 using문

public class PlayerBehavior : MonoBehaviour
{
    public readonly string attack = "ATTACK";
    public readonly string monsterTag = "MONSTER";

    [SerializeField] private MonsterHealth monster;

    private float attackPower = 10;
    private bool isAttacked = false;
    private bool isInTrigger = false;
    private Animator animator;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isAttacked && isInTrigger && !monster.isDead)
        {
            AsyncAttack();
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(monsterTag))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(monsterTag))
        {
            isInTrigger = false;
        }
    }
    public void OnMonsterDead()
    {
        isInTrigger = false;
        monster = null;
    }
    private void Attack()
    {
        animator.SetBool(attack, true);
    }
    public void Hit()
    {
        if(monster != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 hitPosition = transform.position;

            if (Physics.Raycast(ray, out hit))
            {
                hitPosition = hit.point;
            }
            monster.Ondamage(attackPower, hitPosition);
        }
    }

    private async UniTaskVoid AsyncAttack()
    {
        isAttacked = true;      

        Attack();

        await AttackAnimationDelay();

        isAttacked = false;
    }

    private async UniTask AttackAnimationDelay()
    {
        float delay = 0.5f;
        await UniTask.Delay(TimeSpan.FromSeconds(delay));
        animator.SetBool(attack, false);
    }
}
