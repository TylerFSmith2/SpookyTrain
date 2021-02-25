using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public Character p;

    public float speed;
    public float currentscroll;

    public Material mat;
    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Character>();
    }

    void Update()
    {
        transform.position = new Vector3(-18.25f, 5f, p.transform.position.z);
        
        currentscroll += speed * Time.deltaTime;
        mat.mainTextureOffset = new Vector2(0, -currentscroll);
    }
}
