using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public bool isDead { get; set; }
    public float currentHp { get; set; }
    public float currentMp { get; set; }

    protected virtual void OnEnable()
    {
        isDead = false;
    }

    public virtual void OnDamage(float damage, Vector3 hitPosition)
    {
        currentHp -= damage;
        if(currentHp <= 0)
        {
            currentHp = 0;
            gameObject.SetActive(false);            
            isDead = true;
        }
    }


}
