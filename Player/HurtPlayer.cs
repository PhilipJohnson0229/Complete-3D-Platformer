using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class HurtPlayer : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                HealthManager.instance.Hurt();
            }
        }
    }

}
