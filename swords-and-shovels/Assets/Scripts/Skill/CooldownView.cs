using UnityEngine;
using UnityEngine.UI;

public class CooldownView : MonoBehaviour
{
    [SerializeField] private Skill skill;

    private Image fill;

    void Awake()
    {
        fill = GetComponent<Image>();
        if (fill)
        {
            fill.type = Image.Type.Filled;
            fill.fillAmount = 1f;
        }
    }

    void Update()
    {
        if (!skill || !fill) return;

        float remain = skill.CooldownRemain;
        float dur = Mathf.Max(0.0001f, skill.LastCooldownDuration);
        float fracRemain = Mathf.Clamp01(remain / dur);
        float display = 1f - fracRemain;

        fill.fillAmount = display;
    }
}
