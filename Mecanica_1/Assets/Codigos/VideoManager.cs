using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public GameObject mainVideoPlane;
    public GameObject secondaryVideoPlane;

    public VideoPlayer mainVideoPlayer;
    public VideoPlayer secondaryVideoPlayer;

    private MeshRenderer mainRenderer;
    private MeshRenderer secondaryRenderer;

    private bool isSecondaryPlaying = false;
    private float inactivityTimer = 0f;
    public float returnDelay = 3f;

    void Start()
    {
        mainRenderer = mainVideoPlane.GetComponent<MeshRenderer>();
        secondaryRenderer = secondaryVideoPlane.GetComponent<MeshRenderer>();

        PlayMainVideo();
    }

    void Update()
    {
        if (isSecondaryPlaying)
        {
            inactivityTimer += Time.deltaTime;

            if (inactivityTimer >= returnDelay)
            {
                PlayMainVideo();
            }
        }
    }

    public void PlaySecondaryVideo()
    {
        mainVideoPlayer.Pause();
        mainRenderer.enabled = false;              

        secondaryRenderer.enabled = true;          
        secondaryVideoPlayer.Play();

        isSecondaryPlaying = true;
        inactivityTimer = 0f;
    }

    public void PlayMainVideo()
    {
        secondaryVideoPlayer.Stop();
        secondaryRenderer.enabled = false;

        mainRenderer.enabled = true;
        mainVideoPlayer.Play();                    

        isSecondaryPlaying = false;
        inactivityTimer = 0f;
    }
}
