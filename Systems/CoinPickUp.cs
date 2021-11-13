using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class CoinPickUp : MonoBehaviour
    {
        public int value;
        public GameObject pickupEffect;
        public int soundToPlay;
        // Start is called before the first frame update
    

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GameManager.instance.AddCoins(value);
                Destroy(gameObject);
                Instantiate(pickupEffect, gameObject.transform.position, gameObject.transform.rotation);
                AudioManager.instace.PlaySoundEffects(soundToPlay);
            }
        }
    }

}
