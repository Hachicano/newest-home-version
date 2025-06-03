using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    [SerializeField] private Image dashImage;
    [SerializeField] private Image parryImage;
    [SerializeField] private Image crystalImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image blackholeImage;
    [SerializeField] private Image flaskImage;
    [SerializeField] private Image flaskDefaultImage;

    [Header("Souls Info")]
    [SerializeField] private TextMeshProUGUI currentSouls;
    [SerializeField] private float soulsAmount;
    [SerializeField] private float increaseRate = 2000;
    private SkillManager skill;

    void Start()
    {
        if (playerStats != null)  // 如果开局不是满血的话好像会有问题
        {
            playerStats.onHealthChanged += UpdateHealthUI;
        }

        skill = SkillManager.instance;
        if (Inventory.instance.GetEquipment(EquipmentType.Flask) != null)
        {
            flaskImage.sprite = Inventory.instance.GetEquipment(EquipmentType.Flask).icon;
            flaskDefaultImage.sprite = Inventory.instance.GetEquipment(EquipmentType.Flask).icon;
        }
        CheckLock();
    }

    void Update()
    {
        UpdateSoulsUI();

        if (Input.GetKeyDown(KeyCode.LeftShift) && skill.dash.dashUnlocked)
            SetCooldownOf(dashImage);

        if (Input.GetKeyDown(KeyCode.F) && skill.parry.parryUnlocked)
            SetCooldownOf(parryImage);

        if (Input.GetKeyDown(KeyCode.Mouse3) && skill.crystal.crystalUnlocked)
            SetCooldownOf(crystalImage);

        if (Input.GetKeyDown(KeyCode.Mouse1) && skill.sword.swordUnlocked)
            SetCooldownOf(swordImage);

        if (skill.blackhole.blackholeUnlocked && PlayerManager.instance.player.blackholeState.skillFinished)
            SetCooldownOf(blackholeImage);

        if (Input.GetKeyDown(KeyCode.R) && Inventory.instance.GetEquipment(EquipmentType.Flask) != null)
            SetCooldownOf(flaskImage);

        if (skill.dash.dashUnlocked)
            CheckCooldownOf(dashImage, skill.dash.cooldown);

        if (skill.parry.parryUnlocked)
            CheckCooldownOf(parryImage, skill.parry.cooldown);

        if (skill.crystal.crystalUnlocked)
            CheckCooldownOf(crystalImage, skill.crystal.cooldown);

        if (skill.sword.swordUnlocked)
            CheckCooldownOf(swordImage, skill.sword.cooldown);

        if (Inventory.instance.GetEquipment(EquipmentType.Flask) != null)
            CheckCooldownOf(flaskImage, Inventory.instance.flaskCooldown);

        if (skill.blackhole.blackholeUnlocked && PlayerManager.instance.player.blackholeState.skillFinished)
            CheckCooldownOf(blackholeImage, skill.blackhole.cooldown);
    }

    private void UpdateSoulsUI()
    {
        if (soulsAmount < PlayerManager.instance.GetCurrency())
        {
            soulsAmount += Time.deltaTime * increaseRate;
        }
        else
        {
            soulsAmount = PlayerManager.instance.GetCurrency();
        }

        currentSouls.text = ((int)soulsAmount).ToString("#,#");
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetTotalMaxHealthValue();
        slider.value = playerStats.currentHealth;
    }

    private void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }

    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
        }
    }

    private void SetLock(Image _image)
    {
        _image.fillAmount = 1;
    }

    private void SetUnlock(Image _image)
    {
        _image.fillAmount = 0;
    }

    private void OnEnable()
    {
        if (Inventory.instance.GetEquipment(EquipmentType.Flask) != null)
        {
            flaskImage.sprite = Inventory.instance.GetEquipment(EquipmentType.Flask).icon;
            flaskDefaultImage.sprite = Inventory.instance.GetEquipment(EquipmentType.Flask).icon;
        }

        CheckLock();
    }

    private void CheckLock()
    {
        if (!skill.dash.dashUnlocked)
            SetLock(dashImage);
        else if (!skill.dash.isCooldown())
            SetUnlock(dashImage);

        if (!skill.parry.parryUnlocked)
            SetLock(parryImage);
        else if (!skill.parry.isCooldown())
            SetUnlock(parryImage);

        if (!skill.crystal.crystalUnlocked)
            SetLock(crystalImage);
        else if (!skill.crystal.isCooldown())
            SetUnlock(crystalImage);

        if (!skill.sword.swordUnlocked)
            SetLock(swordImage);
        else if (!skill.sword.isCooldown())
            SetUnlock(swordImage);

        if (Inventory.instance.GetEquipment(EquipmentType.Flask) == null)
            SetLock(flaskImage);
        else if (!Inventory.instance.isFlaskCooldown())
            SetUnlock(flaskImage);

        if (!skill.blackhole.blackholeUnlocked)
            SetLock(blackholeImage);
        else if (!skill.blackhole.isCooldown())
            SetUnlock(blackholeImage);
    }
}
