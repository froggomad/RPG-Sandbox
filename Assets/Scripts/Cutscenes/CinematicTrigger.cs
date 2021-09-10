using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;

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
            if (other.gameObject.tag == PlayerController.Tag)
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