using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DillonsDream;

public class BossDamagePoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("trying to huert the boss");
        if (other.tag == "Player")
        {
           BossController.instance.DamageBoss();
        }

    }
}
