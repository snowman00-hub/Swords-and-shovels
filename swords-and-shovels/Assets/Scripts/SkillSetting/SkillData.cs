using UnityEngine;

[CreateAssetMenu(fileName = "AreaSkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : SkillHeaderData
{
    [Header("AreaSkill Params")]
    public float baseDamage = 0;
    public float range = 0;
}
