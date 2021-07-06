using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleController : MonoBehaviour
{
    #region Field Declarations

    RaycastHit2D hit;
    int layerMaskDroppedModule;
    bool isBot;

    #endregion
    void Start()
    {
        layerMaskDroppedModule = (1 << LayerMask.NameToLayer("DroppedModule"));
        if (GetComponentInParent<PlayerInput>())
        {
            GetComponentInParent<PlayerInput>().OnPickUpModule += HandlePickUpModule;
            GetComponentInParent<PlayerInput>().OnDestroyModule += HandleDestroyModule;
            isBot = false;
        }
        if (GetComponentInParent<BotController>())
        {
            isBot = true;
        }
    }
    void Update()
    {
        if (isBot)
        {
            InteractModule();
        }
    }
    public void SpawnModule() //по смерти
    {
        int random = UnityEngine.Random.Range(0, 3);
        if (random == 0)
        {
            Base currentModule = GetComponentInChildren<Base>();
            Base newModuleBase = Instantiate(currentModule, transform.position, transform.rotation);
            currentModule.ChangeBase(newModuleBase, currentModule); //передаю значения заспавленому модулю
            newModuleBase.gameObject.layer = LayerMask.NameToLayer("DroppedModule");
        }
        else if (random == 1)
        {
            Cannon currentModule = GetComponentInChildren<Cannon>();
            Cannon newModuleCannon = Instantiate(currentModule, transform.position, transform.rotation);
            currentModule.ChangeCannon(newModuleCannon, currentModule); //передаю значения заспавленому модулю
            newModuleCannon.gameObject.layer = LayerMask.NameToLayer("DroppedModule");
        }
        else
        {
            Tower currentModule = GetComponentInChildren<Tower>();
            Tower newModuleTower = Instantiate(currentModule, transform.position, transform.rotation);
            currentModule.ChangeTower(newModuleTower, currentModule); //передаю значения заспавленому модулю
            newModuleTower.gameObject.layer = LayerMask.NameToLayer("DroppedModule");

        }
    }

    #region PickUp and Destroy module
    void InteractModule() //ДЛЯ БОТОВ если currentModuleLVL < newModuleLVL менять, если нет - уничтожать
    {
        Physics2D.queriesHitTriggers = true; //лучам EnemyController-а и AllyController-а не нужно попадать по триггерам,
                                             //которые внутри них, а чтобы взять/уничтожить модуль нужно
        hit = Physics2D.Raycast(transform.position, Vector3.forward, 3, layerMaskDroppedModule);
        if (hit.collider != null)
        {
            if (hit.transform.gameObject.GetComponent<Base>())
            {
                if (GetComponentInChildren<Base>().Level < hit.collider.gameObject.GetComponent<Base>().Level)
                    GetComponentInChildren<Base>().ChangeBase(GetComponentInChildren<Base>(),
                        hit.collider.gameObject.GetComponent<Base>()); // ChangeBase
            }
            else if (hit.transform.gameObject.GetComponent<Cannon>())
            {
                if (GetComponentInChildren<Cannon>().Level < hit.collider.gameObject.GetComponent<Cannon>().Level)
                    GetComponentInChildren<Cannon>().ChangeCannon(GetComponentInChildren<Cannon>(),
                    hit.collider.gameObject.GetComponent<Cannon>()); // ChangeCanon
            }
            else if (hit.transform.gameObject.GetComponent<Tower>())
            {
                if (GetComponentInChildren<Tower>().Level < hit.collider.gameObject.GetComponent<Tower>().Level)
                    GetComponentInChildren<Tower>().ChangeTower(GetComponentInChildren<Tower>(),
                    hit.collider.gameObject.GetComponent<Tower>()); // ChangeTower
            }
            Destroy(hit.collider.gameObject);
        }
        Physics2D.queriesHitTriggers = false; //после взятия/уничтожения включить обратно
    }
    void HandlePickUpModule()
    {
        Physics2D.queriesHitTriggers = true; //лучам EnemyController-а и AllyController-а не нужно попадать по триггерам,
                                             //которые внутри них, а чтобы взять модуль нужно
        hit = Physics2D.Raycast(transform.position, Vector3.forward * 3, 3, layerMaskDroppedModule);
        if (hit.collider != null)
        {
            if (hit.transform.gameObject.GetComponent<Base>())
            {
                GetComponentInChildren<Base>().ChangeBase(GetComponentInChildren<Base>(),
                    hit.collider.gameObject.GetComponent<Base>()); // ChangeBase
            }
            else if (hit.transform.gameObject.GetComponent<Cannon>())
            {
                GetComponentInChildren<Cannon>().ChangeCannon(GetComponentInChildren<Cannon>(),
                    hit.collider.gameObject.GetComponent<Cannon>()); // ChangeCanon
            }
            else if (hit.transform.gameObject.GetComponent<Tower>())
            {
                GetComponentInChildren<Tower>().ChangeTower(GetComponentInChildren<Tower>(),
                    hit.collider.gameObject.GetComponent<Tower>()); // ChangeTower
            }
            Destroy(hit.collider.gameObject);
        }
        Physics2D.queriesHitTriggers = false; //после взятия включить обратно
    }
    void HandleDestroyModule()
    {
        Physics2D.queriesHitTriggers = true; //лучам EnemyController-а и AllyController-а не нужно попадать по триггерам,
                                             //которые внутри них, а чтобы уничтожить модуль нужно
        hit = Physics2D.Raycast(transform.position, Vector3.forward, 3, layerMaskDroppedModule);
        if (hit.collider != null)
        {
            if (hit.transform.gameObject.GetComponent<Base>() || hit.transform.gameObject.GetComponent<Cannon>()
                || hit.transform.gameObject.GetComponent<Tower>())
            {
                Destroy(hit.collider.gameObject);
            }
        }
        Physics2D.queriesHitTriggers = false; //после уничтожения включить обратно
    }

    #endregion
}
