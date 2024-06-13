using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestCardPlayer : QuestCard
{
    [SerializeField] TextMeshProUGUI status, goldReward, xpReward, itemQty;
    [SerializeField] Image itemIcon;
    [SerializeField] GameObject claimButton, rewardPannel;

    private void Update()
    {
        status.text = $"Status\n{QuestToComplete.CurrentStatus}/{QuestToComplete.QuestGoal}";
    }

    private void OnEnable()
    {
        QuestCompletedCheck();
    }
    public override void ConfigQuestUI(Quest quest)
    {
        base.ConfigQuestUI(quest);
        status.text = $"Status\n{quest.CurrentStatus}/{quest.QuestGoal}";
        goldReward.text = quest.GoldReward.ToString();
        xpReward.text = quest.XPReward.ToString();

        itemIcon.sprite = quest.ItemReward.Item.Icon;
        itemQty.text = quest.ItemReward.Quantity.ToString();
    }
    private void QuestCompletedCheck()
    {
        if (QuestToComplete.QuestCompleted)
        {
            claimButton.SetActive(true);
            rewardPannel.SetActive(false);
        }
    }
    public void ClaimQuest()
    {
        GameManager.i.AddPlayerXP(QuestToComplete.XPReward);
        Inventory.i.AddItem(QuestToComplete.ItemReward.Item, QuestToComplete.ItemReward.Quantity);
        CoinManager.i.AddCoins(QuestToComplete.GoldReward);
        gameObject.SetActive(false);
    }
}