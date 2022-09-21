using UnityEngine;

public class MenuMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Camera.main.transform.position;
        newPos += offset;
        transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
    }
}
