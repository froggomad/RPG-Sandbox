using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
        public class Weapon: ScriptableObject
    {
        [SerializeField]
        private AnimatorOverrideController animatorOverride = null;
        [SerializeField]
        private GameObject weaponPrefab = null;

        [SerializeField]
        private float weaponRange = 2f;
        [SerializeField]
        private float weaponDamage = 5f;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (weaponPrefab != null)
            {
                Instantiate(weaponPrefab, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            
        }

        public float WeaponRange()
        {
            return weaponRange;
        }

        public float WeaponDamage()
        {
            return weaponDamage;
        }
    }
 
}