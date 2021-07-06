using UnityEngine;

[CreateAssetMenu(menuName = "Custom/CannonData", fileName = "NewCannonData")]
public class CannonData : ScriptableObject
{
    private enum CannonType
    {
        ArmorPiercingCannon,
        RapidFireCannon
    }
    [SerializeField] CannonType _cannonType;
    [SerializeField]int _level;
    [SerializeField] float _damage;
    [SerializeField] float _distance;
    [SerializeField] [Range(1, 2)] int _barrel;
    [SerializeField] Sprite _cannonSpriteEnemy;
    [SerializeField] Sprite _cannonSpritePlayer;
    public int Level { get => _level; }
    public float Damage { get => _damage; }
    public float Distance { get => _distance; }
    public int Barrel { get => _barrel; }
    public Sprite CannonSpriteEnemy { get => _cannonSpriteEnemy; }
    public Sprite CannonSpritePlayer { get => _cannonSpritePlayer; }
}
