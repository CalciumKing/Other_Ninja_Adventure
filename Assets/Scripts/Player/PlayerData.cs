using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    private PlayerAnimations playerAnimation;
    private PlayerMana playerMana;
    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimations>();
        playerMana = GetComponent<PlayerMana>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (playerStats.CurrentHealth <= 0)
            {
                PlayerReset();
            }
        }
    }
    public PlayerStats PlayerStats => playerStats;
    public void PlayerReset()
    {
        playerStats.ResetPlayer();
        playerAnimation.handleReviveAnimation();
        playerMana.ResetMana();
    }
}