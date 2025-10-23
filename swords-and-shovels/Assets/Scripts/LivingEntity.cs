using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public bool isDead { get; set; }
    public float currentHp { get; set; }
    public float currentMp { get; set; }
    public int def { get; set; }

    protected virtual void OnEnable()
    {
        isDead = false;
    }

    public virtual void OnDamage(float damage, Vector3 hitPosition)
    {
        float reduced = Mathf.Max(0f, damage - def);
        currentHp -= reduced;

        if(currentHp <= 0)
        {
            currentHp = 0;
            gameObject.SetActive(false);
            isDead = true;
        }
    }
}
