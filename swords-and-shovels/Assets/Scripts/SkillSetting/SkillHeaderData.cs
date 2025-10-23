using UnityEngine;

public abstract class SkillHeaderData : ScriptableObject
{
    [Header("Identity")]
    public string id;
    public string displayName;

    [Header("Costs/Cooldown")]
    public float manaCost = 0f;
    public float cooldown = 0f;

    [Header("FX")]
    public GameObject castVfxPrefab;
}
