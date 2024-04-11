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
    public bool performTakeDamageAnimation = false;


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

        if (!playerInAttackRange && performAttackAnimation == false &&performTakeDamageAnimation==false) ChasePlayer();
        else if (playerInSightRange && playerInAttackRange && performTakeDamageAnimation==false) AttackPlayer();

       
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

    public void DamageAnimation()
    {
        //if player get hit in attack animation
        if (performAttackAnimation == true)
        {
            performAttackAnimation = false;
        }
        // Trigger the "Damage" animation
        animator.SetTrigger("Damage");
        performTakeDamageAnimation = true;
        // Start a coroutine to wait for the animation to finish
        //StartCoroutine(WaitForAnimation());
        Invoke(nameof(TakeDamageFinish), 0.3f);
    }

    public void TakeDamageFinish()
    {
        performTakeDamageAnimation = false;
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
            if (other.TryGetComponent<Actor>(out Actor T))
            {
                Vector3 knockbackDirection = (actor.transform.position - transform.position).normalized;
                actor.ApplyKnockback(knockbackDirection);
                T.TakeDamage(2);
            }
        }
    }

    public void SetActorReference(Actor actor)
    {
        this.actor = actor;
    }
}
