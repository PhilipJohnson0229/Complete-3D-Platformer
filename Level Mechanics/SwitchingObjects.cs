using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class SwitchingObjects : MonoBehaviour
    {
        public GameObject theObject;
        public LevelButton theButton;

        public bool revealWhenPressed;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (theButton.isPressed)
            {
                theObject.SetActive(revealWhenPressed);
            }
            else
            {
                theObject.SetActive(!revealWhenPressed);
            }
        }
    }

}
