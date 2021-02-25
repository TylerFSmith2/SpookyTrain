using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    public Character p;

    public GameObject notificationPanel;

    public void Start()
    {
        p = FindObjectOfType<Character>();
    }

    public abstract void SetUnlockStatus(bool var);

    public abstract void Interact();

    public abstract void Highlight();

    public abstract void RemoveHighlight();

    public void Fade(string t)
    {
        StartCoroutine(FadeNotif(t));
    }

    public IEnumerator FadeNotif(string t)
    {
        notificationPanel.GetComponentInChildren<Text>().text = t;
        notificationPanel.SetActive(true);
        Color test = notificationPanel.GetComponentInChildren<Text>().color;
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            Color c = test;
            c.a = ft;
            test = c;
            yield return new WaitForSeconds(.1f);
        }
        notificationPanel.SetActive(false);
    }
}
