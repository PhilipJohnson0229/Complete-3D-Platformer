using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DillonsDream
{
    public class LSLevelEntry : MonoBehaviour
    {
        public string levelName, levelToCheck, displayName;
        private bool canLoadLevel, levelUnlocked, levelLoading;

        public GameObject mapPointActive, mapPointInactive;
        void Awake()
        {
           
        }

        void Start()
        {
            if (PlayerPrefs.GetInt(levelToCheck + "_Unlocked!") == 1 || levelToCheck == "")
            {
                mapPointActive.SetActive(true);
                mapPointInactive.SetActive(false);
                levelUnlocked = true;
            }
            else
            {
                mapPointActive.SetActive(false);
                mapPointInactive.SetActive(true);
                levelUnlocked = false;
            }
            //Debug.Log(PlayerPrefs.GetInt(levelName + "_coins"));
            if (PlayerPrefs.GetString("CurrentLevel") == levelName)
            {
                PlayerController.instance.transform.position = transform.position;
                LSResetPosition.instance.respawnPosition = transform.position;
            }
        }

        void Update()
        {
            if (canLoadLevel)
            {
                if (Input.GetButtonDown("Jump") && levelUnlocked && !levelLoading)
                {
                    StartCoroutine(LevelLoadCo(levelName));
                    levelLoading = true;

                    
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                canLoadLevel = true;

                LSUIManager.instance.lNamePanel.SetActive(true);
                LSUIManager.instance.lNameText.text = displayName;

                if (PlayerPrefs.HasKey(levelName + "_coins"))
                {
                    LSUIManager.instance.coinText.text = PlayerPrefs.GetInt(levelName + "_coins").ToString();
                }
                else
                {
                    LSUIManager.instance.coinText.text = "???";
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                canLoadLevel = false;
                LSUIManager.instance.lNamePanel.SetActive(false);
            }
        }

        public IEnumerator LevelLoadCo(string levelToLoad)
        {

            PlayerController.instance.stopMove = true;

            UIManager.instance.fadeToBlack = true;

            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene(levelToLoad);
            PlayerPrefs.SetString("CurrentLevel", levelName);
        }
    }

}
