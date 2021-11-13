using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DillonsDream
{
    public class HealthManager : MonoBehaviour
    {
        public static HealthManager instance;
        public int currentHealth;
        public int maxHealth;
        public int hurtSound;

        public float invincibleLength = 2f;
        private float invincCounter;

        public GameObject deathEffect;

        public Sprite[] healthImages;
        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            ResetHealth();
        }

        void Update()
        {
            if (invincCounter > 0)
            {
                invincCounter -= Time.deltaTime;

                for (int i = 0; i < PlayerController.instance.playerPeices.Length; i++)
                {
                    if (Mathf.Floor(invincCounter * 5f) % 2 == 0)
                    {
                        PlayerController.instance.playerPeices[i].SetActive(true);
                    }
                    else
                    {
                        PlayerController.instance.playerPeices[i].SetActive(false);
                    }

                    if (invincCounter <= 0)
                    {
                        PlayerController.instance.playerPeices[i].SetActive(true);
                    }
                } 
            }
        }

        public void Hurt()
        {
            if (invincCounter <=0)
            {
                currentHealth--;
                AudioManager.instace.PlaySoundEffects(hurtSound);
                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    GameManager.instance.Respawn();
                }
                else
                {
                    PlayerController.instance.KnockBack();
                    invincCounter = invincibleLength;     
                }
                UpdateUI();
            }
        }

        public void ResetHealth()
        {
            currentHealth = maxHealth;
            UIManager.instance.healthImage.enabled = true;
            UpdateUI();
        }

        public void AddHealth(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            UpdateUI();
        }

        public void UpdateUI()
        {
            UIManager.instance.healthText.text = currentHealth.ToString();

            switch (currentHealth)
            {
                case 5:
                    UIManager.instance.healthImage.sprite = healthImages[4];
                    break;
                case 4:
                    UIManager.instance.healthImage.sprite = healthImages[3];
                    break;
                case 3:
                    UIManager.instance.healthImage.sprite = healthImages[2];
                    break;
                case 2:
                    UIManager.instance.healthImage.sprite = healthImages[1];
                    break;
                case 1:
                    UIManager.instance.healthImage.sprite = healthImages[0];
                    break;
                case 0:
                    UIManager.instance.healthImage.enabled = false;
                    break;
            }
        }

        public void PlayerKilled()
        {
            currentHealth = 0;
            UpdateUI();
        }
    }

}
