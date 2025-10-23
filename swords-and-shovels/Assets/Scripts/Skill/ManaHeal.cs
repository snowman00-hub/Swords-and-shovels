using UnityEngine;

public class ManaHeal : Skill
{

    [Header("Data/Refs")]
    [SerializeField] private SkillData data;
    [SerializeField] private GameObject vfx;
    [SerializeField] public float mana;


    [Header("Projectile Params")]
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private GameObject hitVfx;

    private void Start()
    {
        if (data != null)
            mana = data.baseDamage;
        else
            mana = 100;
    }
    public override bool OnCast(Mana mana, Vector3 target, Vector3 castPoint)
    {
        if (!data || !mana || !vfx)
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
        var proj = Instantiate(vfx, castPoint, rot);
        Destroy(proj, 1.2f);
        StartCooldown(data.cooldown);
        return true;
    }
}
