using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class LSResetPosition : MonoBehaviour
    {
        public static LSResetPosition instance;
        public Vector3 respawnPosition;
        public string desiredLevel;
        void Awake()
        {
            instance = this;
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                PlayerController.instance.gameObject.SetActive(false);
                PlayerController.instance.transform.position = respawnPosition;
                PlayerController.instance.gameObject.SetActive(true);
            }
        }
    }

}
