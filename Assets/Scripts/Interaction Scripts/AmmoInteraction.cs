using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInteraction : MonoBehaviour
{
    public gun playerGun;

    public AudioSource ammoPickupSFX;
    bool hasPlayed = false;
    public bool hasPickedUpAmmo = false;

    private void Awake()
    {
        playerGun = playerGun.gameObject.GetComponent<gun>();
    }

    public void AmmoInteract()
    {
        hasPickedUpAmmo = true;
        PlayAmmoSFXPickupOnce();
        Invoke("ChangeBool", 5);

        if (playerGun.currentMagazineAmmo != playerGun.maxMagazineAmmo)
        {
            playerGun.currentMagazineAmmo += playerGun.ammoPickupNum;
            Destroy(gameObject);

            // Check that the max magazine counter doesn't go above 230 
            if(playerGun.currentMagazineAmmo > playerGun.maxMagazineAmmo)
            {
                playerGun.currentMagazineAmmo = playerGun.maxMagazineAmmo; 
            }
        }
    }

    void PlayAmmoSFXPickupOnce()
    {
        if (!hasPlayed)
        {
            ammoPickupSFX.Play();
            hasPlayed = true;
        }
    }

    void ChangeBool()
    {
        hasPickedUpAmmo = false;
    }
}
