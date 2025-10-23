using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float speed = 20f;
    [SerializeField] float maxDistance = 20f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float hitRadius = 0.12f;               // ← 추가: 판정 반경
    [SerializeField] LayerMask hitMask;
    [SerializeField] GameObject hitVfx;

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
        if (vfx) hitVfx = vfx;

        traveled = 0f;
        life = 0f;
        firstTickChecked = false;

        if (dir.sqrMagnitude > 0f)
            transform.rotation = Quaternion.LookRotation(dir);

        gameObject.SetActive(true);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        // ★ 첫 프레임: 내부에서 시작했는지 오버랩으로 한 번 확인
        if (!firstTickChecked)
        {
            firstTickChecked = true;
            var overlaps = Physics.OverlapSphere(transform.position, hitRadius, hitMask, QueryTriggerInteraction.Collide);
            if (overlaps.Length > 0)
            {
                var col = overlaps[0];
                OnHit(col, transform.position, -dir); // 노말은 대충 반대방향
                return;
            }
        }

        // ★ 터널링 방지: SphereCast + 트리거도 충돌
        if (Physics.SphereCast(transform.position, hitRadius, dir, out var hit, step, hitMask, QueryTriggerInteraction.Collide))
        {
            OnHit(hit.collider, hit.point, hit.normal);
            return;
        }

        // 이동
        transform.position += dir * step;
        traveled += step;

        // 수명/거리 초과
        life += Time.deltaTime;
        if (life >= lifeTime || traveled >= maxDistance)
            Destroy(gameObject);
    }

    void OnHit(Collider col, Vector3 point, Vector3 normal)
    {
        // 자기 쏜 놈은 무시하고 싶으면 레이어로 분리하는 게 베스트 (코드 필터도 가능)
        var target = col.GetComponentInParent<IDamageable>();
        if (target != null)
            target.OnDamage(damage, point);

        if (hitVfx)
        {
            var v = Instantiate(hitVfx, point, Quaternion.LookRotation(normal));
            Destroy(v, 1.5f);
        }
        Destroy(gameObject); // 풀링이면 비활성화
    }
}