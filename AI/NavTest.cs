using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DillonsDream
{
    public class NavTest : MonoBehaviour
    {
        public Transform target;
        public NavMeshAgent agent;
        void Start()
        {
            agent.SetDestination(target.position);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
