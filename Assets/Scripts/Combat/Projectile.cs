using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1f;        
        private Health target = null;
        float damage = 4f;

        private void OnTriggerEnter(Collider other)
        {            
            if (other.GetComponent<Health>() != target) { return; }            
            target.TakeDamage(damage);            
            Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) { return; }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) { return target.transform.position; }

            return target.transform.position + Vector3.up * targetCapsule.height / 1.6f;
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }
    }
}