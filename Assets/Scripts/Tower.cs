using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour //Башня
{
    #region Field Declarations

    [SerializeField] List<TowerData> _towers;
    public List<TowerData> Towers { get => _towers; private set => _towers = value; }
    public SpriteRenderer SpriteRendererTower { get; private set; }
    public int Level { get; private set; } = 0;
    public float Accuracy { get; private set; } = 0;

    #endregion
    void Awake()
    {
        SpriteRendererTower = GetComponent<SpriteRenderer>();
    }

    #region Tower Initialization 

    public void RandomTower() //передать данные рандомного уровня башни
    {
        TowerData currentTower = Towers[Random.Range(0, 3)];
        if (GetComponentInParent<BotControllerFactory>())
        {
            if (GetComponentInParent<BotControllerFactory>().isEnemy)
                SpriteRendererTower.sprite = currentTower.TowerSpriteEnemy;
            else
                SpriteRendererTower.sprite = currentTower.TowerSpritePlayer;
        }
        else
        {
            SpriteRendererTower.sprite = currentTower.TowerSpritePlayer;
        }
        Level = currentTower.Level;
        Accuracy = currentTower.Accuracy;
    }
    public void ChangeTower(Tower currentTower, Tower newTower) //передать значения из одной башни к другой
    {
        TowerData newTowerData = newTower.Towers[newTower.Level - 1];
        if (GetComponentInParent<BotControllerFactory>())
        {
            if (GetComponentInParent<BotControllerFactory>().isEnemy)
                currentTower.SpriteRendererTower.sprite = newTowerData.TowerSpriteEnemy;
            else
                currentTower.SpriteRendererTower.sprite = newTowerData.TowerSpritePlayer;
        }
        else
        {
            currentTower.SpriteRendererTower.sprite = newTowerData.TowerSpritePlayer;
        }
        currentTower.Level = newTowerData.Level;
        currentTower.Accuracy = newTowerData.Accuracy;
    }

    #endregion
}
