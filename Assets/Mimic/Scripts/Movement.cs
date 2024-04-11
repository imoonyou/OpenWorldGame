using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MimicSpace
{
    /// <summary>
    /// This is a very basic movement script, if you want to replace it
    /// Just don't forget to update the Mimic's velocity vector with a Vector3(x, 0, z)
    /// </summary>
    public class Movement : MonoBehaviour
    {
        [Header("Controls")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;
        public float speed = 5f;
        Vector3 velocity = Vector3.zero;
        public float velocityLerpCoef = 4f;
        Mimic myMimic;
        private Transform player;
        public float attackRange = 10f;
        private bool readyToAttack = true;
        public GameObject bulletPrefab;
        public float bulletSpeed = 20f;
        public float fireRate = 1f;
       
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            myMimic = GetComponent<Mimic>();
        }

        void Update()
        {
            Vector3 direction = (player.position - transform.position).normalized;
            velocity = Vector3.Lerp(velocity, direction * speed, velocityLerpCoef * Time.deltaTime);
           
            // Check if the player is within attack range
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                // Stop movement if the player is within attack range
                velocity = Vector3.zero;

                // Attack the player if ready
                if (readyToAttack)
                {
                    Attack();
                }
            }
            else
            {
                // Move towards the player if not in attack range
                velocity = Vector3.Lerp(velocity, direction * speed, velocityLerpCoef * Time.deltaTime);
            }
            myMimic.velocity = velocity;

            // Update enemy position
            transform.position = transform.position + velocity * Time.deltaTime;

            RaycastHit hit;
            Vector3 destHeight = transform.position;
            if (Physics.Raycast(transform.position + Vector3.up * 5f, -Vector3.up, out hit))
                destHeight = new Vector3(transform.position.x, hit.point.y + height, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destHeight, velocityLerpCoef * Time.deltaTime);
        }
        private void Attack()
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            // Calculate bullet direction
            Vector3 bulletDirection = (player.position - transform.position).normalized;

            // Fire bullet
            bulletRigidbody.velocity = bulletDirection * bulletSpeed;

            // Set cooldown for next attack
            readyToAttack = false;
            Invoke("ResetAttack", fireRate);
        }


        private void ResetAttack()
        {
            readyToAttack = true;
        }
    }

}