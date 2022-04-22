using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteractions : MonoBehaviour
{
    public Camera fpsCam;
    public float range = 10f;

    [Header("Skull Interactions")]
    public int skullScore = 0;
    [SerializeField] AudioSource skullPickupSFX;
    [SerializeField] TextMeshProUGUI skullScoreText;

    private int lastSkullCount;

    void Start()
    {
        lastSkullCount = skullScore;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CrateInteraction();
            AmmoInteraction();
        }

        SkullInteraction();
    }

    void CrateInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            CrateInteraction cI = hit.transform.GetComponent<CrateInteraction>();
            if (cI != null && hit.transform.gameObject.tag == "Crate")
            {
                cI.CrateInteract();
            }
        }
    }

    void AmmoInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            AmmoInteraction aI = hit.transform.GetComponent<AmmoInteraction>();
            if (aI != null && hit.transform.gameObject.tag == "Ammo")
            {
                aI.AmmoInteract();
                Debug.Log("30 Rounds Picked Up!");
            }
        }
    }

    void SkullInteraction()
    {
        skullScoreText.text = "skulls collected: " + skullScore.ToString();
        if(skullScore > lastSkullCount)
        {
            skullPickupSFX.Play();
            lastSkullCount = skullScore;
        }
        else
        {
            lastSkullCount = skullScore;
        }
    }
}
