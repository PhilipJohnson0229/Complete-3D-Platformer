using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class DestroyOverTime : MonoBehaviour
    {
        public float time;
        void Update()
        {
            Destroy(gameObject, time);
        }
    }

}
