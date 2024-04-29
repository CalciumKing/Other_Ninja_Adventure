using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] PlayerStats ps;
    public void AddXP(float amount)
    {
        ps.TotalXP += amount;
        ps.CurrentXP += amount;

        while (ps.CurrentXP >= ps.NextLevelXP)
        {
            ps.CurrentXP -= ps.NextLevelXP;
            NextLevel();
        }
    }
    public void NextLevel()
    {
        ps.CurrentLevel++;
        var currentXPRequired = ps.NextLevelXP;
        var newNextLevel = Mathf.Round(currentXPRequired + ps.NextLevelXP * (ps.XPMultiplier / 100f));
        ps.NextLevelXP = newNextLevel;
    }
}