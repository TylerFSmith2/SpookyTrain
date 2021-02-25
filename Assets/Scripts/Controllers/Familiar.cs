using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Familiar : MonoBehaviour
{
    public bool familiarActive = false;

    public float moveSpeed = 7;

    public GameObject player;

    public List<Collider> Enemies = new List<Collider>();

    // Update is called once per frame
    void Update()
    {
        if (familiarActive)
        {
            //Control player movement
            float Hmovement = Input.GetAxis("Horizontal");
            float Vmovement = Input.GetAxis("Vertical");
            transform.position += new Vector3(-Vmovement, 0, Hmovement) * Time.deltaTime * moveSpeed;

            //Rotate to face forward
            if (!Mathf.Approximately(0, Hmovement))
            {
                GetComponent<SpriteRenderer>().flipX = Hmovement > 0;
            }

            //Burst Ability
            if (Input.GetKeyDown(KeyCode.V))
            {
                //Animation
                //Draw monsters closer
                PulseAbility();
            }
            
            //Deactivate Familiar
            if(Input.GetKeyDown(KeyCode.Q))
            {
                StopFamiliar();
            }
        }
    }

    public void PulseAbility()
    {
        foreach(Collider c in Enemies)
        {
            c.GetComponent<Paimon>().StartPara(40f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemies.Add(other);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemies.Remove(other);
        }
    }


    public void StartFamiliar()
    {
        familiarActive = true;
    }

    public void StopFamiliar()
    {
        familiarActive = false;
        foreach (Collider c in Enemies)
        {
            c.GetComponent<CreaturesAbstract>().RemoveColor();
        }
        player.GetComponent<Character>().DeactivateFamiliar();
    }

    public void SetPlayer(GameObject p)
    {
        player = p;
    }
}
