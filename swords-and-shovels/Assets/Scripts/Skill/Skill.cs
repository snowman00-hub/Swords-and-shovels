using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    //player Mana
    public int playerMana;

    //attackSkill
    public int skillDamage;
    public float cooldown;
    public float range;
    public float requiredMana;
    public float duration;

    //attbuff, defbuff, healbuff, meditation, duration
    public int skillDamageBuff;
    public float defenseBuff;
    public float addHp;
    public float addMana;

    //Block/stop repeated key presses
    public bool meditation;
    public bool useSkillDamageBuff;
    public bool useDefenseBuff;

    //buffDuration(Timer)
    public float skillDamageBuffDuration;
    public float defenseBuffDuration;
    public float meditationDuration;
    private float warningTimer;



    public TextMeshProUGUI warning;

    public void Start()
    {
        playerMana = 100;
        skillDamage = 0;
        cooldown = 0;
        range = 0;
        requiredMana = 0;
        duration = 0;

        skillDamageBuff = 0;
        defenseBuff = 0;
        addHp = 0;
        addMana = 0;

    
        useSkillDamageBuff = false;
        useDefenseBuff = false;
        meditation = false;
        
        skillDamageBuffDuration = 0;
        defenseBuffDuration = 0;
        meditationDuration = 0;
        warningTimer = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseAttackBuff();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UseDefenceBuff();
        }

        UpdateBuffTimer();
    }
    public void UseAttackBuff()
    {
        requiredMana = 10;


        if(playerMana < requiredMana)
        {
            warning.text = "스킬 사용에 마나가 부족합니다.";
        }
        
        if (useSkillDamageBuff)
        skillDamageBuff = 50;

        skillDamageBuffDuration += 3;



        Debug.Log($"dmgduration{skillDamageBuffDuration} / dmg{skillDamageBuff}");
    }
    public void UseDefenceBuff()
    {
        //Player?Hero add Def
        defenseBuff = 10;

        defenseBuffDuration += 3;
        requiredMana = 10;

        Debug.Log($"defduration{defenseBuffDuration} / def{skillDamageBuff}");
    }
    public void UseHeal()
    {
        addHp = 30;
        requiredMana = 20;

        //Add player hp += currenthp + heal;
    }
    public void UseMeditation()
    {
        if (meditation)
            return;

        meditation = true;
        addMana = 30;
        meditationDuration = 5;

        //if meditation = true; / Add player don't move meditationDuration
        //Add player meditationDuration after mana += currentMana + addMana;
    }

    public void UpdateBuffTimer()
    {
        if (skillDamageBuffDuration > 0)
        {
            skillDamageBuffDuration = Mathf.Max(0f, skillDamageBuffDuration - Time.deltaTime);
            if (skillDamageBuffDuration <= 0)
            {
                skillDamageBuffDuration = 0;
                skillDamageBuff = 0;
                Debug.Log($"dmgduration{skillDamageBuffDuration} / dmg{skillDamageBuff}");
            }
        }
        if (defenseBuffDuration > 0)
        {
            defenseBuffDuration = Mathf.Max(0f, defenseBuffDuration - Time.deltaTime);
            if (defenseBuffDuration <= 0)
            {
                defenseBuffDuration = 0;
                defenseBuff = 0;
                Debug.Log($"dmgduration{defenseBuffDuration} / dmg{defenseBuff}");
            }
        }
    }


    public void UseFireBall(Vector3 pos)
    {
        skillDamage = 50 + skillDamageBuff;
        cooldown = 10f;
        range = 10f;
        requiredMana = 40f;

        Physics.OverlapSphere(pos, range);
    }



}
