using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 1f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Vector3 waypoint = GetWaypoint(i);
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(waypoint, waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }
        /// <summary>
        ///  returns the next available index from `transform.childCount`
        ///  if it reaches the last position, it returns the first index
        ///  </summary>
        public int GetNextIndex(int currentIndex)
        {
            if (currentIndex + 1 == transform.childCount) { return 0; }

            return currentIndex + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}