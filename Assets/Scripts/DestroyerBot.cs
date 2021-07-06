using System;
using UnityEngine;

public class DestroyerBot : BotController
{
    public event Action<bool> DestroyerBotFire = delegate { };
    void Awake() { } // избегаю случайного переопределения родительских методов
    protected override void Update()
    {
        base.Update();
    }
    protected override void Behavior()
    {
        base.Behavior();
        if (shootTarget == null || moveTarget == null)
            FindTarget();
        if (moveTarget != null)
            MoveToTarget(moveTarget);
        if (hitForward.collider != null)
        {
            if ((hitForward.transform.gameObject.CompareTag("Obstacle") || hitForward.transform.gameObject == shootTarget) && canShoot)
            {
                DestroyerBotFire?.Invoke(!isEnemy);
                StartCoroutine(ShootCoroutine());
            }
        }
    }
    void FindTarget()
    {
        if (isEnemy)
        {
            moveTarget = GameObject.FindGameObjectWithTag("BasePlayer");
            shootTarget = GameObject.FindGameObjectWithTag("BasePlayer");
        }
        else
        {
            moveTarget = GameObject.FindGameObjectWithTag("BaseEnemy");
            shootTarget = GameObject.FindGameObjectWithTag("BaseEnemy");
        }
    }
}
