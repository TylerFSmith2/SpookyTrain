using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 velocity;

    public float smoothTimeX;
    public float smoothTimeZ;

    public float offsetX = 1.5f;

    public float sensitivity = 5f;

    public GameObject focus;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(offsetX - Input.GetAxis("Mouse ScrollWheel") * sensitivity >= 1.0f && offsetX - Input.GetAxis("Mouse ScrollWheel") * sensitivity < 4)
        {
            offsetX -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        }
        //Follow player
        //TODO: Have Camera follow players change in X
        float posX = Mathf.SmoothDamp(transform.position.x, focus.transform.position.x, ref velocity.x, smoothTimeX);
        float posZ = Mathf.SmoothDamp(transform.position.z, focus.transform.position.z, ref velocity.z, smoothTimeZ);

        transform.position = new Vector3(posX + offsetX, transform.position.y, posZ);
    }

    public void setFocus(GameObject newFocus)
    {
        focus = newFocus;
    }
}
