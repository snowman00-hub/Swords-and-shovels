using UnityEngine;
using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices; // UniTask ����� ���� using��

public class PlayerBehavior : MonoBehaviour
{
    public readonly string attack = "ATTACK";
    public readonly string monsterTag = "Monster";

    private MonsterHealth currentTarget;

    private float attackPower = 10;
    private bool isAttacked = false;
    private bool isInTrigger = false;
    private Animator animator;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isAttacked && isInTrigger)
        {
            FindNearestMonster();
            if(currentTarget!=null && !currentTarget.isDead)
            {
                AsyncAttack();
            }
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
            MonsterHealth monster = other.GetComponent<MonsterHealth>();
            if(monster != null && !monster.isDead)
            {
                currentTarget = monster;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(monsterTag))
        {
            MonsterHealth monster = other.GetComponent<MonsterHealth>();
            if(monster == currentTarget)
            {
                currentTarget = null;
            }
            CheckForRemainingMonsters();
        }
    }

    private void FindNearestMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(monsterTag);
        MonsterHealth nearestMonster = null;
        float nearestDistance = float.MaxValue;

        foreach(GameObject monsterObj in monsters)
        {
            if(!monsterObj.activeInHierarchy) continue;

            MonsterHealth monster = monsterObj.GetComponent<MonsterHealth>();
            if(monster == null || monster.isDead) continue;

            float distance = Vector3.Distance(transform.position, monsterObj.transform.position);
            if(distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestMonster = monster;
            }
        }
        currentTarget = nearestMonster;
    }

    private void CheckForRemainingMonsters()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(monsterTag);
        bool hasActiveMonster = false;
        
        foreach(var monsterObj in monsters)
        {
            if(!monsterObj.activeInHierarchy) continue;

            MonsterHealth monster = monsterObj.GetComponent<MonsterHealth>();
            if (monster != null && !monster.isDead)
            {
                float distance = Vector3.Distance(transform.position, monsterObj.transform.position);
                if (distance <= GetComponent<Collider>().bounds.size.magnitude)
                {
                    hasActiveMonster = true;
                    break;
                }
            }
        }
        isInTrigger = hasActiveMonster;
    }
    public void OnMonsterDead()
    {
        isInTrigger = false;
        CheckForRemainingMonsters();
    }
    private void Attack()
    {
        animator.SetBool(attack, true);
    }
    public void Hit()
    {
        if(currentTarget != null && !currentTarget.isDead)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 hitPosition = transform.position;

            if (Physics.Raycast(ray, out hit))
            {
                hitPosition = hit.point;
            }
            currentTarget.OnDamage(attackPower, hitPosition);
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
