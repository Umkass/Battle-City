using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour //Корпус
{
    #region Field Declarations

    [SerializeField] List<BaseData> _bases;
    public List<BaseData> Bases { get => _bases; private set => _bases = value; }
    public SpriteRenderer SpriteRendererBase { get; private set; }
    public int Level { get; private set; } = 0;
    public float Speed { get; private set; } = 0;
    public int Health { get; private set; } = 0;

    #endregion
    private void Awake()
    {
        SpriteRendererBase = GetComponent<SpriteRenderer>();
    }

    #region Base Initialization 

    public void RandomBase() //передать данные рандомного уровня корпуса
    {
        BaseData currentBase = Bases[Random.Range(0, 3)];
        if (GetComponentInParent<BotControllerFactory>())
        {
            if (GetComponentInParent<BotControllerFactory>().isEnemy)
                SpriteRendererBase.sprite = currentBase.BaseSpriteEnemy;
            else
                SpriteRendererBase.sprite = currentBase.BaseSpritePlayer;
        }
        else
        {
            SpriteRendererBase.sprite = currentBase.BaseSpritePlayer;
        }
        Level = currentBase.Level;
        Speed = currentBase.Speed;
        Health = currentBase.Health;
    }
    public void ChangeBase(Base currentBase, Base newBase) //передать значения из одного корпуса к другому
    {
        BaseData newBaseDefinition = newBase.Bases[newBase.Level - 1];
        if (GetComponentInParent<BotControllerFactory>())
        {
            if (GetComponentInParent<BotControllerFactory>().isEnemy)
                currentBase.SpriteRendererBase.sprite = newBaseDefinition.BaseSpriteEnemy;
            else
                currentBase.SpriteRendererBase.sprite = newBaseDefinition.BaseSpritePlayer;
        }
        else
        {
            currentBase.SpriteRendererBase.sprite = newBaseDefinition.BaseSpritePlayer;
        }
        currentBase.Level = newBaseDefinition.Level;
        currentBase.Speed = newBaseDefinition.Speed;
        currentBase.Health = newBaseDefinition.Health;
    }

    #endregion
}
