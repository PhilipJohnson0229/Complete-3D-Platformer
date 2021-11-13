using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class HurtEnemy : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<EnemyHealth>().TakeDamage();
                if (other.GetComponent<EnemyController>())
                {
                    other.GetComponent<EnemyController>().KnockBack();
                }           
            }
        }
    }

}
