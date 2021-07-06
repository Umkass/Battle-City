using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour //Пушка
{
    #region Field Declarations

    [SerializeField] List<CannonData> _cannons;
    public List<CannonData> Cannons { get => _cannons; private set => _cannons = value; }
    public CannonData currentCannonData; // из-за того что уровней 6, не получится получить текущий уровень в ChangeCannon без этой переменной
    public SpriteRenderer SpriteRendererCannon { get; private set; }
    public int Level { get; private set; } = 0;
    public float Damage { get; private set; } = 0;
    public float Distance { get; private set; } = 0;
    public int Barrel { get; private set; } = 0;

    #endregion
    void Awake()
    {
        SpriteRendererCannon = GetComponent<SpriteRenderer>();
    }

    #region Cannon Initialization 

    public void RandomCannon() //передать данные рандомного уровня пушки
    {
        currentCannonData = Cannons[Random.Range(0, 6)];
        if (GetComponentInParent<BotControllerFactory>())
        {
            if (GetComponentInParent<BotControllerFactory>().isEnemy)
                SpriteRendererCannon.sprite = currentCannonData.CannonSpriteEnemy;
            else
                SpriteRendererCannon.sprite = currentCannonData.CannonSpritePlayer;
        }
        else
        {
            SpriteRendererCannon.sprite = currentCannonData.CannonSpritePlayer;
        }
        Level = currentCannonData.Level;
        Damage = currentCannonData.Damage;
        Distance = currentCannonData.Distance;
        Barrel = currentCannonData.Barrel;
    }
    public void ChangeCannon(Cannon currentCannon, Cannon newCannon) //передать значения из одной пушки к другой
    {
        CannonData newCannonData = newCannon.currentCannonData;
        if (GetComponentInParent<BotControllerFactory>())
        {
            if (GetComponentInParent<BotControllerFactory>().isEnemy)
                currentCannon.SpriteRendererCannon.sprite = newCannonData.CannonSpriteEnemy;
            else
                currentCannon.SpriteRendererCannon.sprite = newCannonData.CannonSpritePlayer;
        }
        else
        {
            currentCannon.SpriteRendererCannon.sprite = newCannonData.CannonSpritePlayer;
        }
        currentCannon.Level = newCannonData.Level;
        currentCannon.Damage = newCannonData.Damage;
        currentCannon.Distance = newCannonData.Distance;
        currentCannon.Barrel = newCannonData.Barrel;
    }

    #endregion
}
