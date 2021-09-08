using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField]
        private float weaponRange = 2f;
        [SerializeField]
        private float timeBetweenAttacks = 1f;
        [SerializeField]
        private float weaponDamage = 5f;

        public Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        public static string attackTriggerName = "attack";        
        public static string stopAttackName = "stopAttack";

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.IsDead()) return;

            if (!GetIsInRange())
            {                
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }
        // MARK: Hit Event Trigger
        private void AttackBehavior()
        {            
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger(Fighter.stopAttackName);
            GetComponent<Animator>().SetTrigger(Fighter.attackTriggerName);
        }

        public bool CanAttack(GameObject combatTarget)
        {            
            if (combatTarget == null) { return false; }
            Health testTarget = combatTarget.GetComponent<Health>();
            return testTarget != null && !testTarget.IsDead();
        }

        // MARK: Animation Event
        void Hit()
        {
            if (target == null) { return; }
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();            
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().SetTrigger(Fighter.stopAttackName);
            GetComponent<Animator>().ResetTrigger(Fighter.attackTriggerName);
        }
    }
}