using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TuxiGrabHand : MonoBehaviour
{
    [SerializeField] GameObject leftPlayerHand;
    [SerializeField] GameObject rightPlayerHand;

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, leftPlayerHand))
        {
            SceneManager.LoadScene("ReachingCity2");
        }
    }
}
