using System.Collections;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField]public bool isEnemy;
    public float speed;
    protected GameObject shootTarget;
    protected GameObject moveTarget;
    protected bool canShoot = true; //cooldown timer
    float Horizontal;
    float Vertical;
    protected RaycastHit2D hitForward;
    protected RaycastHit2D hitBack;
    protected RaycastHit2D hitLeft;
    protected RaycastHit2D hitRight;
    protected float detectionDistance = 16;
    protected int IgnoreCurrentLayer;

    void Start()
    {
        IgnoreCurrentLayer = ~(1 << gameObject.layer);
        Physics2D.queriesHitTriggers = false;
    }
    protected virtual void Update()
    {
        Behavior();
    }

    protected virtual void Behavior()
    {
        if(isEnemy)
        {
            hitForward = Physics2D.Raycast(transform.position, -Vector2.up * detectionDistance, Mathf.Infinity, IgnoreCurrentLayer);
            hitBack = Physics2D.Raycast(transform.position, Vector2.up * detectionDistance, Mathf.Infinity, IgnoreCurrentLayer);
            hitLeft = Physics2D.Raycast(transform.position, -Vector2.right * detectionDistance, Mathf.Infinity, IgnoreCurrentLayer);
            hitRight = Physics2D.Raycast(transform.position, Vector2.right * detectionDistance, Mathf.Infinity, IgnoreCurrentLayer);
        }
        else
        {
            hitForward = Physics2D.Raycast(transform.position, Vector2.up * detectionDistance, Mathf.Infinity, IgnoreCurrentLayer);
            hitBack = Physics2D.Raycast(transform.position, -Vector2.up * detectionDistance, Mathf.Infinity, IgnoreCurrentLayer);
            hitLeft = Physics2D.Raycast(transform.position, Vector2.right * detectionDistance, Mathf.Infinity, IgnoreCurrentLayer);
            hitRight = Physics2D.Raycast(transform.position, -Vector2.right * detectionDistance, Mathf.Infinity, IgnoreCurrentLayer);
        }
    }
    protected IEnumerator ShootCoroutine()
    {
        canShoot = false;
        yield return new WaitForSeconds(3f);
        canShoot = true;
        yield break;
    }
    protected void MoveToTarget(GameObject target)
    {
        if (target != null)
        {
            if (Mathf.Abs(transform.position.y - target.transform.position.y) > 1.5f)
            {
                if (transform.position.y > target.transform.position.y)
                {
                    if (Vertical > -1)
                        Vertical -= Time.fixedDeltaTime * 0.5f;
                    else if (Vertical <= -1)
                    {
                        Vertical = -1;
                    }
                    transform.rotation = Quaternion.Euler(0, 0, 90f);
                    transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.fixedDeltaTime * Vertical);
                }
                else
                {
                    if (Vertical < 1)
                        Vertical += Time.fixedDeltaTime * 0.5f;
                    else if (Vertical >= 1)
                    {
                        Vertical = 1;
                    }
                    transform.rotation = Quaternion.Euler(0, 0, -90f);
                    transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.fixedDeltaTime * Vertical);
                }
            }
            else if (Mathf.Abs(transform.position.x - target.transform.position.x) > 1.5f)
            {
                if (transform.position.x > target.transform.position.x)
                {
                    if (Horizontal > -1)
                        Horizontal -= Time.deltaTime * 0.5f;
                    else if (Horizontal <= -1)
                    {
                        Horizontal = -1;
                    }
                    transform.rotation = Quaternion.Euler(0, 0, 0f);
                    transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * Horizontal, transform.position.y);
                }
                else
                {
                    if (Horizontal < 1)
                        Horizontal += Time.deltaTime * 0.5f;
                    else if (Horizontal >= 1)
                    {
                        Horizontal = 1;
                    }
                    transform.rotation = Quaternion.Euler(0, 0, 180f);
                    transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * Horizontal, transform.position.y);
                }
            }
        }
    }
}