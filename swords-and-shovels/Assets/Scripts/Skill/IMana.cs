using UnityEngine;

public interface IMana
{
    bool TrySpend(float amount);
    void Restore(float amount);
}
