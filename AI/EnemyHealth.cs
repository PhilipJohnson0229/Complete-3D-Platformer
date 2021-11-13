using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class EnemyHealth : MonoBehaviour
    {
        public int maxHealth;
        public int currentHealth;

        public int deathSound;
        public GameObject deathEffect;
        public GameObject itemToDrop;
        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage()
        {
            currentHealth--;
            PlayerController.instance.Bounce();
            if (currentHealth <= 0)
            {
                AudioManager.instace.PlaySoundEffects(deathSound);
                Instantiate(deathEffect, transform.position + new Vector3(0,1.2f,0), transform.rotation);
                Instantiate(itemToDrop, transform.position + new Vector3(0, .4f, 0), transform.rotation);
                Destroy(gameObject);
            }
        }
    }

}
