using UnityEngine;

public class Fireball : Skill
{
    public SkillData data;

    private void Update()
    {
        
    }

    public override bool OnCast(Mana mana, Vector3 target)
    {
        if (!TryCast(mana, data))
        {
            Debug.Log($"Fireball cast = false {mana.GetCurrentMana()} / {target}");
            return false;
        }
        Debug.Log($"Fireball cast = true {mana.GetCurrentMana()} / {target}");
        var go = Instantiate(data.castVfxPrefab, target, Quaternion.identity);

        return true;
    }
}
