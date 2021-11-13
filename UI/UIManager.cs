using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace DillonsDream
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        public Image blackScreen;
        public float fadeSpeed = 2f;
        public bool fadeToBlack, fadeFromBlack;

        public Text healthText;
        public Image healthImage;

        public Text coinText;

        public GameObject pausedScreen, optionsScreen;

        public Slider musicVolSLider, sfxVolSlider;

        //ledgeGrab
        public Text wallCheckText;
        public Text LedgeCheckText;

        public string levelSelect, mainMenu;

        public GameObject backButton;

        public GameObject resumeButton;
        public GameObject mainMenuButton;
        public GameObject levelSelectButton;
        public GameObject optionsButton;
        public GameObject pausedText;


        void Awake()
        {
            //handle this when ready to build final iteration
            /*
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }*/

            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            if (fadeToBlack)
            {
                blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
                if (blackScreen.color.a == 1f)
                {
                    fadeToBlack = false;
                }
            }

            if (fadeFromBlack)
            {
                blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
                if (blackScreen.color.a == 0f)
                {
                    fadeFromBlack = false;
                }
            }
        }

        public void Resume()
        {
            GameManager.instance.PauseAndUnpause();
        }

        public void OpenOptions()
        {
            optionsScreen.SetActive(true);
            EventSystem currentSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            currentSystem.SetSelectedGameObject(null);
            currentSystem.SetSelectedGameObject(backButton);
            resumeButton.SetActive(false);
            mainMenuButton.SetActive(false);
            levelSelectButton.SetActive(false);
            optionsButton.SetActive(false);
        }

        public void CloseOptions()
        {
            optionsScreen.SetActive(false);
            EventSystem currentSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            resumeButton.SetActive(true);
            mainMenuButton.SetActive(true);
            levelSelectButton.SetActive(true);
            optionsButton.SetActive(true);
            currentSystem.SetSelectedGameObject(null);
            currentSystem.SetSelectedGameObject(resumeButton);
        }

        public void LevelSelect()
        {
            StartCoroutine(GameManager.instance.MenuLoadCO(levelSelect));
            Time.timeScale = 1;
        }

        public void MainMenu()
        {
            StartCoroutine(GameManager.instance.MenuLoadCO(mainMenu));
            Time.timeScale = 1;
        }

        public void SetMusicLevel()
        {
            AudioManager.instace.SetMusicLevel();
        }

        public void SetSFXLevel()
        {
            AudioManager.instace.SetSFXLevel();
        }

    }

}
