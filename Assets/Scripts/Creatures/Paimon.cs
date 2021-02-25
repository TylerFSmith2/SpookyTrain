using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paimon : CreaturesAbstract
{
    //public Transform[] patrolPoints;
    public Vector3[] patrolPoints;

    public int nextPoint;

    public void Start()
    {
        speed = 2;
        visionSphere = GetComponentInChildren<VisionSphere>();
        //paralyzedEffect = GetComponentInChildren<ParticleSystem>();
        //paralyzedEffect.gameObject.SetActive(false);
        monsterColor.gameObject.SetActive(false);

        player = FindObjectOfType<Character>().gameObject;
    }

    public void Update()
    {
        //If player is in range
        if(visionSphere.GetCanSeePlayer() && !paralyzed && !player.GetComponent<Character>().GetHidden())
        {
            ChasePlayer();
        }
        else if (!paralyzed)
        {
            //Patrol
            GoToNextPoint();
        }
        if (Vector3.Distance(transform.gameObject.transform.position, player.transform.position) < 2 && !player.GetComponent<Character>().GetHidden() && !paralyzed)
        {
            //Kill player
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Rotate to face forward
        if (GetComponent<Rigidbody>().velocity.z > 0)
        {
            //FIXME: Flip when going right, unflip when going left
            GetComponent<SpriteRenderer>().flipX = GetComponent<Rigidbody>().velocity.z > 0;
        }
    }

    public void GoToNextPoint()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }

        float step = speed/2 * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[nextPoint], step);

        if(Vector3.Distance(transform.gameObject.transform.position, patrolPoints[nextPoint]) < 1f)
        {
            if(nextPoint < patrolPoints.Length -1)
            {
                nextPoint++;
            }
            else
            {
                nextPoint = 0;
            }
        }

    }

    public override void ChasePlayer()
    {
        //move to player
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

    }

    public void StartPara(float timer)
    {
        StopAllCoroutines();
        StartCoroutine(Paralyzed(timer));
    }

    public IEnumerator Paralyzed(float timer)
    {
        //Paralyze Paimon
        paralyzed = true;
        transform.GetComponent<SpriteRenderer>().color = Color.yellow;

        //Fade color
        for (float ft = 0f; ft <= 1f; ft += 0.1f)
        {
            Color c = transform.GetComponent<SpriteRenderer>().color;
            c.b = ft;
            transform.GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(.1f);
        }

        //Unparalyze Paimon
        transform.GetComponent<SpriteRenderer>().color = Color.white;
        paralyzed = false;        
    }


}
