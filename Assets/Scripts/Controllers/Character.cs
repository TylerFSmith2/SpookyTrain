using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    public CameraFollow cam;

    public float moveSpeed = 3;
    public float jumpForce = 5;

    public Interactable currentInteractable;

    public List<GameObject> familiars = new List<GameObject>();
    public bool familiarActive = false;
    public GameObject familiarSpawn;
    public GameObject familiarPrefab;

    public CreaturesAbstract[] creatures;
    public NPC[] npcs;

    public bool hidden = false;
    public bool cantMove = false;

    public int hidingItems = 3;
    public GameObject hidingItemSpawn;
    public GameObject hidingItemPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        creatures = FindObjectsOfType<CreaturesAbstract>();
        npcs = FindObjectsOfType<NPC>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Spawn Familiar
        if (Input.GetKeyDown(KeyCode.V))
        {
            ActivateFamiliar();
        }


        //Block while familiar is active
        if (!familiarActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Interact
                Interact();
            }

            //Activate Sight
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

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (hidingItems > 0)
                {
                    //Spawn Hiding Item
                    GameObject hidItem = Instantiate(hidingItemPrefab, hidingItemSpawn.transform.position, Quaternion.identity);
                    hidItem.name = "HidingItem " + hidingItems;
                    hidingItems--;
                    hidden = true;
                }
                else
                {
                    Debug.Log("No more hiding items available");
                }
            }


            
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

    public void SetCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
    }

    public Interactable GetCurrentInteractable()
    {
        return currentInteractable;
    }
    public void Interact()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    public void DeactivateFamiliar()
    {
        foreach (CreaturesAbstract c in creatures)
        {
            c.RemoveColor();
        }

        familiarActive = false;

        //Refocus camera on player
        cam.setFocus(gameObject);
    }

    public void ActivateFamiliar()
    {
        foreach (CreaturesAbstract c in creatures)
        {
            c.ShowColor();
        }

        GameObject f;
        familiarActive = true;
        if (familiars.Count == 0)
        {
            f = Instantiate(familiarPrefab, familiarSpawn.transform.position, Quaternion.identity);
            familiars.Add(f);
            f.transform.Rotate(new Vector3(0f, 90f));
        }
        else
        {
            f = familiars[0];
        }
        

        //Focus camera on familiar
        cam.setFocus(f);

        //Set Familiar player var
        f.GetComponent<Familiar>().familiarActive = true;
        f.GetComponent<Familiar>().SetPlayer(gameObject);
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
