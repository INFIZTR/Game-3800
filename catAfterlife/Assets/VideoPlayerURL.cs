using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerURL : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign the VideoPlayer in the Inspector
    public string videoURL = "https://example.com/yourvideo.mp4";

    private void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer component is not assigned.");
            return;
        }

        // Set the VideoPlayer source to URL
        videoPlayer.source = VideoSource.Url;

        // Assign the URL
        videoPlayer.url = videoURL;

        // Prepare and play the video
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        vp.Play();
        Debug.Log("Video started playing from URL: " + videoURL);
    }
}
