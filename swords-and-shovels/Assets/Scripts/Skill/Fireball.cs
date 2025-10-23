using UnityEngine;

public class Fireball : Skill
{
    [Header("Data/Refs")]
    [SerializeField] private SkillData data;        // ��
    [SerializeField] private Projectile projectile; // �� (Projectile��)
    private Transform castPoint;   // �ɼ�

    [Header("Projectile Params")]
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private GameObject hitVfx;

    public override bool OnCast(Mana mana, Vector3 target)
    {
        if (!data || !projectile || !mana)
        {
            Debug.LogError("Fireball: data/projectile/mana ����");
            return false;
        }

        // 1) �˻縸
        if (!TryCast(mana, data))
        {
            Debug.Log($"Fireball cast = false (ready={IsReady}, cd={CooldownRemain:F2}s, mana={mana?.GetCurrentMana()})");
            return false;
        }

        // 2) ���� ���� (�� ���⼭��)
        if (!mana.TrySpend(data.manaCost))
        {
            Debug.Log("Fireball: TrySpend failed");
            return false;
        }

        // 3) �߻�
        var origin = castPoint ? castPoint.position : transform.position;
        var dir = target - origin;
        if (dir.sqrMagnitude < 1e-6f) dir = transform.forward;

        var proj = Instantiate(projectile);
        proj.Launch(origin, dir, data.range, data.baseDamage, hitMask, gameObject, hitVfx);

        // 4) �� ���� (�� ���⼭��)
        StartCooldown(data.cooldown);
        return true;
    }

    public void SetCastPoint(Transform t) => castPoint = t;
}