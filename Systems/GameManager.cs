using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace DillonsDream
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private Vector3 respawnPos, camSpawnPos;

        public int currentCoins;

        public GameObject deathEffect;

        public int levelEndMusic = 8;

        public string levelToLoad;

        public GameObject resumeButton;

        public bool isRespawning;

        //public EventSystem currentSystem;
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
            //for some reason declaring this right away even though its already set will prevent the freezing before pausing bug
            Time.timeScale = 1;
        }
        void Start()
        {
            if (SceneManager.GetActiveScene().name != "LevelSelect")
            {
                respawnPos = PlayerController.instance.transform.position;
            }
            

            StartCoroutine(LevelLoadCo());
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;

            AddCoins(0);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseAndUnpause();
            }
        }
        public void Respawn()
        {
            StartCoroutine(RespawnCo());
            HealthManager.instance.PlayerKilled();
        }

       

        //coroutine is something that happens outside the normal order of unity
        public IEnumerator RespawnCo()
        {
            PlayerController.instance.gameObject.SetActive(false);

            CameraController.instance.theCMBrain.enabled = false;

            Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f,1f,0f), PlayerController.instance.transform.rotation);

            UIManager.instance.fadeToBlack = true;

            yield return new WaitForSeconds(2f);

            isRespawning = true;

            HealthManager.instance.ResetHealth();

            UIManager.instance.fadeFromBlack = true;

            PlayerController.instance.gameObject.transform.position = respawnPos;

            CameraController.instance.theCMBrain.enabled = true;

            PlayerController.instance.gameObject.SetActive(true);
        }

        public void SetSpawnPoint(Vector3 newSpawnPoint)
        {
            respawnPos = newSpawnPoint;
            Debug.Log("spawn point set");
        }

        public void AddCoins(int coinsToAdd)
        {
            currentCoins += coinsToAdd;
            UIManager.instance.coinText.text = "" + currentCoins;
        }

        public void PauseAndUnpause()
        {
            if (UIManager.instance.pausedScreen.activeInHierarchy)
            {
                UIManager.instance.pausedScreen.SetActive(false);
                Time.timeScale = 1f;
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;

            }
            else
            {
                UIManager.instance.pausedScreen.SetActive(true);
                EventSystem currentSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
                currentSystem.SetSelectedGameObject(null);
                currentSystem.SetSelectedGameObject(resumeButton);
                UIManager.instance.CloseOptions();
                Time.timeScale = 0f;
               // Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.none;
            }
        }

        public IEnumerator LevelLoadCo()
        {

            PlayerController.instance.stopMove = true;

            UIManager.instance.fadeFromBlack = true;

            yield return new WaitForSeconds(1f);

            PlayerController.instance.stopMove = false;
        }

        public IEnumerator LevelEndCO()
        {
            AudioManager.instace.PlayMusic(levelEndMusic);

            PlayerController.instance.stopMove = true;

            UIManager.instance.fadeToBlack = true;

            yield return new WaitForSeconds(2f);

            Debug.Log("Trying to load the next level");

            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Unlocked!", 1);

            if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_coins"))
            {
                if (currentCoins > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_coins"))
                {
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);
                }
            }
            else
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);
            }

            SceneManager.LoadScene(levelToLoad);
        }

        public IEnumerator MenuLoadCO(string levelToLoad)
        {
            PlayerController.instance.stopMove = true;

            UIManager.instance.resumeButton.SetActive(false);
            UIManager.instance.mainMenuButton.SetActive(false);
            UIManager.instance.levelSelectButton.SetActive(false);
            UIManager.instance.optionsButton.SetActive(false);
            UIManager.instance.pausedText.SetActive(false);

            yield return new WaitForSeconds(.2f);

            

            UIManager.instance.fadeToBlack = true;

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(levelToLoad);
        }
    }
}
