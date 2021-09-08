using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField]
        private float chaseDistance = 5f;
        [SerializeField]
        private float suspicionTime = 5f;
        [SerializeField]
        private PatrolPath patrolPath;
        [SerializeField]
        private float waypointTolerance = 1f;
        [SerializeField]
        private float dwellTime = 3f;

        [Range(0,1)] [SerializeField]
        private float patrolSpeedFraction = 0.2f;

        private GameObject player;
        private Fighter fighter;
        private Health health;

        private Mover mover;
        private Vector3 guardPosition;
        private int currentWaypointIndex = 0;
        private float timeSincePatrol = Mathf.Infinity;

        private float timeSinceLastSawPlayer = Mathf.Infinity;
        
        private void Start()
        {
            player = GameObject.FindWithTag(PlayerController.Tag);
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            
            mover = GetComponent<Mover>();
            guardPosition = transform.position;            
        }

        private void Update()
        {
            if (health.IsDead()) { return; }
            if (FoundPlayer() && fighter.CanAttack(player))
            {   
                AttackBehavior();
            }
            else if (IsSuspicious())
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }
            UpdateTimers();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private void UpdateTimers()
        {
            timeSincePatrol += Time.deltaTime;
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSincePatrol = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (CanPatrol())
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private bool CanPatrol()
        {
            return timeSincePatrol > dwellTime;
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private bool FoundPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position) <= chaseDistance;            
        }

        public void ReturnToPosition()
        {
            mover.StartMoveAction(guardPosition, patrolSpeedFraction);
        }

        private bool IsSuspicious()
        {
            return timeSinceLastSawPlayer < suspicionTime;
        }

    }
}