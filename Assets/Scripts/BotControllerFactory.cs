using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotControllerFactory : MonoBehaviour
{
    public bool isEnemy;
    public float speed;
    BotController botController;

    void Awake()
    {
        RandomBot();
    }

    void Start()
    {
        botController.speed = speed;
        botController.isEnemy = isEnemy;
    }

    public void RandomBot()
    {
        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0: // Hunter
                botController = gameObject.AddComponent<HunterBot>();
                break;
            case 1: // Destroyer
                botController = gameObject.AddComponent<DestroyerBot>();
                break;
            case 2: // Stormtrooper
                botController = gameObject.AddComponent<StormtrooperBot>();
                break;
            case 3: // Assistant
                botController = gameObject.AddComponent<AssistantBot>();
                break;
            default:
                botController = gameObject.AddComponent<StormtrooperBot>();
                break;
        }
    }
}
