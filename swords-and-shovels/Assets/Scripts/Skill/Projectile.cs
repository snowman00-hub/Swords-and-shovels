using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float speed = 20f;
    [SerializeField] float maxDistance = 20f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float hitRadius = 0.12f;               // ← 추가: 판정 반경
    [SerializeField] LayerMask hitMask;
    GameObject hitVfx;

    Vector3 dir;
    float traveled;
    float life;
    float damage;
    GameObject owner;
    bool firstTickChecked;

    public void Launch(
        Vector3 origin, Vector3 direction, float range,
        float dmg, LayerMask mask, GameObject owner, GameObject vfx = null)
    {
        transform.position = origin;
        dir = direction.normalized;
        maxDistance = range;
        damage = dmg;
        hitMask = mask;
        this.owner = owner;

        if (vfx)
            hitVfx = vfx;

        traveled = 0f;
        life = 0f;
        firstTickChecked = false;

        //if (dir.sqrMagnitude > 0f)
        //    transform.rotation = Quaternion.LookRotation(dir);

        gameObject.SetActive(true);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (!firstTickChecked)
        {
            firstTickChecked = true;
            var overlaps = Physics.OverlapSphere(transform.position, hitRadius, hitMask, QueryTriggerInteraction.Collide);
            if (overlaps.Length > 0)
            {
                var col = overlaps[0];
                OnHit(col, transform.position, -dir);
                return;
            }
        }

        if (Physics.SphereCast(transform.position, hitRadius, dir, out var hit, step, hitMask, QueryTriggerInteraction.Collide))
        {
            OnHit(hit.collider, hit.point, hit.normal);
            return;
        }

        transform.position += dir * step;
        traveled += step;

        life += Time.deltaTime;
        if (life >= lifeTime || traveled >= maxDistance)
            Destroy(gameObject);
    }

    void OnHit(Collider col, Vector3 point, Vector3 normal)
    {
        var target = col.GetComponentInParent<IDamageable>();
        if (target != null)
            target.OnDamage(damage, point);

        if (hitVfx)
        {
            var v = Instantiate(hitVfx, point, Quaternion.LookRotation(normal));
            Destroy(v, 1.5f);
        }
        Destroy(gameObject);
    }
}