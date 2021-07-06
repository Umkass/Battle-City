using UnityEngine;

[CreateAssetMenu(menuName = "Custom/TowerData", fileName = "NewTowerData")]
public class TowerData : ScriptableObject
{
    [SerializeField] int _level;
    [SerializeField] [Range(0,0.9f)] float _accuracy;
    [SerializeField] Sprite _towerSpriteEnemy;
    [SerializeField] Sprite _towerSpritePlayer;
    public int Level { get => _level; }
    public float Accuracy { get => _accuracy; }
    public Sprite TowerSpriteEnemy { get => _towerSpriteEnemy; }
    public Sprite TowerSpritePlayer { get => _towerSpritePlayer; }
}
