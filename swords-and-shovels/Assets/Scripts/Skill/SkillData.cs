using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : SkillHeaderData
{
    [Header("AreaSkill Params")]
    public float baseDamage = 0;
    public float range = 0;
}
