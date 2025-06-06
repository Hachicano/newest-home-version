using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Blackhole : Skill
{
    [SerializeField] private UI_SkillTreeSlot blackholeUnlockButton;
    public bool blackholeUnlocked {  get; private set; }

    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private float blackholeDuration;
    [SerializeField] public float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;
    [Space]
    [SerializeField] private int AttackTimes;
    [SerializeField] private float cloneAttackCooldown;

    public Skill_Blackhole_Controller currentBlackhole;
    public bool haveBlackhole;

    private void UnlockBlackhole()
    {
        blackholeUnlocked = blackholeUnlockButton.unlocked;
    }

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newBlackhole = Instantiate(blackholePrefab, player.transform.position, Quaternion.identity);

        currentBlackhole = newBlackhole.GetComponent<Skill_Blackhole_Controller>();

        currentBlackhole.SetupBlackhole(blackholeDuration, maxSize, growSpeed, shrinkSpeed, AttackTimes, cloneAttackCooldown);

        haveBlackhole = true;

        //AudioManager.instance.PlayerSFX(6, null);
        AudioManager.instance.PlayerSFX(34, null);

    }

    protected override void Start()
    {
        base.Start();

        blackholeUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBlackhole);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void CheckUnlock()
    {
        UnlockBlackhole();
    }

    public bool BlackholeSkillFinish()
    {
        if (!currentBlackhole) 
            return false;

        if (currentBlackhole.playerCanExitTheState)
        {
            currentBlackhole = null;
            return true;
        }
        return false;
    }

    public float GetBlackholeRadius() => maxSize / 2;
}
