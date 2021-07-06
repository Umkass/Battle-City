using System;
using UnityEngine;

public class HunterBot : BotController
{
    public event Action<bool> HunterBotFire = delegate { };
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
            if (((hitForward.transform.gameObject.CompareTag("Obstacle") && hitForward.transform.gameObject != moveTarget)
                || hitForward.transform.gameObject == shootTarget) && canShoot)
            {
                HunterBotFire?.Invoke(!isEnemy);
                StartCoroutine(ShootCoroutine());
            }
        }
        if (hitForward.collider != null)
        {
            if (isEnemy && hitForward.collider.CompareTag("RedTank"))
            {
                MoveToTarget(hitForward.collider.gameObject);
            }
            if (!isEnemy && hitForward.collider.CompareTag("BlueTank"))
            {
                MoveToTarget(hitForward.collider.gameObject);
            }
        }
        if (hitBack.collider != null)
        {
            if (isEnemy && hitBack.collider.CompareTag("RedTank"))
            {
                MoveToTarget(hitBack.collider.gameObject);
            }
            if (!isEnemy && hitBack.collider.CompareTag("BlueTank"))
            {
                MoveToTarget(hitBack.collider.gameObject);
            }
        }
        if (hitLeft.collider != null)
        {
            if (isEnemy && hitLeft.collider.CompareTag("RedTank"))
            {
                MoveToTarget(hitLeft.collider.gameObject);
            }
            if (!isEnemy && hitLeft.collider.CompareTag("BlueTank"))
            {
                MoveToTarget(hitLeft.collider.gameObject);
            }
        }
        if (hitRight.collider != null)
        {
            if (isEnemy && hitRight.collider.CompareTag("RedTank"))
            {
                MoveToTarget(hitRight.collider.gameObject);
            }
            if (!isEnemy && hitRight.collider.CompareTag("BlueTank"))
            {
                MoveToTarget(hitRight.collider.gameObject);
            }
        }
    }
    void FindTarget()
    {
        FindRandomHide();
        if (isEnemy)
            shootTarget = GameObject.FindGameObjectWithTag("RedTank");
        else
            shootTarget = GameObject.FindGameObjectWithTag("BlueTank");
    }
    void FindRandomHide() // для хантера
    {
        GameObject[] hides = GameObject.FindGameObjectsWithTag("Obstacle");
        moveTarget = hides[UnityEngine.Random.Range(0, hides.Length)];
    }
}
