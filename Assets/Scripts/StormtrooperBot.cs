using System;
using UnityEngine;

public class StormtrooperBot : BotController
{
    public event Action<bool> StormtrooperBotFire = delegate { };
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
        MoveToTarget(moveTarget);
        if (hitForward.collider != null)
        {
            if ((hitForward.transform.gameObject.CompareTag("Obstacle") || hitForward.transform.gameObject == shootTarget) && canShoot)
            {
                StormtrooperBotFire?.Invoke(!isEnemy);
                StartCoroutine(ShootCoroutine());
            }
        }
    }
    void FindTarget()
    {
        if (isEnemy)
        {
            moveTarget = GameObject.FindGameObjectWithTag("RedTank");
            shootTarget = GameObject.FindGameObjectWithTag("RedTank");
        }
        else
        {
            moveTarget = GameObject.FindGameObjectWithTag("BlueTank");
            shootTarget = GameObject.FindGameObjectWithTag("BlueTank");
        }
    }
}
