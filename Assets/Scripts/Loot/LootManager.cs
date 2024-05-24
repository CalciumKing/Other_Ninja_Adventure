using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [SerializeField] GameObject lootPanel;
    [SerializeField] Transform container;
    [SerializeField] LootButton lootButton;
    private bool IsLootPanelEmpty()
    {
        return container.childCount > 0;
    }
    public void ClosePanel()
    {
        lootPanel.SetActive(false);
    }
    public void ShowLoot(EnemyLoot enemyLoot)
    {
        lootPanel.SetActive(true);
        if (!IsLootPanelEmpty())
            foreach (Transform child in container)
                Destroy(child.gameObject);

        foreach (DroppedItem item in enemyLoot.DroppedItems)
        {
            if (item.PickedItem)
                continue;

            LootButton button = Instantiate(lootButton, container);
            button.SetData(item);
        }
    }
}