using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneController : MonoBehaviour
{
    public int SceneIndex;
    public VideoPlayer player;
    public Animator anim;
    public Animator titles;
    int scene;
    public bool loading = true;
    public GameObject udpcon;
    void Start()
    {
#if !UNITY_EDITOR
    player.url = Application.dataPath + "/Videos/video_" + SceneIndex + ".mp4";
#endif
        player.prepareCompleted += Player_prepareCompleted;
        player.loopPointReached += Player_loopPointReached;
        player.Play();
    }

    private void Player_loopPointReached(VideoPlayer source)
    {
        loading = true;
        udpcon.GetComponent<UDPWorker>().StopThread();
        scene = 0;
        anim.Play("FadeIn");
    }

    public void loaded()
    {
        loading = false;
    }
    private void Player_prepareCompleted(VideoPlayer source)
    {
        
        anim.Play("FadeOut");
        titles.Play("Titles");
    }

    public void LoadScene() {
        SceneManager.LoadSceneAsync(scene);
    }

    private static int GetNum() {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            return 4;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            return 5;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            return 6;
        }
        return 0;
    }

    private void Update()
    {
        if (!loading) {
            
            if (Input.GetKeyDown("joystick button 0"))
            {
                loading = true;
                scene = 0;
                anim.Play("FadeIn");
                return;
            }
            if (Input.GetKeyDown("joystick button 1"))
            {
                loading = true;
                scene = 0;
                anim.Play("FadeIn");
                return;
            }
            if (udpcon.GetComponent<UDPWorker>().RightButton)
            {
                udpcon.GetComponent<UDPWorker>().StopThread();
                scene = 0;
                loading = true;
                anim.Play("FadeIn");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (SceneIndex > 1)
                {
                    loading = true;
                    scene = SceneIndex - 1;
                    anim.Play("FadeIn");
                    return;
                    //SceneManager.LoadScene();
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (SceneIndex < 6)
                {
                    loading = true;
                    scene = SceneIndex + 1;
                    anim.Play("FadeIn");
                    return;
                    //SceneManager.LoadScene(SceneIndex + 1);
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                loading = true;
                udpcon.GetComponent<UDPWorker>().StopThread();
                scene = 0;
                anim.Play("FadeIn");
                return;
                //SceneManager.LoadScene(0);
            }
            int num = GetNum();
            if (num > 0 && num != SceneIndex) {
                loading = true;
                scene = num;
                anim.Play("FadeIn");
                return;
                //SceneManager.LoadScene(num);
            } 
        }
    }
}
