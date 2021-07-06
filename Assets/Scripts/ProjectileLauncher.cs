using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    #region  Field Declarations

    [SerializeField] ProjectileMove _projectilePrefab;
    public float damage;
    public float distance;
    public int barrel;
    public float accuracy;
    [SerializeField] Transform _weaponMountPoint;

    #endregion 
    void Start()
    {
        if (GetComponent<PlayerInput>())
            GetComponent<PlayerInput>().OnFire += HandleFire;
        if (GetComponent<HunterBot>())
            GetComponent<HunterBot>().HunterBotFire += HandleFire;
        if (GetComponent<DestroyerBot>())
            GetComponent<DestroyerBot>().DestroyerBotFire += HandleFire;
        if (GetComponent<StormtrooperBot>())
            GetComponent<StormtrooperBot>().StormtrooperBotFire += HandleFire;
        if (GetComponent<AssistantBot>())
            GetComponent<AssistantBot>().AssistantBotFire += HandleFire;
    }
    void HandleFire(bool isRed)
    {
        if (barrel == 1)
        {
            Vector3 spawnPosition = _weaponMountPoint.position;
            ProjectileMove projectile = Instantiate(_projectilePrefab, spawnPosition, _projectilePrefab.transform.rotation);
            if (isRed)
            {
                projectile.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                projectile.isPlayers = true;
            }
            else
            {
                projectile.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                projectile.isPlayers = false;
            }
            projectile.projectileSpeed = 10f;
            if (UnityEngine.Random.Range(0f, 1f) >= accuracy)
            {
                projectile.damage = 0;
            }
            else
            {
                projectile.damage = damage;
            }
            projectile.damage = damage;
            projectile.projectileDistance = distance;
            projectile.startTransform = _weaponMountPoint;
        }
        else
        {
            Vector3 spawnPosition1 = Vector3.zero;
            Vector3 spawnPosition2 = Vector3.zero;
            Transform spawnTransform1 = _weaponMountPoint;
            Transform spawnTransform2 = _weaponMountPoint;
            if (transform.eulerAngles.z == 180 || transform.eulerAngles.z == 0)
            {
                spawnPosition1 = new Vector3(_weaponMountPoint.position.x, _weaponMountPoint.position.y - 0.3f);
                spawnPosition2 = new Vector3(_weaponMountPoint.position.x, _weaponMountPoint.position.y + 0.3f);
                spawnTransform1.position = new Vector3(_weaponMountPoint.position.x, _weaponMountPoint.position.y - 0.3f);
                spawnTransform2.position = new Vector3(_weaponMountPoint.position.x, _weaponMountPoint.position.y + 0.3f);
            }
            else
            {
                spawnPosition1 = new Vector3(_weaponMountPoint.position.x - 0.3f, _weaponMountPoint.position.y);
                spawnPosition2 = new Vector3(_weaponMountPoint.position.x + 0.3f, _weaponMountPoint.position.y);
                spawnTransform1.position = new Vector3(_weaponMountPoint.position.x - 0.3f, _weaponMountPoint.position.y);
                spawnTransform2.position = new Vector3(_weaponMountPoint.position.x + 0.3f, _weaponMountPoint.position.y);
            }
            ProjectileMove projectile1 = Instantiate(_projectilePrefab, spawnPosition1, _projectilePrefab.transform.rotation);
            ProjectileMove projectile2 = Instantiate(_projectilePrefab, spawnPosition2, _projectilePrefab.transform.rotation);
            if (isRed)
            {
                projectile1.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                projectile2.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                projectile1.isPlayers = true;
                projectile2.isPlayers = true;
            }
            else
            {
                projectile1.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                projectile2.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                projectile1.isPlayers = false;
                projectile2.isPlayers = false;
            }
            projectile1.projectileSpeed = 10f;
            projectile2.projectileSpeed = 10f;
            projectile1.projectileDistance = distance;
            projectile2.projectileDistance = distance;
            projectile1.startTransform = spawnTransform1;
            projectile2.startTransform = spawnTransform2;
            if (UnityEngine.Random.Range(0f, 1f) >= accuracy)
            {
                projectile1.damage = 0;
            }
            else
            {
                projectile1.damage = damage;
            }
            if (UnityEngine.Random.Range(0f, 1f) >= accuracy)
            {
                projectile2.damage = 0;
            }
            else
            {
                projectile2.damage = damage;
            }
        }
    }
}
