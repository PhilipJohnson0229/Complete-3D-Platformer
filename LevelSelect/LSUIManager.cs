using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DillonsDream
{
    public class LSUIManager : MonoBehaviour
    {
        public static LSUIManager instance;

        public Text lNameText;
        public GameObject lNamePanel;

        public Text coinText;
        // Start is called before the first frame update
        private void Awake()
        {
            instance = this;
        }
    }

}


