using UnityEngine;

[CreateAssetMenu(menuName = "Custom/BaseData", fileName = "NewBaseData")]
public class BaseData : ScriptableObject
{
    [SerializeField] int _level;
    [SerializeField] float _speed;
    [SerializeField] int _health;
    [SerializeField] Sprite _baseSpriteEnemy;
    [SerializeField] Sprite _baseSpritePlayer;
    public int Level { get => _level; }
    public float Speed { get => _speed; }
    public int Health { get => _health; }
    public Sprite BaseSpriteEnemy { get => _baseSpriteEnemy; }
    public Sprite BaseSpritePlayer { get => _baseSpritePlayer; }
}
