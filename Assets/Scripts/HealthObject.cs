using System;
using UnityEngine;

public class HealthObject : MonoBehaviour //можно было воспользоваться наследованием
{
    private enum HealthType
    {
        Obstacle,
        BaseEnemy,
        BasePlayer,
        AllyTank,
        PlayerTank,
        EnemyTank
    }
    #region Field Declarations

    public float health;
    [SerializeField] private HealthType _healthType;
    bool Dead = false; //избежать двойной регистрации попадания
    public bool isHit { get; private set; } = false; //получил повреждение (для танка ассистента)

    #endregion

    public Action Died = delegate { };

    void Update()
    {
        if (ScreenBounds.OutOfBounds(transform.position))
        {
            Died?.Invoke();
            Destroy(gameObject);
            if (_healthType == HealthType.BaseEnemy)
                GameManager.Instance.LevelComplete();
            if (_healthType == HealthType.BasePlayer)
                GameManager.Instance.GameOver();
        }
    }

    #region Take damage
    void OnCollisionEnter2D(Collision2D other) //TakeDamage(без friendly fire)
    {
        if (other.gameObject.GetComponent<ProjectileMove>() && Dead != true)
        {
            if (other.gameObject.GetComponent<ProjectileMove>().isPlayers == true && (_healthType == HealthType.BaseEnemy //для красных
                || _healthType == HealthType.EnemyTank || _healthType == HealthType.Obstacle))
                TakeDamage(other.gameObject.GetComponent<ProjectileMove>().damage);
            if (other.gameObject.GetComponent<ProjectileMove>().isPlayers == false && (_healthType == HealthType.BasePlayer //для синих
                || _healthType == HealthType.AllyTank || _healthType == HealthType.Obstacle || _healthType == HealthType.PlayerTank))
                TakeDamage(other.gameObject.GetComponent<ProjectileMove>().damage);
            isHit = true;
            Destroy(other.gameObject);
        }
    }
    void TakeDamage(float damage) //TakeDamage для разных HealthType
    {
        health -= damage;
        if (health <= 0f)
        {
            Dead = true;
            switch ((int)_healthType)
            {
                case 0: // Obstacle
                    Destroy(gameObject);
                    break;
                case 1: // BaseEnemy
                    Destroy(gameObject);
                    GameManager.Instance.LevelComplete();
                    break;
                case 2: // BasePlayer
                    Destroy(gameObject);
                    GameManager.Instance.GameOver();
                    break;
                case 3: // AllyTank
                    gameObject.GetComponent<ModuleController>().SpawnModule();
                    Destroy(gameObject);
                    Died?.Invoke();
                    break;
                case 4: // PlayerTank
                    Destroy(gameObject);
                    Died?.Invoke();
                    break;
                case 5: // EnemyTank
                    gameObject.GetComponent<ModuleController>().SpawnModule();
                    Destroy(gameObject);
                    Died?.Invoke();
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }
        }
    }

    #endregion
}
