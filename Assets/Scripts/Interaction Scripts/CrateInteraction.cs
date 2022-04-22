using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateInteraction : MonoBehaviour
{
    public Animator animator;
    public GameObject unopenedColliders; 
    public GameObject openedColliders;
    AudioSource chestSFX;

    float sec = 0.3f;

    bool hasPlayed = false;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
        chestSFX = GetComponent<AudioSource>();  
    }

    public void CrateInteract()
    {
        animator.SetBool("isOpen", true);
        PlayOpeningSoundOnce();
        StartCoroutine(ColliderSwap(sec));
    }

    void PlayOpeningSoundOnce()
    {
        if (!hasPlayed)
        {
            chestSFX.Play();
            hasPlayed = true;
        }
    }

    IEnumerator ColliderSwap(float seconds)
    {
        if (unopenedColliders.activeInHierarchy)
            openedColliders.SetActive(false);

        yield return new WaitForSeconds(seconds);

        openedColliders.SetActive(true);
        unopenedColliders.SetActive(false);
    }
}
