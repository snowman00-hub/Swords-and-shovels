using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour, IMana
{
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana = 100f;

    [SerializeField] private Slider ManaSlider;

    private void Update()
    {
        SetManaSlider();
    }

    private void OnEnable()
    {
        ManaSlider.maxValue = maxMana;
        ManaSlider.value = currentMana;
    }
    public bool TrySpend(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            return true;
        }
        return false;
    }

    public void Restore(float amount)
    {
        currentMana = Mathf.Min(maxMana, currentMana + Mathf.Max(0f, amount));
    }

    public float GetCurrentMana()
    {
        return currentMana;
    }
    public void SetCurrentMana(float amount)
    {
        var mp = Mathf.Max(100, currentMana + amount);
        currentMana = mp;
    }

    public void SetManaSlider()
    {
        ManaSlider.maxValue = maxMana;
        ManaSlider.value = currentMana;
    }
}