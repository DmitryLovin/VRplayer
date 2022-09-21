using UnityEngine;

public class LookingTitleController : MonoBehaviour
{
    public float angleLeft;
    public float angleRight;
    public float angleUp;
    public float angleDown;


    public Animator imageLeft,imageRight,imageUp,imageDown,helpTitle;

    public bool active = false;


    void setAnimBool(int index) {
        switch (index) {
            case 0: {
                    imageUp.SetBool("in", true);
                    imageDown.SetBool("in", false);
                    imageLeft.SetBool("in", false);
                    imageRight.SetBool("in", false); break; }
            case 1: {
                    imageUp.SetBool("in", false);
                    imageDown.SetBool("in", true);
                    imageLeft.SetBool("in", false);
                    imageRight.SetBool("in", false); break; }
            case 2: {
                    imageUp.SetBool("in", false);
                    imageDown.SetBool("in", false);
                    imageLeft.SetBool("in", true);
                    imageRight.SetBool("in", false); break; }
            case 3: {
                    imageUp.SetBool("in", false);
                    imageDown.SetBool("in", false);
                    imageLeft.SetBool("in", false);
                    imageRight.SetBool("in", true); break; }
            case 4: {
                    imageUp.SetBool("in", false);
                    imageDown.SetBool("in", false);
                    imageLeft.SetBool("in", false);
                    imageRight.SetBool("in", false); break;
                }
        }
    }

    public void turnOff() {
        active = false;
        imageUp.SetBool("in", false);
        imageDown.SetBool("in", false);
        imageLeft.SetBool("in", false);
        imageRight.SetBool("in", false);
    }

    public void setHelp() {
        helpTitle.SetBool("in", true);
    }
    public void unsetHelp() {
        helpTitle.SetBool("in", false);
    }

    void Update()
    {
        if (active) {
            Vector3 angles = Camera.main.transform.localEulerAngles;
            if (angles.x < angleUp && angles.x > 180)
            {
                setAnimBool(0);
            }
            else if (angles.x > angleDown && angles.x < 180)
            {
                setAnimBool(1);
            }
            else if (angles.y > angleRight && angles.y < 180)
            {
                setAnimBool(3);
            }
            else if (angles.y < angleLeft && angles.y > 180)
            {
                setAnimBool(2);
            }
            else
            {
                setAnimBool(4);
            }
        }
    }
}
