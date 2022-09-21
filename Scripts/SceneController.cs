using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator[] Images;
    public Animator anim;
    int CurrentScene = 2;
    bool loading = true;

    public GameObject udpcon;
    void Start()
    {
        
        for (int i = 0; i < Images.Length; i++) {
            Images[i].Play("Out" + i);
        }
        Images[CurrentScene].SetBool("In", true);
        anim.Play("FadeOut");
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 0) {
            anim.Play("FadeOut");
        }
    }
    public void loaded() {
        loading = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!loading) {
            
            int newCurrentScene = CurrentScene;
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < Images.Length; i++)
                {
                    if (hit.transform == Images[i].transform)
                    {
                        newCurrentScene = i;
                        continue;
                    }
                }
            }
            if (newCurrentScene != CurrentScene)
            {
                Images[CurrentScene].SetBool("In", false);
                Images[newCurrentScene].SetBool("In", true);
                CurrentScene = newCurrentScene;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                int thisScene = CurrentScene;
                CurrentScene = CurrentScene > 0 ? CurrentScene - 1 : 0;
                if (thisScene != CurrentScene)
                {
                    Images[thisScene].SetBool("In", false);
                    Images[CurrentScene].SetBool("In", true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                int thisScene = CurrentScene;
                CurrentScene = CurrentScene < Images.Length - 1 ? CurrentScene + 1 : Images.Length - 1;
                if (thisScene != CurrentScene)
                {
                    Images[thisScene].SetBool("In", false);
                    Images[CurrentScene].SetBool("In", true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                loading = true;
                anim.Play("FadeIn");

            }
            if (Input.GetKeyDown("joystick button 0")) {
                loading = true;
                anim.Play("FadeIn");
            }
            if (udpcon.GetComponent<UDPWorker>().RightButton) {
                udpcon.GetComponent<UDPWorker>().StopThread();
                loading = true;
                anim.Play("FadeIn");
            }
        }
    }
    public void LoadScene() {
        SceneManager.LoadSceneAsync(CurrentScene + 1);
    }
}
