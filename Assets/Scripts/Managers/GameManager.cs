using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PlayerData playerData;

    public PlayerData PlayerData => playerData;
    public void AddPlayerXP(float xpAmount)
    {
        PlayerXP playerXP = playerData.GetComponent<PlayerXP>();
        playerXP.AddXP(xpAmount);
    }
}