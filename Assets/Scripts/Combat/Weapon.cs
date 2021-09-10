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
        [SerializeField]
        public float timeBetweenAttacks = 2f;

        [SerializeField]
        private bool isRightHanded = true;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            Transform handTransform;
            if (isRightHanded)
            {
                handTransform = rightHand; 
            } else
            {
                handTransform = leftHand;
            }
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