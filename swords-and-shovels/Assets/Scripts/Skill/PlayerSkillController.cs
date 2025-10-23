using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    public readonly string skill = "SKILL";
    public readonly string monster = "Monster";
    private Animator animator;

    [SerializeField] private Mana playerMana;
    [SerializeField] private CursorManager cursorManager;
    [SerializeField] private Transform castPoint;
    [Header("Fireball")]
    [SerializeField] private Fireball fireball;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        fireball.SetCastPoint(castPoint);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && cursorManager.GetCursor())
        {
            var cam = Camera.main;
            if (cam == null) return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 500f, LayerMask.GetMask(monster), QueryTriggerInteraction.Collide))
            {
                Vector3 targetPos = hit.point;
                if (fireball.OnCast(playerMana, targetPos))
                {
                    animator.SetTrigger(skill);
                    Stomp();
                }

            }
        }
    }

    public void Stomp()
    { }
}