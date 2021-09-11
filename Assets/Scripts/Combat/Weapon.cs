using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using System;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
        public class Weapon: ScriptableObject
    {
        [SerializeField]
        private AnimatorOverrideController animatorOverride = null;
        [SerializeField]
        private GameObject equippedPrefab = null;

        [SerializeField]
        private float weaponRange = 2f;
        [SerializeField]
        private float weaponDamage = 5f;
        [SerializeField]
        public float timeBetweenAttacks = 2f;
        [SerializeField]
        private Projectile projectile = null;

        [SerializeField]
        private bool isRightHanded = true;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = RPGSandboxIDs.WeaponName;
            } else
            {
                Debug.Log("equippedPrefab is null");
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform previousWeapon = rightHand.Find(RPGSandboxIDs.WeaponName);
            if (previousWeapon == null)
            {
                previousWeapon = leftHand.Find(RPGSandboxIDs.WeaponName);                
            }            
            if (previousWeapon == null) { return; }
            Debug.Log("previous weapon: " + previousWeapon.name);
            previousWeapon.name = "DESTROYED";
            Destroy(previousWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded)
            {
                handTransform = rightHand;
            }
            else
            {
                handTransform = leftHand;
            }
            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Debug.Log("Launching projectile");
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
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