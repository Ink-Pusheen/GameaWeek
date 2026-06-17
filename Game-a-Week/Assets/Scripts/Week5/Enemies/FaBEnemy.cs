using UnityEngine;

public class FaBEnemy : MonoBehaviour
{
    public GameObject target; //Target in this case being the player

    [SerializeField] float movementSpeed;

    [SerializeField] float lastFireTime; //Time.time of the last fire
    [SerializeField] float timeBetweenFire; //Time between fires

    [SerializeField] FaBWeapon weaponSystem;
    [SerializeField] float visionDistance;

    [SerializeField] GameObject ammoPack; //Droppable ammo to reload the player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponSystem = GetComponentInChildren<FaBWeapon>();

        target = FindFirstObjectByType<FaBPlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return; //Don't do anything if the target is null

        //Always move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);

        //Always rotate towards the target
        Vector3 lookRotation = target.transform.position - transform.position;
        float lookVector = Mathf.Atan2(lookRotation.y, lookRotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, lookVector);

        if (Vector3.Distance(transform.position, target.transform.position) < visionDistance && Time.time > lastFireTime + timeBetweenFire)
        {
            weaponSystem.FireWeapon();

            lastFireTime = Time.time; //Reset the timer
        }
    }

    private void OnDestroy()
    {
        if (FaBGameManager.instance == null || target == null) return;
        int Chances = Random.Range(0, 11);

        if (Chances >= 6)
        {
            Instantiate(ammoPack, transform.position, Quaternion.identity);
        }

        
        FaBGameManager.instance.GainScore();
    }
}
