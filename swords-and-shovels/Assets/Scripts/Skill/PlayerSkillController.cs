using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    public readonly string skill = "SKILL";
    public readonly string monster = "Monster";
    private Animator animator;
    private PlayerHealth health;
    private Mana mana;

    [SerializeField] private Mana playerMana;
    [SerializeField] private CursorManager cursorManager;
    public Transform castPoint;
    [Header("Meteor")]
    [SerializeField] private Meteor meteor;
    [SerializeField] private Sharks sharks;
    [SerializeField] private Sword sword;
    [SerializeField] private Heal heal;
    [SerializeField] private ManaHeal manaHeal;




    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<PlayerHealth>();
        mana = GetComponent<Mana>();
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
                if (meteor.OnCast(playerMana, targetPos, castPoint.position))
                {
                    animator.SetTrigger(skill);
                    Stomp();
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && cursorManager.GetCursor())
        {
            var cam = Camera.main;
            if (cam == null) return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 500f, LayerMask.GetMask(monster), QueryTriggerInteraction.Collide))
            {
                Vector3 targetPos = hit.point;
                if (sharks.OnCast(playerMana, targetPos, castPoint.position))
                {
                    animator.SetTrigger(skill);
                    Stomp();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && cursorManager.GetCursor())
        {
            var cam = Camera.main;
            if (cam == null) return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 500f, LayerMask.GetMask(monster), QueryTriggerInteraction.Collide))
            {
                Vector3 targetPos = hit.point;
                if (sword.OnCast(playerMana, targetPos, castPoint.position))
                {
                    animator.SetTrigger(skill);
                    Stomp();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && cursorManager.GetCursor())
        {
            var cam = Camera.main;
            if (cam == null) return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 500f, LayerMask.GetMask(monster), QueryTriggerInteraction.Collide))
            {
                Vector3 targetPos = hit.point;
                if (sword.OnCast(playerMana, targetPos, castPoint.position))
                {
                    animator.SetTrigger(skill);
                    Stomp();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (heal.OnCast(playerMana, gameObject.transform.position, gameObject.transform.position))
            {
                float hp = Mathf.Max(100, health.currentHp + heal.heal);
                health.currentHp = hp;
                animator.SetTrigger(skill);
                Stomp();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (manaHeal.OnCast(playerMana, gameObject.transform.position, gameObject.transform.position))
            {
                float mp = Mathf.Max(100, mana.GetCurrentMana() + heal.heal);
                mana.SetCurrentMana(100);
                animator.SetTrigger(skill);
                Stomp();
            }
        }
    }

    public void Stomp()
    { }
}