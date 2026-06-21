using UnityEngine;

public class IJaSMeteorSpawner : MonoBehaviour
{
    [SerializeField] GameObject meteor;

    [SerializeField] int remainingMeteors; //How many more until this deletes itself

    [SerializeField] float timeTillNextMeteor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remainingMeteors = Random.Range(10, 16); //10 - 15 meteors
        timeTillNextMeteor = Random.Range(3, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        //Reduce timer
        timeTillNextMeteor -= Time.deltaTime;

        if (timeTillNextMeteor <= 0)
        {
            //Create the meteor
            Instantiate(meteor, new Vector3(Random.Range(-6f, 6f), 15, -10.76f), Quaternion.identity);

            remainingMeteors--; //Reduce meteor cound

            if (remainingMeteors is 0) Destroy(gameObject); //Destroy the spawner if so

            else timeTillNextMeteor = Random.Range(3f, 6f); //Queue new meteor
        }
    }
}
