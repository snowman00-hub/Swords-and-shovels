using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour, ISkill
{
    public virtual bool TryCast(Mana mana, SkillData data)
    {
        if (!mana || !data)
            return false;

        if (!mana.TrySpend(data.manaCost)) 
            return false;

        return true;
    }

    public virtual bool OnCast(Mana mana, Vector3 target) => true;
}