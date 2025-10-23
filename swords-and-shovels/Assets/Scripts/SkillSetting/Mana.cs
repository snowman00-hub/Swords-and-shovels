using UnityEngine;

public class Mana : MonoBehaviour, IMana
{
    public float currentMana { get; set; }
    public float maxMana { get; set; }
    public bool TrySpend(float amount)
    {
        if (currentMana > amount)
        {
            currentMana -= amount;
            return true;
        }
        return false;
    }
    public void Restore(float amount)
    {
        currentMana = Mathf.Min(maxMana, currentMana + amount);
    }
}
