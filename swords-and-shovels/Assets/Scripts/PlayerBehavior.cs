using UnityEngine;
using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices; // UniTask 사용을 위한 using문

public class PlayerBehavior : MonoBehaviour
{
    public readonly string attack = "ATTACK";
    public readonly string monsterTag = "Monster";

    [SerializeField] private MonsterHealth monster;

    private float attackPower = 10;
    private bool isAttacked = false;
    private bool isInTrigger = false;
    private Animator animator;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacked && isInTrigger)
        {
            //AsyncAttack();
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

    private void Attack(MonsterHealth monster, float damage, Vector3 hitPosition)
    {
        animator.SetBool(attack, true);
        monster.OnDamage(attackPower, hitPosition);
    }
    public void Hit()
    {
    }

    private async UniTaskVoid AsyncAttack()
    {
        isAttacked = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 hitPosition = transform.position;

        if (Physics.Raycast(ray, out hit))
        {
            hitPosition = hit.point;
        }

        Attack(monster, attackPower, hitPosition);
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
