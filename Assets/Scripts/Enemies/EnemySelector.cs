using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    [SerializeField] GameObject selectorSprite;
    private EnemyBrain brain;
    private void Awake()
    {
        brain = GetComponent<EnemyBrain>();
    }
    private void Start()
    {
        SelectionManager.OnEnemySelected += ActivateSelector;
        SelectionManager.OnNoSelection += DeactivateSelector;
    }
    private void ActivateSelector(EnemyBrain enemyBrain)
    {
        if (brain == enemyBrain)
            selectorSprite.SetActive(true);
        else
            selectorSprite.SetActive(false);
    }
    public void DeactivateSelector()
    {
        selectorSprite.SetActive(false);
    }
}