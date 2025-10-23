using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Meteor : Skill
{
    [Header("Data/Refs")]
    [SerializeField] private SkillData data;
    [SerializeField] private Projectile projectile;

    [Header("Projectile Params")]
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private GameObject hitVfx;

    public override bool OnCast(Mana mana, Vector3 target, Vector3 castPoint)
    {
        if (!data || !projectile || !mana)
        {
            return false;
        }

        if (!TryCast(mana, data))
        {
            return false;
        }

        if (!mana.TrySpend(data.manaCost))
        {
            return false;
        }
 
        var dir = target - castPoint;
        if (dir.sqrMagnitude < 0.000001f) dir = transform.forward;

        var rot = Quaternion.LookRotation(dir);
        var proj = Instantiate(projectile, castPoint, rot);

        proj.Launch(castPoint, dir, data.range, data.baseDamage, hitMask, gameObject, hitVfx);

        StartCooldown(data.cooldown);
        return true;
    }
}