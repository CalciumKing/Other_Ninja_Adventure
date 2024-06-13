using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerStats ps;

    [Header("Player UI Bars")]
    [SerializeField] Image healthBar;
    [SerializeField] Image manaBar;
    [SerializeField] Image xpBar;

    [Header("Player UI Text")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI manaText;
    [SerializeField] TextMeshProUGUI xpText;

    [Header("Stats UI")]
    [SerializeField] GameObject statsPanel;
    [SerializeField] TextMeshProUGUI statLevel;
    [SerializeField] TextMeshProUGUI statDamage;
    [SerializeField] TextMeshProUGUI statCChance;
    [SerializeField] TextMeshProUGUI statCDamage;
    [SerializeField] TextMeshProUGUI statTotalXP;
    [SerializeField] TextMeshProUGUI statCurrentXP;
    [SerializeField] TextMeshProUGUI statNextLevelXP;

    [Header("Attrbutes UI")]
    [SerializeField] TextMeshProUGUI availablePoints;
    [SerializeField] TextMeshProUGUI strength;
    [SerializeField] TextMeshProUGUI dexterity;
    [SerializeField] TextMeshProUGUI intelligence;

    [Header("Quest Panel")]
    [SerializeField] GameObject npcQuestPanel;
    [SerializeField] GameObject playerQuestPanel;

    private void Start()
    {
        PlayerUpgrade.OnPlayerUpgrade += PlayerUpgraded;
        DialogManager.OnExtraInteraction += HandleExtraInteraction;
    }
    private void PlayerUpgraded()
    {
        UpdateStatsPannel();
    }
    private void Update() { UpdatePlayerUI(); }
    private void UpdatePlayerUI()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, ps.CurrentHealth / ps.MaxHealth, 10f * Time.deltaTime);
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, ps.CurrentMana / ps.MaxMana, 10f * Time.deltaTime);
        xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, ps.CurrentXP / ps.NextLevelXP, 10f * Time.deltaTime);

        levelText.text = $"Level {ps.CurrentLevel}";
        healthText.text = $"{ps.CurrentHealth} / {ps.MaxHealth}";
        manaText.text = $"{ps.CurrentMana} / {ps.MaxMana}";
        xpText.text = $"{ps.CurrentXP} / {ps.NextLevelXP}";
    }
    public void ToggleStatsPanel()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
        if (statsPanel.activeSelf)
            UpdateStatsPannel();
    }
    private void UpdateStatsPannel()
    {
        statLevel.text = ps.CurrentLevel.ToString();
        statDamage.text = ps.BaseDamage.ToString();
        statCChance.text = ps.CriticalChance.ToString();
        statCDamage.text = ps.CriticalDamage.ToString();
        statTotalXP.text = ps.TotalXP.ToString();
        statCurrentXP.text = ps.CurrentXP.ToString();
        statNextLevelXP.text = ps.NextLevelXP.ToString();

        availablePoints.text = $"Points: {ps.AvailablePoints}";
        strength.text = ps.Strength.ToString();
        dexterity.text = ps.Dexterity.ToString();
        intelligence.text = ps.Intelligence.ToString();
    }

    public void ToggleNPCQuestPanel(bool value)
    {
        npcQuestPanel.SetActive(value);
    }
    public void TogglePlayerQuestPanel()
    {
        playerQuestPanel.SetActive(!playerQuestPanel.activeSelf);
    }
    void HandleExtraInteraction(InteractionType type)
    {
        switch (type)
        {
            case InteractionType.Quest:
                ToggleNPCQuestPanel(true);
                break;

            case InteractionType.Shop:
                break;
        }
    }
}