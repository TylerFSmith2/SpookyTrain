using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreaturesAbstract : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public bool paralyzed;
    public VisionSphere visionSphere;
    public ParticleSystem monsterColor;

    

    public abstract void ChasePlayer();

    public void ShowColor()
    {
        monsterColor.gameObject.SetActive(true);
    }

    public void RemoveColor()
    {
        monsterColor.gameObject.SetActive(false);
    }
}
