using UnityEngine;

public class ShipBlasters : MonoBehaviour
{
    public BlasterSO activeBlaster; //Scriptable object for player sakes, can be applied to enemies too

    [SerializeField] AudioPlayer audPlayer;

    public GameObject outProjectile;

    public GameObject[] fireSpots;

    public float timePerShot;
    private float lastFireTime;

    public string targetIdentity;

    public bool canFire = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (activeBlaster != null)
        {
            outProjectile = activeBlaster.bulletPrefab;

            timePerShot = activeBlaster.fireDelay;
        }

        audPlayer = GetComponentInChildren<AudioPlayer>();
    }

    public void ApplyNewBlaster()
    {
        outProjectile = activeBlaster.bulletPrefab;

        timePerShot = activeBlaster.fireDelay;
    }

    public void Fire()
    {
        if (Time.time > lastFireTime + timePerShot && canFire)
        {
            lastFireTime = Time.time;

            //Instantiate the bullets in their respective spots
            for (int fireSpot = 0; fireSpot < fireSpots.Length; fireSpot++)
            {
                GameObject SpawnedProjectile = outProjectile;

                if (SpawnedProjectile is null) return;

                SpawnedProjectile.GetComponent<ProjectileBase>().Target = targetIdentity;

                Instantiate(SpawnedProjectile, fireSpots[fireSpot].transform.position, fireSpots[fireSpot].transform.rotation);
            }

            if (audPlayer != null) audPlayer.PlayRandomSound(0); //Play random fire sound
        }
    }
}
