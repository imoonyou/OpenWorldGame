using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Monster : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    
    //check
    public LayerMask whatIsGround, Player;

    public float timeBetweenAttack;
    bool alreadyAttacked;
    bool performAttackAnimation = false;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    
    public Animator animator;

    DamageCaculate dmgEnable;

    [SerializeField] private Actor actor;

    


    private void Awake()
    {
        player = GameObject.Find("FPC_Melee").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); 
        dmgEnable = GetComponentInChildren<DamageCaculate>();
        
    }


    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (playerInSightRange && !playerInAttackRange && performAttackAnimation == false || !playerInSightRange && !playerInAttackRange && performAttackAnimation==false) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

  
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attackcode
            animator.SetTrigger("Attack");
            alreadyAttacked = true;
            performAttackAnimation = true;
            Invoke(nameof(AttackReset), timeBetweenAttack);
        }
    }

    public void AttackReset()
    {
        alreadyAttacked = false;
        performAttackAnimation = false;
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void EnableDamage()
    {
        dmgEnable.attackCollider.enabled = true;
    }

    public void DisableDamage()
    {
        dmgEnable.attackCollider.enabled = false;
    }

    //hit player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.TryGetComponent<Actor>(out Actor T))
            {
                Vector3 knockbackDirection = (actor.transform.position - transform.position).normalized;
                actor.ApplyKnockback(knockbackDirection);
                T.TakeDamage(2);
                Debug.Log("Trigger");
            }
        } 
    }

    public void SetActorReference(Actor actor)
    {
        this.actor = actor;
    }
}
