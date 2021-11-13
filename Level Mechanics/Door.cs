using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class Door : MonoBehaviour
    {
        //public bool shouldOpen;
        public Transform theDoor, openRot;
        public float openSpeed;

        private Quaternion startRot;

        public LevelButton button;
        // Start is called before the first frame update
        void Start()
        {
            startRot = transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (button.isPressed)
            {
                theDoor.rotation = Quaternion.Slerp(theDoor.rotation, openRot.rotation, openSpeed * Time.deltaTime);
            }
            else
            {
                theDoor.rotation = Quaternion.Slerp(theDoor.rotation, startRot, openSpeed * Time.deltaTime);
            }
        }

       
    }

}
