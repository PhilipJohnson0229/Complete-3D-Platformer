using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class Checkpoint : MonoBehaviour
    {
        public GameObject cp_On, cp_Off;
        public int soundToPlay;
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GameManager.instance.SetSpawnPoint(transform.position);

                Checkpoint[] allCp = FindObjectsOfType<Checkpoint>();
                for (int i = 0; i < allCp.Length; i++)
                {
                    allCp[i].cp_Off.SetActive(true);
                    allCp[i].cp_On.SetActive(false);
                }

                cp_Off.SetActive(false);
                AudioManager.instace.PlaySoundEffects(soundToPlay);
                cp_On.SetActive(true);
                Debug.Log("bingo");
            }
        }
    }

}
