using UnityEngine;

public class Fireball : Skill
{
    [Header("Data/Refs")]
    [SerializeField] private SkillData data;        // 必
    [SerializeField] private Projectile projectile; // 必 (Projectile만)
    private Transform castPoint;   // 옵션

    [Header("Projectile Params")]
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private GameObject hitVfx;

    public override bool OnCast(Mana mana, Vector3 target)
    {
        if (!data || !projectile || !mana)
        {
            Debug.LogError("Fireball: data/projectile/mana 누락");
            return false;
        }

        // 1) 검사만
        if (!TryCast(mana, data))
        {
            Debug.Log($"Fireball cast = false (ready={IsReady}, cd={CooldownRemain:F2}s, mana={mana?.GetCurrentMana()})");
            return false;
        }

        // 2) 마나 차감 (★ 여기서만)
        if (!mana.TrySpend(data.manaCost))
        {
            Debug.Log("Fireball: TrySpend failed");
            return false;
        }

        // 3) 발사
        var origin = castPoint ? castPoint.position : transform.position;
        var dir = target - origin;
        if (dir.sqrMagnitude < 1e-6f) dir = transform.forward;

        var proj = Instantiate(projectile);
        proj.Launch(origin, dir, data.range, data.baseDamage, hitMask, gameObject, hitVfx);

        // 4) 쿨 시작 (★ 여기서만)
        StartCooldown(data.cooldown);
        return true;
    }

    public void SetCastPoint(Transform t) => castPoint = t;
}