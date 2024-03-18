using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    PlayerAnimations playerAnimation;
    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimations>();
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
    }
}