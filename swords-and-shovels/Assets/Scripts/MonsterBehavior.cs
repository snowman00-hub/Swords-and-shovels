using Cysharp.Threading.Tasks;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public readonly string playerTag = "Player";

    private PlayerHealth player;

    private float attackPower = 10;
    private float attackInterval = 1f;
    private bool isInTrigger = false;


    private void OnEnable()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerHealth>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isInTrigger = true;
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
            player.Ondamage(attackPower, hitPosition);
            Debug.Log("플레이어 공격받음");
        }
    }

    private async UniTaskVoid AsyncAttack()
    {
        while(isInTrigger && player != null && !player.isDead)
        {
            Hit();
            await UniTask.Delay(TimeSpan.FromSeconds(attackInterval));
        }
    }
    


}
