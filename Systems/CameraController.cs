using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace DillonsDream
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController instance;

        public CinemachineBrain theCMBrain;
        void Awake()
        {
            instance = this;
        }

    }

}
