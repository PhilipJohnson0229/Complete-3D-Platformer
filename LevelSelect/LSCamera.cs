using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class LSCamera : MonoBehaviour
    {
        public Transform target;
        private Vector3 offset;
        public float transitionSpeed;
        // Start is called before the first frame update
        void Awake()
        {
            offset = transform.position - target.position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
           // transform.position = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * transitionSpeed);
        }
    }

}
