using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    [SerializeField] float coinTest;
    private const string COIN_KEY = "Coins";
    public float Coins { get; set; }
    private void Start()
    {
        Coins = SaveGame.Load(COIN_KEY, coinTest);
    }
    public void AddCoins(float amount)
    {
        Coins += amount;
        SaveGame.Load(COIN_KEY, Coins);
    }
    public void RemoveCoins(float amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            SaveGame.Load(COIN_KEY, coinTest);
        }
    }
}