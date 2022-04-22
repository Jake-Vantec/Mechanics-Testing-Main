using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkullInteraction : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI skullScoreText;

    public PlayerInteractions playerInteractions; 

    public bool playerCollectedSkull = false;

    void Awake()
    {
        playerInteractions = playerInteractions.gameObject.GetComponent<PlayerInteractions>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerCollectedSkull = true;

            if(playerCollectedSkull == true)
            {
                playerInteractions.skullScore += 1;
            }

            playerCollectedSkull = false;
            Destroy(gameObject);
        }
    }
}
