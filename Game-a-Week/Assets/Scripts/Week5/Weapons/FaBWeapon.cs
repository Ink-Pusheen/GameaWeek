using TMPro;
using UnityEngine;

public class FaBWeapon : MonoBehaviour
{
    [Header("Firing")]

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject[] fireSpots;

    [SerializeField] string heldUser;

    [Header("Ammo")]

    [SerializeField] bool bIsInfinite; //Is this weapon currently infinite?

    [SerializeField] int magazineAmmo; //What is currently in the clip
    [SerializeField] int maxAmmoClip; //Maximum ammo that can go into one reload

    [SerializeField] int remainingAmmo; //What ammo is left for that weapon

    [SerializeField] bool reloading; //Is the user currently reloading?

    [SerializeField] float reloadQueueTime; //Time.time of when the reload was done
    [SerializeField] float reloadingTime; //How long does it take for the user to reload?

    [Header("Extras")]

    [SerializeField] bool bEnemyBullet; //Is this gonna be an enemy bullet?

    [Header("UI")]

    [SerializeField] GameObject UIElement; //Toggleable UI element

    [SerializeField] TMP_Text ammoText;

    [SerializeField] bool bUpdateUI; //Should this weapons UI be updated?

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(bUpdateUI) ammoText.text = $"{magazineAmmo}/{remainingAmmo}";
    }

    // Update is called once per frame
    void Update()
    {
        if (!reloading) return;

        //Run if the player is currently reloading
        if (Time.time > reloadQueueTime + reloadingTime)
        {
            reloading = false; //Stop reloading

            if (bIsInfinite) //If infinite, do not take ammo away
            {
                magazineAmmo = maxAmmoClip;
                return;
            }

            if (remainingAmmo < maxAmmoClip) //Reload what is left of the pouch
            {
                magazineAmmo = remainingAmmo;
                remainingAmmo = 0;
            }
            else //Take away from the max ammo [With calculation of currently in chamber]
            {
                remainingAmmo -= (maxAmmoClip - magazineAmmo);
                magazineAmmo = maxAmmoClip;
            }

            ammoText.text = $"{magazineAmmo}/{remainingAmmo}";
        }
    }

    public void FireWeapon()
    {
        if (magazineAmmo == 0 && !bIsInfinite) return; //Don't do anything, you are out of ammo bish

        for (int fireSpot = 0; fireSpot < fireSpots.Length; fireSpot++)
        {
            GameObject bullet = Instantiate(bulletPrefab, fireSpots[fireSpot].transform.position, Quaternion.identity);

            bullet.transform.rotation = fireSpots[fireSpot].transform.rotation;

            bullet.GetComponent<FaBBullet>().firingUser = heldUser;

            if (bEnemyBullet) bullet.GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (!bIsInfinite) magazineAmmo--; //Subtract from ammo if not infinite    

        if (!bUpdateUI) return; //Return if it shouldn't update

        ammoText.text = $"{magazineAmmo}/{remainingAmmo}";

        if (magazineAmmo == 0 && remainingAmmo == 0)
        {
            ammoText.text = $"0/0";
            ammoText.color = Color.red;
        }
    }

    public void OnEquipt(string inUser)
    {
        heldUser = inUser;

        DisplayUI();
    }

    public void DisplayUI()
    {
        UIElement.SetActive(true);
    }

    public void HideUI()
    {
        UIElement.SetActive (false);
    }

    public void OnDrop()
    {
        heldUser = "";

        UIElement.SetActive(false);
    }

    //Function to queue the weapon reload
    public void ReloadWeapon()
    {
        if (reloading) return; //Don't proceeed if already reloading

        if (remainingAmmo == 0 && !bIsInfinite) //Throw the weapon and make it do contact damage if there is absolutely no more ammo to spare
        {
            //TODO: Make weapon a projectile. I'm lazy rn. Bout to work on the spawner.

            return;
        }

        if (magazineAmmo == maxAmmoClip) return; //Return if the ammo is already full

        else
        {
            reloadQueueTime = Time.time;

            reloading = !reloading; //Flip the reloading state

            if (bUpdateUI)
            {
                ammoText.text = "Reloading";
            }
        }
    }

    //Function for picking up ammo
    public void AddAmmo(int inAmmoCount)
    {
        remainingAmmo += inAmmoCount;

        if(!reloading) ammoText.text = $"{magazineAmmo}/{remainingAmmo}";
    }
}
