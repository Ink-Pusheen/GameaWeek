using UnityEngine;

public class IJaSAttackManager : MonoBehaviour
{
    [SerializeField] GameObject pObject; //Reference to the player game object
    [SerializeField] IJaSHealth pHealth; //Reference to the Player Health

    [SerializeField] GameObject sObject; //Reference to the slime game object
    [SerializeField] IJaSHealth sHealth; //Reference to the Slime Health

    [Header("Instantiatables")]

    [SerializeField] GameObject crownObject;
    [SerializeField] GameObject potObject;

    [SerializeField] GameObject meteor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Select a random attack to enact
    public void RandomAttack()
    {
        /*
         * Attacks
         * 0 - Fire
         * 1 - Blizzard
         * 2 - Falling pot
         * 3 - Falling Crown
         * 4 - Poison
         * 5 - Meteor Rain
         * 6 - Heal
         * 7 - Max Heal
         */

        int RandomAttack = Random.Range(0, 8); //Upper bounds being the final number in attacks + 1

        int target = Random.Range(0, 2); //0 - Player, 1 - Slime

        switch (RandomAttack)
        {
            case 0: //Fire
                Debug.Log("Fire Attack");
                IJaSBurnEffect appliedBurn = new();

                if(target is 0) //Target is Player
                {
                    pHealth.AddStatus(appliedBurn);
                }
                else if(target is 1) //Target is Slime
                {
                    sHealth.AddStatus(appliedBurn);
                }

                    break;

            case 1: //Blizzard
                Debug.Log("Blizzard Attack");
                //Damage all

                break;

            case 2: //Falling pot
                Debug.Log("Falling Pot");
                GameObject spawnedPot = Instantiate(potObject, new Vector3(0, 15, 0), Quaternion.identity);

                if (target is 0) //Target is Player
                {
                    spawnedPot.transform.position = pObject.transform.position + new Vector3(0, 9, 0);
                }
                else if (target is 1) //Target is Slime
                {
                    spawnedPot.transform.position = sObject.transform.position + new Vector3(0, 9, 0);
                }

                break;

            case 3: //Falling Crown
                Debug.Log("Falling Crown");
                GameObject spawnedCrown = Instantiate(crownObject, new Vector3(0, 15, 0), Quaternion.identity);

                if (target is 0) //Target is Player
                {
                    spawnedCrown.transform.position = pObject.transform.position + new Vector3(0, 9, 0);
                }
                else if (target is 1) //Target is Slime
                {
                    spawnedCrown.transform.position = sObject.transform.position + new Vector3(0, 9, 0);
                }

                break;

            case 4: //Poison
                Debug.Log("Poison");
                IJaSPoisonEffect appliedPoison = new();

                if (target is 0) pHealth.AddStatus(appliedPoison); //Target is Player

                else if (target is 1) sHealth.AddStatus(appliedPoison); //Target is Slime

                break;

            case 5: //Meteor Rain
                Debug.Log("Meteor Rain");

                break;

            case 6: //Heal
                Debug.Log("Heal");

                int healAmount = Random.Range(10, 16);

                if (target is 0) pHealth.Heal(healAmount); //Target is Player

                else if (target is 1) sHealth.Heal(healAmount); //Target is Slime

                break;

            case 7: //Max Heal
                Debug.Log("Max Heal");

                if (target is 0) pHealth.Heal(999); //Target is Player

                else if (target is 1) sHealth.Heal(999); //Target is Slime

                break;
        }
    }

    //Function to pause the application to let text or other matters display before going to next ronud
    private void QueueNextRound()
    {

    }

    public void NextRound()
    {
        //Run the status list of both Player and Slime
        pHealth.RunStatuses();

        sHealth.RunStatuses();
    }
}
