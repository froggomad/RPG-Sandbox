using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Control;
using RPG.Core;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {

        enum DestinationIdentifier
        {
            LootedHomeHouse, LootedHomeForest, LootedHomeShack, ForestStart,
            HouseStart
        }

        [SerializeField]
        float fadeInTime = 3f;

        [SerializeField]
        float fadeOutTime = 3f;

        [SerializeField]
        float waitTime = 1f;

        [SerializeField]
        private DestinationIdentifier destination;

        [SerializeField]
        private Transform spawnPoint;

        [SerializeField]
        private int sceneToLoad = -1;

        private void OnTriggerEnter(Collider other)
        {
            GameObject player = GameObject.FindGameObjectWithTag(RPGSandboxIDs.PlayerTag);
            if (other.gameObject == player) {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("scene not set on portal!");
                yield break;
            }
            
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);

            SavingWrapper saver = FindObjectOfType<SavingWrapper>() ;
            saver.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            saver.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            saver.Save();

            yield return new WaitForSeconds(waitTime);
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
            
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag(RPGSandboxIDs.PlayerTag);            
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;            
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) { continue; }
                if (portal.destination != destination) { continue; }
                return portal;
            }
            return null;
        }
    }
}