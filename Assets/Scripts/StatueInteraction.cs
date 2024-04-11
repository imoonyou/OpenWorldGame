using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueInteraction : MonoBehaviour
{

    public int statueNumber; // Unique identifier for each statue
    public GameManager gameManager; // Reference to the GameManager script

    public bool canInteract = true; // Flag to check if interaction is allowed

    public GameObject monsterPrefab; // Prefab for the monster to summon

    private GameObject summonedMonster;

    //private BoxCollider colliderInteract;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canInteract)
        {
            canInteract = false;

           
            SummonMonster();
            //gameManager.HandleStatueInteraction(statueNumber);
        }
    }

    // Method to reset interaction for this statue
    public void ResetInteraction()
    {
        canInteract = true;
    }

    private void SummonMonster()
    {
        summonedMonster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        Monster monsterComponent = summonedMonster.GetComponent<Monster>();
        AnimationHandle monster = summonedMonster.GetComponent<AnimationHandle>();
        if (monster != null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            monster.OnDeath += HandleMonsterDeath;
            Actor actorComponent = playerObject.GetComponent<Actor>();
            if (actorComponent != null)
            {
                monsterComponent.SetActorReference(actorComponent);
            }
            else
            {
                Debug.LogError("Actor component not found on player object!");
            }
        }

    }


    private void HandleMonsterDeath()
    {
        gameManager.HandleStatueInteraction(statueNumber);
    }

}
