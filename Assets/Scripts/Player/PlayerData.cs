using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    private PlayerAnimations playerAnimation;
    public PlayerMana playerMana;
    private PlayerHealth playerHealth;

    [SerializeField] Item_HealthPotion healthPotion;
    [SerializeField] Item_ManaPotion manaPotion;

    public PlayerStats PlayerStats => playerStats;
    public PlayerHealth PlayerHealth => playerHealth;
    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimations>();
        playerMana = GetComponent<PlayerMana>();
        playerHealth = GetComponent<PlayerHealth>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var result = healthPotion.UseItem();
            if (result)
            {
                print("Used health potion");
            }
            else
            {
                print("health potion not used");
            }
            result = manaPotion.UseItem();
            if (result)
            {
                print("Used mana potion");
            }
            else
            {
                print("mana potion not used");
            }
        }
    }
    public void PlayerReset()
    {
        playerStats.ResetPlayer();
        playerAnimation.handleReviveAnimation();
        playerMana.ResetMana();
    }
}