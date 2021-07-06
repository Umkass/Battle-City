using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    #region Field Declarations

    public float projectileSpeed;
    public float projectileDistance;
    public bool isPlayers;
    public float damage;
    public Transform startTransform;

    #endregion

    void Start()
    {
        ProjectileDirection();
    }
    void Update()
    {
        MoveProjectile();
    }
    void MoveProjectile()
    {
        if(isPlayers)
        transform.Translate(-transform.right * Time.deltaTime * projectileSpeed, Space.World);
        else
        {
            transform.Translate(-transform.up * Time.deltaTime * projectileSpeed, Space.World);
        }
        if (startTransform != null)
        {
            float currentDistanceX = Mathf.Abs(startTransform.position.x - transform.position.x);
            float currentDistanceY = Mathf.Abs(startTransform.position.y - transform.position.y);
            if ((projectileDistance * 2) <= currentDistanceX || (projectileDistance * 2) <= currentDistanceY)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void ProjectileDirection()
    {
        transform.rotation = Quaternion.Euler(startTransform.eulerAngles.x, startTransform.eulerAngles.y, startTransform.eulerAngles.z);
    }
}