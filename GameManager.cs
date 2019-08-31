using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private Vector3 respawnPos, camSpawnPos;

        public int currentCoins;

        public GameObject deathEffect;
        void Awake()
        {
            instance = this;
        }
        void Start()
        {
            respawnPos = PlayerController.instance.transform.position;
           
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            AddCoins(0);
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
    }

}
