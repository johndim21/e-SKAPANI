using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TuxiGrabHand : MonoBehaviour
{
    [SerializeField] private GameObject leftPlayerHand;
    [SerializeField] private GameObject rightPlayerHand;
    [SerializeField] private OVRScreenFade cameraFade;

    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.ReferenceEquals(other.gameObject, leftPlayerHand) || GameObject.ReferenceEquals(other.gameObject, rightPlayerHand))
        {
            StartCoroutine(LoadAsyncReachingCityScene());
        }
    }

    IEnumerator LoadAsyncReachingCityScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        cameraFade.FadeOut();
        yield return new WaitForSeconds(2f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ReachingCity");        

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
