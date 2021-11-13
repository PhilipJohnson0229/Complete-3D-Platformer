using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class LevelButton : MonoBehaviour
    {
        public bool isPressed;
        public Transform button, buttonPressedPos;
        private Vector3 buttonUp;

        public bool isOnOff;
        // Start is called before the first frame update
        void Start()
        {
            buttonUp = button.position;
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (isOnOff)
                {
                    if (isPressed)
                    {
                        button.position = buttonUp;
                        isPressed = false;
                    }
                    else
                    {
                        button.position = buttonPressedPos.position;
                        isPressed = true;
                    }
                }
                else
                {
                    button.position = buttonPressedPos.position;
                    isPressed = true;
                }
            }
        }

        
    }
}
