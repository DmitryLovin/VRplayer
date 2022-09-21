using UnityEngine;

public class CameraTest : MonoBehaviour
{
    // Start is called before the first frame update
    bool pause = false;
  
    // Update is called once per frame
    void Update()
    {
        if (!pause) {
            Vector3 newAngle = transform.eulerAngles;
            newAngle += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            transform.eulerAngles = newAngle;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
    }
}
