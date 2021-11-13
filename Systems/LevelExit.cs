using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class LevelExit : MonoBehaviour
    {
        public Animator anim;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                anim.SetTrigger("Hit");
                StartCoroutine(GameManager.instance.LevelEndCO());
            }
        }
    }

}
