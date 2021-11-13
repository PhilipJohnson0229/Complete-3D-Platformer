using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DillonsDream
{
    public class MainMenu : MonoBehaviour
    {
        public string firstLevel, levelSelect;

        public GameObject continueButton;

        public GameObject newGameButton;

        public Image blackScreen;
        public float fadeSpeed = 3f;
        public bool fadeToBlack, fadeFromBlack;

        public Button _newGameButton, _continueButton, _exitButton;

        public EventSystem myEventSystem;

        public string[] levelNames;
        // Start is called before the first frame update
        void Awake()
        {
            Time.timeScale = 1;
        }

        void Start()
        {
            StartCoroutine(MenuLoadCo());
            if (PlayerPrefs.HasKey("Continue"))
            {
                continueButton.SetActive(true);
                myEventSystem.SetSelectedGameObject(continueButton);
            }
            else
            {
                ResetProgress();
                myEventSystem.SetSelectedGameObject(newGameButton);
            }
        }

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

        public void NewGame()
        {
            StartCoroutine(LoadLevelCO(levelSelect));
            //PlayerPrefs.DeleteAll();
            
            PlayerPrefs.SetInt("Continue", 0);
            PlayerPrefs.SetString("CurrentLevel", firstLevel);

            ResetProgress();
        }

        public void Continue()
        {
            StartCoroutine(LoadLevelCO(levelSelect));
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public IEnumerator LoadLevelCO(string levelToLoad)
        {
            _newGameButton.interactable = false;
            _continueButton.interactable = false;
            _exitButton.interactable = false;

            fadeToBlack = true;

            yield return new WaitForSeconds(2f);

            Debug.Log("Trying to load the next level");

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Unlocked!", 1);
            SceneManager.LoadScene(levelToLoad);
        }

        public IEnumerator MenuLoadCo()
        {

            _newGameButton.interactable = false;
            _continueButton.interactable = false;
            _exitButton.interactable = false;

            fadeFromBlack = true;

            yield return new WaitForSeconds(.5f);

            _newGameButton.interactable = true;
            _continueButton.interactable = true;
            _exitButton.interactable = true;
        }

        public void ResetProgress()
        {
             for (int i = 0; i < levelNames.Length; i++)
             {
                 PlayerPrefs.SetInt(levelNames[i] + "_Unlocked!", 0);
             }

             PlayerPrefs.SetInt("_coins", 0);
           // PlayerPrefs.DeleteAll();
        }
    }

}
