using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class BossController : MonoBehaviour
    {
        public static BossController instance;

        public Animator anim;

        public GameObject victoryZone;

        public float exitRevealTime;

        public GameObject hurtBoxLeft, hurtboxRight, hurtBossLeft, hurtBossRight;
        public enum BossPhase { Intro, Phase1, Phase2, Phase3, End };

        public BossPhase currentPhase = BossPhase.Intro;

        public int bossMusic, bossDeath, bossDeathShout, bossHit;
        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            AudioManager.instace.PlayMusic(bossMusic);
        }

        void Update()
        {
            if (GameManager.instance.isRespawning)
            {
                currentPhase = BossPhase.Intro;

                anim.SetBool("Phase1", false);
                anim.SetBool("Phase2", false);
                anim.SetBool("Phase3", false);

                AudioManager.instace.PlayMusic(AudioManager.instace.levelMusicToPlay);

                gameObject.SetActive(false);

                BossActivator.instance.gameObject.SetActive(true);
                BossActivator.instance.entrance.SetActive(true);

                GameManager.instance.isRespawning = false;
            }
        }


      
        
        public void DamageBoss()
        {
            AudioManager.instace.PlaySoundEffects(bossHit);

            currentPhase++;

            if (currentPhase != BossPhase.End)
            {
                anim.SetTrigger("Hurt");
            }

            switch (currentPhase)
            {
                case BossPhase.Phase1:
                    anim.SetBool("Phase1", true);
                    break;
                case BossPhase.Phase2:
                    anim.SetBool("Phase2", true);
                    anim.SetBool("Phase1", false);
                    break;
                case BossPhase.Phase3:
                    anim.SetBool("Phase3", true);
                    anim.SetBool("Phase2", false);
                    break;
                case BossPhase.End:
                    anim.SetTrigger("End");
                    StartCoroutine(EndBoss());
                    break;

            }
            

            IEnumerator EndBoss()
            {
                AudioManager.instace.PlaySoundEffects(bossHit);
                AudioManager.instace.PlaySoundEffects(bossDeathShout);
                AudioManager.instace.PlayMusic(AudioManager.instace.levelMusicToPlay);
                yield return new WaitForSeconds(exitRevealTime);
                victoryZone.SetActive(true);
            }
        }
    }

}
