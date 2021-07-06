using System;
using UnityEngine;

public class AssistantBot : BotController
{
    [SerializeField] GameObject nearest;
    public event Action<bool> AssistantBotFire = delegate { };
    void Awake() { } // избегаю случайного переопределения родительских методов
    protected override void Update()
    {
        base.Update();
    }
    protected override void Behavior()
    {
        base.Behavior();
        if (moveTarget == null)
            FindTarget();
        MoveToTarget(moveTarget);
        if (hitForward.collider != null)
        {
            if ((hitForward.transform.gameObject.CompareTag("Obstacle") || hitForward.transform.gameObject == shootTarget) && canShoot)
            {
                AssistantBotFire?.Invoke(!isEnemy);
                StartCoroutine(ShootCoroutine());
            }
        }
        if (moveTarget.GetComponent<HealthObject>().isHit)
        {
            FindNearestEnemy();
        }
    }
    void FindTarget()
    {
        if (isEnemy)
            moveTarget = GameObject.FindGameObjectWithTag("BlueTank");
        else
            moveTarget = GameObject.FindGameObjectWithTag("RedTank");
    }
    void FindNearestEnemy() //получить урон союзный танк мог только от ближайшего врага
    {
        GameObject[] enemies;
        if (isEnemy)
        {
            enemies = GameObject.FindGameObjectsWithTag("RedTank");
        }
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("BlueTank");
        }
        float distance = Mathf.Infinity;
        foreach (GameObject go in enemies)
        {
            Vector3 diff = go.transform.position - transform.position;
            float currentDistance = diff.sqrMagnitude;
            if (currentDistance < distance)
            {
                nearest = go;
                distance = currentDistance;
            }
        }
        shootTarget = nearest;
    }
}
