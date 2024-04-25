using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;


    [SerializeField] Transform player;

    private void Awake()
    {
        i = this;
    }
    public void AddPlayerXP(float xpAmount)
    {
        PlayerXP playerXP = player.GetComponent<PlayerXP>();
        playerXP.AddXP(xpAmount);
    }
}
