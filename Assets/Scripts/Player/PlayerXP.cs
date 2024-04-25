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
            //NextLevel();
        }
    }
}