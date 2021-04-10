using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Familiar : MonoBehaviour
{
    public float moveSpeed = 7;

    public GameObject player;

    public bool pulse;
    public GameObject pulseAOE;

    public List<Collider> Enemies = new List<Collider>();

    // Update is called once per frame
    void Update()
    {
        if(pulse)
        {
            PulseAbility();
        }
        if (player.GetComponent<Character>().familiarActive)
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                //Animation
                //Paralyze everything near it
                pulse = true;
                pulseAOE.SetActive(true);
            }
            if(Hmovement != 0 || Vmovement != 0)
            {
                pulse = false;
                pulseAOE.SetActive(false);
            }
        }
    }

    public void PulseAbility()
    {
        foreach(Collider c in Enemies)
        {
            c.GetComponent<Paimon>().StartPara(1f);
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

    public void DecolorEnemies()

    {
        foreach (Collider c in Enemies)
        {
            c.GetComponent<CreaturesAbstract>().RemoveColor();
        }
        //player.GetComponent<Character>().DeactivateFamiliar();
    }

    public void SetPlayer(GameObject p)
    {
        player = p;
    }
}
