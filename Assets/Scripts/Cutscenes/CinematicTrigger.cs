using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;

namespace RPG.Cutscenes
{
    public class CinematicTrigger : MonoBehaviour
    {

        private bool isTriggered = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<PlayableDirector>().Stop();
            }
        }

        private void OnTriggerEnter(Collider other)
        {            
            if (other.gameObject.tag == RPGSandboxIDs.PlayerTag)
            {
                
                if (!isTriggered)
                {
                    isTriggered = true;
                    GetComponent<PlayableDirector>().Play();
                }
            }
        }
    }
}