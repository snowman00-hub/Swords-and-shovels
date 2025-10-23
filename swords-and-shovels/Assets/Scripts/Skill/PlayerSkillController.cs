using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    [SerializeField] private Mana playerMana;
    [Header("Fireball")]
    [SerializeField] private Fireball fireball;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var cam = Camera.main;
            if (cam == null) return;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPos = ray.direction;

            fireball.OnCast(playerMana, targetPos);
        }
    }
}