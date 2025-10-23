using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    private float playerMaxHealth = 100f;
    private float playerMaxMp = 100f;

    [SerializeField] private Slider healthSlider;

    [SerializeField] public int playerdef = 5;

    private void Update()
    {
        SetHealthSlider();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        currentHp = playerMaxHealth;
        currentMp = playerMaxMp;
        def = playerdef;

        healthSlider.maxValue = playerMaxHealth;
        healthSlider.value = currentHp;
    }

    public override void OnDamage(float damage, Vector3 hitPosition)
    {
        if (isDead)
            return;

        base.OnDamage(damage, hitPosition);

        if (currentHp == 0)
        VictoryDefeatManager.Instance.Defeat();
        DamagePopupManager.Instance.ShowDamage(hitPosition, Mathf.FloorToInt(damage));
    }

    public void SetHealthSlider()
    {
        healthSlider.maxValue = playerMaxHealth;
        healthSlider.value = currentHp;
    }
}
