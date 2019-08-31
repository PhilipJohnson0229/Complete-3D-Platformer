using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class HealthPickUp : MonoBehaviour
    {
        public int healAmount;
        public bool isFullHeal;
        public GameObject healthEffect;
        public int soundToPlay;
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Destroy(gameObject);
                Instantiate(healthEffect, gameObject.transform.position, gameObject.transform.rotation);
                AudioManager.instace.PlaySoundEffects(soundToPlay);
                if (isFullHeal)
                {
                    HealthManager.instance.ResetHealth();
                 
                }
                else
                {
                    HealthManager.instance.AddHealth(healAmount);
                }

            }
        }
    }

}
