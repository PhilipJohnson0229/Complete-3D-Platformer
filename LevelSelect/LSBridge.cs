using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class LSBridge : MonoBehaviour
    {
        public string levelToUnlock;
        // Start is called before the first frame update
        void Start()
        {
            if (PlayerPrefs.GetInt(levelToUnlock + "_Unlocked!") == 0)
            {
                gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
