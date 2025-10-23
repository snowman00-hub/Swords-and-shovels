using UnityEngine;

public class Mana : MonoBehaviour, IMana
{
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana = 100f;

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
}