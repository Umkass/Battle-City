using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region  Field Declarations

    [Header("Prefabs")]
    [Space]
    [SerializeField] GameObject _groundPrefab;
    [SerializeField] GameObject _baseEnemyPrefab;
    [SerializeField] GameObject _basePlayerPrefab;
    [SerializeField] GameObject _enemyTankPrefab;
    [SerializeField] GameObject _playerTankPrefab;
    [SerializeField] GameObject _allyTankPrefab;
    [SerializeField] GameObject _obstacleHorizontalPrefab;
    [SerializeField] GameObject _obstacleVerticalPrefab;

    int _enemiesLives = 30;
    int _alliesLives = 30;
    int _totalPoints = 0;

    #endregion
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (SceneManager.GetActiveScene().buildIndex == 0)
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        else
            Debug.LogError("Set active scene with index 0!");
    }
    public void StartGame()
    {
        _enemiesLives = 30;
        _alliesLives = 30;
        float groundPosX = -40;
        float groundPosY = 40;
        for (int i = 0; i <= 40; i++)
        {
            for (int j = 0; j <= 40; j++)
            {
                GameObject ground = Instantiate(_groundPrefab, transform.position, _groundPrefab.transform.rotation);
                ground.transform.position = new Vector3(groundPosX, groundPosY);
                if (i == 0)
                {
                    if (j == 21 || j == 19)
                    {
                        Vector3 spawnPos = new Vector3(groundPosX, groundPosY);
                        SpawnEnemy(spawnPos);
                    }
                    if (j == 20)
                    {
                        GameObject baseEnemy = Instantiate(_baseEnemyPrefab, transform.position, _baseEnemyPrefab.transform.rotation);
                        baseEnemy.transform.position = new Vector3(groundPosX, groundPosY);
                    }
                }
                else if (i == 40)
                {
                    if (j == 18)
                    {
                        GameObject Player = Instantiate(_playerTankPrefab, transform.position, _playerTankPrefab.transform.rotation);
                        Player.transform.position = new Vector3(groundPosX, groundPosY);
                        Player.GetComponent<HealthObject>().Died += PlayerDead;
                        Player.GetComponentInChildren<Base>().RandomBase();
                        Player.GetComponentInChildren<Cannon>().RandomCannon();
                        Player.GetComponentInChildren<Tower>().RandomTower();
                        Player.GetComponent<PlayerMove>().speed = Player.GetComponentInChildren<Base>().Speed;
                        Player.GetComponent<HealthObject>().health = Player.GetComponentInChildren<Base>().Health;
                        Player.GetComponent<ProjectileLauncher>().damage = Player.GetComponentInChildren<Cannon>().Damage;
                        Player.GetComponent<ProjectileLauncher>().distance = Player.GetComponentInChildren<Cannon>().Distance;
                        Player.GetComponent<ProjectileLauncher>().barrel = Player.GetComponentInChildren<Cannon>().Barrel;
                        Player.GetComponent<ProjectileLauncher>().accuracy = Player.GetComponentInChildren<Tower>().Accuracy;
                    }
                    if (j == 19)
                    {
                        GameObject basePlayer = Instantiate(_basePlayerPrefab, transform.position, _basePlayerPrefab.transform.rotation);
                        basePlayer.transform.position = new Vector3(groundPosX, groundPosY);
                    }
                    if (j == 20)
                    {
                        Vector3 spawnPos = new Vector3(groundPosX, groundPosY);
                        SpawnAlly(spawnPos);
                    }
                }
                else if (Random.Range(0.0f, 1.0f) <= 0.25)
                {
                    GameObject obstacleHorizontal = Instantiate(_obstacleHorizontalPrefab, transform.position, _obstacleHorizontalPrefab.transform.rotation);
                    obstacleHorizontal.transform.position = new Vector3(groundPosX, groundPosY);
                }
                else if ((Random.Range(0.0f, 1.0f) > 0.25) && (Random.Range(0.0f, 1.0f) <= 0.5))
                {
                    GameObject obstacleVertical = Instantiate(_obstacleVerticalPrefab, transform.position, _obstacleVerticalPrefab.transform.rotation);
                    obstacleVertical.transform.position = new Vector3(groundPosX, groundPosY);
                }
                groundPosX += 2f;
            }
            groundPosY -= 2f;
            groundPosX = -40;
        }
    }

    #region Spawn enemy
    void SpawnEnemy() // для респавна
    {
        int randonPos = Random.Range(1, 3);
        Vector3 spawnPos = Vector3.zero;
        switch (randonPos)
        {
            case 1:
                spawnPos = new Vector3(-2, 40);
                break;
            case 2:
                spawnPos = new Vector3(2, 40);
                break;
            default:
                break;
        }
        GameObject Enemy = Instantiate(_enemyTankPrefab, spawnPos, _enemyTankPrefab.transform.rotation);
        Enemy.GetComponent<HealthObject>().Died += EnemyDead;
        Enemy.GetComponentInChildren<Base>().RandomBase();
        Enemy.GetComponentInChildren<Cannon>().RandomCannon();
        Enemy.GetComponentInChildren<Tower>().RandomTower();
        Enemy.GetComponent<HealthObject>().health = Enemy.GetComponentInChildren<Base>().Health;
        Enemy.GetComponent<BotControllerFactory>().speed = Enemy.GetComponentInChildren<Base>().Speed;
        Enemy.GetComponent<ProjectileLauncher>().damage = Enemy.GetComponentInChildren<Cannon>().Damage;
        Enemy.GetComponent<ProjectileLauncher>().distance = Enemy.GetComponentInChildren<Cannon>().Distance;
        Enemy.GetComponent<ProjectileLauncher>().barrel = Enemy.GetComponentInChildren<Cannon>().Barrel;
        Enemy.GetComponent<ProjectileLauncher>().accuracy = Enemy.GetComponentInChildren<Tower>().Accuracy;
    }
    void SpawnEnemy(Vector3 pos) // для старт-спавна
    {
        GameObject Enemy = Instantiate(_enemyTankPrefab, pos, _enemyTankPrefab.transform.rotation);
        Enemy.GetComponent<HealthObject>().Died += EnemyDead;
        Enemy.GetComponentInChildren<Base>().RandomBase();
        Enemy.GetComponentInChildren<Cannon>().RandomCannon();
        Enemy.GetComponentInChildren<Tower>().RandomTower();
        Enemy.GetComponent<HealthObject>().health = Enemy.GetComponentInChildren<Base>().Health;
        Enemy.GetComponent<BotControllerFactory>().speed = Enemy.GetComponentInChildren<Base>().Speed;
        Enemy.GetComponent<ProjectileLauncher>().damage = Enemy.GetComponentInChildren<Cannon>().Damage;
        Enemy.GetComponent<ProjectileLauncher>().distance = Enemy.GetComponentInChildren<Cannon>().Distance;
        Enemy.GetComponent<ProjectileLauncher>().barrel = Enemy.GetComponentInChildren<Cannon>().Barrel;
        Enemy.GetComponent<ProjectileLauncher>().accuracy = Enemy.GetComponentInChildren<Tower>().Accuracy;
    }
    IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        SpawnEnemy();
        yield break;
    }

    #endregion

    #region Spawn ally
    void SpawnAlly() // для респавна
    {
        int randonPos = Random.Range(1, 3);
        Vector3 spawnPos = Vector3.zero;
        switch (randonPos)
        {
            case 1:
                spawnPos = new Vector3(-4, -40);
                break;
            case 2:
                spawnPos = new Vector3(0, -40);
                break;
            default:
                break;
        }
        GameObject Ally = Instantiate(_allyTankPrefab, spawnPos, _allyTankPrefab.transform.rotation);
        Ally.GetComponent<HealthObject>().Died += AllyDead;
        Ally.GetComponentInChildren<Base>().RandomBase();
        Ally.GetComponentInChildren<Cannon>().RandomCannon();
        Ally.GetComponentInChildren<Tower>().RandomTower();
        Ally.GetComponent<HealthObject>().health = Ally.GetComponentInChildren<Base>().Health;
        Ally.GetComponent<BotControllerFactory>().speed = Ally.GetComponentInChildren<Base>().Speed;
        Ally.GetComponent<ProjectileLauncher>().damage = Ally.GetComponentInChildren<Cannon>().Damage;
        Ally.GetComponent<ProjectileLauncher>().distance = Ally.GetComponentInChildren<Cannon>().Distance;
        Ally.GetComponent<ProjectileLauncher>().barrel = Ally.GetComponentInChildren<Cannon>().Barrel;
        Ally.GetComponent<ProjectileLauncher>().accuracy = Ally.GetComponentInChildren<Tower>().Accuracy;
    }
    void SpawnAlly(Vector3 pos) // для старт-спавна
    {
        GameObject Ally = Instantiate(_allyTankPrefab, pos, _allyTankPrefab.transform.rotation);
        Ally.GetComponent<HealthObject>().Died += AllyDead;
        Ally.GetComponentInChildren<Base>().RandomBase();
        Ally.GetComponentInChildren<Cannon>().RandomCannon();
        Ally.GetComponentInChildren<Tower>().RandomTower();
        Ally.GetComponent<HealthObject>().health = Ally.GetComponentInChildren<Base>().Health;
        Ally.GetComponent<BotControllerFactory>().speed = Ally.GetComponentInChildren<Base>().Speed;
        Ally.GetComponent<ProjectileLauncher>().damage = Ally.GetComponentInChildren<Cannon>().Damage;
        Ally.GetComponent<ProjectileLauncher>().distance = Ally.GetComponentInChildren<Cannon>().Distance;
        Ally.GetComponent<ProjectileLauncher>().barrel = Ally.GetComponentInChildren<Cannon>().Barrel;
        Ally.GetComponent<ProjectileLauncher>().accuracy = Ally.GetComponentInChildren<Tower>().Accuracy;
    }
    IEnumerator SpawnAllyCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        SpawnAlly();
        yield break;
    }

    #endregion

    #region Deaths
    void PlayerDead()
    {
        _alliesLives -= 30;
        if (_alliesLives <= 0)
            GameOver();
    }
    void AllyDead()
    {
        _alliesLives--;
        if (_alliesLives <= 0)
            GameOver();
        else
            StartCoroutine(SpawnAllyCoroutine());
    }
    void EnemyDead()
    {
        _enemiesLives--;
        _totalPoints += 50;
        HUDController.Instance.UpdateScore(_totalPoints);
        if (_enemiesLives <= 0)
            LevelComplete();
        else
            StartCoroutine(SpawnEnemyCoroutine());
    }

    #endregion

    #region Game states 
    public void GameOver()
    {
        HUDController.Instance.ShowStatus("Game Over!");
    }
    public void LevelComplete()
    {
        HUDController.Instance.ShowStatus("Level Complete!");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
