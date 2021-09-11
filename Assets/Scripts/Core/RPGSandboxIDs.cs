using UnityEngine;
using System.Collections;

namespace RPG.Core { 
    public class RPGSandboxIDs : MonoBehaviour
    {
        public static string PlayerTag { get { return "Player"; } }
        public static string DieTriggerName { get { return "die"; } }
        public static string AttackTriggerName { get { return "attack"; } }
        public static string StopAttackName { get { return "stopAttack"; } }
        public static string WeaponName { get { return "Weapon"; } }
    }
}
