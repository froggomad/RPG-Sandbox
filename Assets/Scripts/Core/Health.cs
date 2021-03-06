using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        private bool isDead = false;
        ActionScheduler actionScheduler;

        private void Awake()
        {
            actionScheduler = GetComponent<ActionScheduler>();
        }

        public bool IsDead()
        {
            return isDead;
        }

        [SerializeField]
        public float hps = 100f;

        public void TakeDamage(float damage)
        {
            hps = Mathf.Max(hps - damage, 0);
            print(hps);
            if (hps == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            actionScheduler.CancelCurrentAction();
            isDead = true;
            GetComponent<Animator>().SetTrigger(RPGSandboxIDs.DieTriggerName);
        }

        public object CaptureState()
        {
            return hps;
        }

        public void RestoreState(object state)
        {
            hps = (float)state;
            if (hps == 0)                
            {
                Die();
            }
        }
    }
}