using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public Inventory inventory;
    public ItemManager im;

    private Rigidbody rb;
    public CameraFollow cam;

    public float moveSpeed = 3;
    //public float currentSpeed = 3; - Change with concentration?
    public float jumpForce = 5;

    public PlayerInteractables interactableSphere;

    public List<GameObject> familiars = new List<GameObject>();
    public bool familiarActive = false;
    public GameObject familiarSpawn;
    public GameObject familiarPrefab;

    public CreaturesAbstract[] creatures;
    public NPC[] npcs;

    public bool hidden = false;
    public bool cantMove = false;

    public bool concentrating = false;
    public float currentConcentratingTime;
    public float maxConcentratingTime;
    public float concentratingCD;

    public int hidingItems = 3;
    public GameObject hidingItemSpawn;
    public GameObject hidingItemPrefab;
    public Text hidingItemText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        creatures = FindObjectsOfType<CreaturesAbstract>();
        npcs = FindObjectsOfType<NPC>();
        concentratingCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(familiarActive);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Flip control between player and familiar
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(familiarActive)
            {
                DeactivateFamiliar();
            }
            else
            {
                ActivateFamiliar();
            }
        }

        //Concentration CD continues while familiar is active
        if (concentratingCD > 0)
        {
            concentratingCD -= Time.deltaTime;
        }

        //Block while familiar is active
        if (!familiarActive)
        {
            if (Input.GetKeyDown(KeyCode.E) && interactableSphere.toInteract != null)
            {
                //Interact
                interactableSphere.Interact();
            }

            //Enter Special Concentrated State
            //Changes the world around.
            //1 - Monsters cannot see/have reduced vision
            //2 - Can see certain items (marked with a 'concentration' var)
            //3 - They move faster
            


            if (Input.GetKey(KeyCode.LeftShift) && concentratingCD <= 0)
            {
                //Time reaches 0 while concentrating
                if (concentrating && currentConcentratingTime <= 0.3)
                {
                    //Reset vars
                    hidden = false;
                    concentrating = false;

                    //Calculate CD
                    concentratingCD = (1 / currentConcentratingTime + 1);
                    FindObjectOfType<SkillBarControl>().StartCD(concentratingCD);
                    GetComponent<SpriteRenderer>().color = Color.white;

                    im.SetRealmToHuman();
                }
                //If concentration continues
                else if (concentrating && currentConcentratingTime > 0.3)
                {
                    currentConcentratingTime -= Time.deltaTime;
                }
                //If concentrating for the first time
                else
                {
                    //First time stuff 
                    //Set current to max
                    currentConcentratingTime = maxConcentratingTime;

                    im.SetRealmToYokai();

                    //Start CD
                    FindObjectOfType<SkillBarControl>().StartDuration(currentConcentratingTime);
                    GetComponent<SpriteRenderer>().color = Color.blue;

                    hidden = true;
                    concentrating = true;
                }
            }
            //Shift is released and there was time left
            else if (currentConcentratingTime < 5f && currentConcentratingTime != 0)
            {
                //Reset vars
                hidden = false;
                concentrating = false;

                //Calculate CD
                concentratingCD = (1 / currentConcentratingTime) * 10;
                FindObjectOfType<SkillBarControl>().StartCD(concentratingCD);
                GetComponent<SpriteRenderer>().color = Color.white;

                //Set current to max
                currentConcentratingTime = maxConcentratingTime;
                im.SetRealmToHuman();
            }

            //Activate Sight -- Currently removed. TODO: Fully remove
            /*
            if (Input.GetKey(KeyCode.Q))
            {
                //Show all monster colors
                foreach (CreaturesAbstract c in creatures)
                {
                    c.ShowColor();
                }
                foreach (NPC n in npcs)
                {
                    n.ShowColor();
                }
                moveSpeed = 1.5f;
            }
            else
            {
                foreach (CreaturesAbstract c in creatures)
                {
                    c.RemoveColor();
                }
                foreach (NPC n in npcs)
                {
                    n.RemoveColor();
                }
                moveSpeed = 3f;
            }
            */

            /*
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (hidingItems > 0)
                {
                    //Spawn Hiding Item
                    GameObject hidItem = Instantiate(hidingItemPrefab, hidingItemSpawn.transform.position, Quaternion.identity);
                    hidItem.name = "HidingItem " + hidingItems;
                    hidingItems--;
                    hidden = true;
                    hidingItemText.text = "Talismans: " + hidingItems;
                }
                else
                {
                    Debug.Log("No more hiding items available");
                }
            }
            */


            
            //Jump?
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                rb.AddForce(new Vector3(0, jumpForce), ForceMode.Impulse);
            }
        }
        

        
    }

    public void FixedUpdate()
    {
        //Control player movement
        if (!cantMove && !familiarActive)
        {
            float Hmovement = Input.GetAxis("Horizontal");
            float Vmovement = Input.GetAxis("Vertical");
            transform.position += new Vector3(-Vmovement, 0, Hmovement) * Time.deltaTime * moveSpeed;

            //Rotate to face forward
            if(!Mathf.Approximately(0, Hmovement))
            {
                GetComponent<SpriteRenderer>().flipX = Hmovement > 0;
            }    
        }
    }



    public void ActivateFamiliar()
    {
        GameObject f;
        familiarActive = true;
        foreach (CreaturesAbstract c in creatures)
        {
            c.ShowColor();
        }

        
        if (familiars.Count == 0)
        {
            f = Instantiate(familiarPrefab, familiarSpawn.transform.position, Quaternion.identity);
            familiars.Add(f);
            f.transform.Rotate(new Vector3(0f, 90f));
            f.GetComponent<Familiar>().SetPlayer(gameObject);
        }
        else
        {
            f = familiars[0];
        }
        //Focus camera on familiar
        cam.setFocus(f);
    }

    public void DeactivateFamiliar()
    {
        familiarActive = false;
        foreach (CreaturesAbstract c in creatures)
        {
            c.RemoveColor();
        }
        //Refocus camera on player
        cam.setFocus(gameObject);
    }

    public bool GetHidden()
    {
        return hidden;
    }
    public void SetHidden(bool var)
    {
        hidden = var;
    }
}
