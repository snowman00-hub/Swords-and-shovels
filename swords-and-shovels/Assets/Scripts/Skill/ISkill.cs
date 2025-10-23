using UnityEngine;

public interface ISkill
{
    bool TryCast(Mana mana, SkillData data);
}
