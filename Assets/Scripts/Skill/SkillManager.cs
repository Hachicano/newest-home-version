using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Skill_Dash dash {  get; private set; }
    public Skill_Clone clone { get; private set; }
    public Skill_Sword sword { get; private set; }
    public Skill_Blackhole blackhole { get; private set; }
    public Skill_Crystal crystal { get; private set; }
    public Skill_Parry parry { get; private set; }
    public Skill_Dodge dodge { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }

        dash = GetComponent<Skill_Dash>();
        clone = GetComponent<Skill_Clone>();
        sword = GetComponent<Skill_Sword>();
        blackhole = GetComponent<Skill_Blackhole>();
        crystal = GetComponent<Skill_Crystal>();
        parry = GetComponent<Skill_Parry>();
        dodge = GetComponent<Skill_Dodge>();
    }

    private void Start()
    {

    }
}
