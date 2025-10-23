using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private float playerMaxHealth = 100f;
    private float playerMaxMp = 100f;
    protected override void OnEnable()
    {
        base.OnEnable();
        currentHp = playerMaxHealth;
        currentMp = playerMaxMp;
    }

    public override void Ondamage(float damage, Vector3 hitPosition)
    {
        if (isDead) return;

        base.Ondamage(damage, hitPosition);
        DamagePopupManager.Instance.ShowDamage(hitPosition, Mathf.FloorToInt(damage));
    }
}
