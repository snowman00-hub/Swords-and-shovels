using UnityEngine;

public class Skill : MonoBehaviour, ISkill
{
    public SkillData skilldata;


    public virtual bool TryCast(IMana mana)
    {
        throw new System.NotImplementedException();
    }
}
