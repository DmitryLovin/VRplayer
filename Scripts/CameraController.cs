using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float sens;
    public VideoPlayer video;
    public Animator anim;
    public RenderTexture image;
    public int frameIndex;
    public bool started = false;
    bool pause = false;

    private void Start()
    {
        video.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) {
            if (video.frame != -1) {
                video.frame = 0;
                video.Pause();
                started = !started;
            }
        }
        if (!pause) {
            Vector3 newAngle = transform.eulerAngles;
            newAngle += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            transform.eulerAngles = newAngle;
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            video.StepForward();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            anim.Play("Camera");
            video.Play();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pause = !pause;
            Debug.Log(pause);
        }
    }
}
