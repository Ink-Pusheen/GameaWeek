using UnityEngine;

public class ShipBlasters : MonoBehaviour
{
    public GameObject outProjectile;

    public GameObject[] fireSpots;

    public float timePerShot;
    private float lastFireTime;

    public string targetIdentity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Fire()
    {
        if (Time.time > lastFireTime + timePerShot)
        {
            Debug.Log("Fire");

            lastFireTime = Time.time;

            //Instantiate the bullets in their respective spots
            for (int fireSpot = 0; fireSpot < fireSpots.Length; fireSpot++)
            {
                GameObject SpawnedProjectile = outProjectile;

                if (SpawnedProjectile is null) return;

                SpawnedProjectile.GetComponent<ProjectileBase>().Target = targetIdentity;

                Instantiate(SpawnedProjectile, fireSpots[fireSpot].transform.position, fireSpots[fireSpot].transform.rotation);
            }
        }
    }
}
