using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerGoToFirstFrame : MonoBehaviour
{
    private void Start()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += EndReached;
    }

    private void EndReached(VideoPlayer vp)
    {
        this.gameObject.SetActive(false);
        vp.frame = 0;
    }
}
