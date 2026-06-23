using UnityEngine;

public class IJaSAttackManager : MonoBehaviour
{
    public static IJaSAttackManager instance;

    [SerializeField] IJaSTypewriter typeWriter;

    [SerializeField] GameObject pObject; //Reference to the player game object
    [SerializeField] IJaSHealth pHealth; //Reference to the Player Health

    [SerializeField] GameObject sObject; //Reference to the slime game object
    [SerializeField] IJaSHealth sHealth; //Reference to the Slime Health

    public bool bInBattle; //A public bool for people to stop taking damage once one has perished

    [Header("Instantiatables")]

    [SerializeField] GameObject crownObject;
    [SerializeField] GameObject potObject;

    [SerializeField] GameObject meteorSpawner;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        typeWriter = IJaSTypewriter.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QueueRandomAction()
    {
        typeWriter.queuedDialogue.Add("Player used the wildcard");

        SceneSwapper.instance.SwapState("TypeWriter");

        typeWriter.DisplayDialogue();
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
         * 8 - Nothing
         */

        int RandomAttack = Random.Range(0, 9); //Upper bounds being the final number in attacks + 1

        int target = Random.Range(0, 2); //0 - Player, 1 - Slime

        switch (RandomAttack)
        {
            case 0: //Fire
                Debug.Log("Fire Attack");
                IJaSBurnEffect appliedBurn = new();
                appliedBurn.SetupStatus();

                if(target is 0) //Target is Player
                {
                    pHealth.AddStatus(appliedBurn);

                    typeWriter.queuedDialogue.Add("Player inflicted fire on themselves");
                }
                else if(target is 1) //Target is Slime
                {
                    sHealth.AddStatus(appliedBurn);

                    typeWriter.queuedDialogue.Add("Player inflicted fire the slime");
                }

                IJaSTypewriter.Instance.DisplayDialogue();

                break;

            case 1: //Blizzard
                Debug.Log("Blizzard Attack");
                //Clear all Fire

                typeWriter.queuedDialogue.Add($"It got colder...?");
                typeWriter.queuedDialogue.Add("All fire effects removed!");

                IJaSBurnEffect burn = new();
                burn.SetupStatus();

                pHealth.RemoveStatus("Burn");
                sHealth.RemoveStatus("Burn");

                IJaSTypewriter.Instance.DisplayDialogue();

                break;

            case 2: //Falling pot
                Debug.Log("Falling Pot");
                GameObject spawnedPot = Instantiate(potObject, new Vector3(0, 15, 0), Quaternion.identity);

                if (target is 0) //Target is Player
                {
                    spawnedPot.transform.position = pObject.transform.position + new Vector3(0, 9, 0);
                    typeWriter.queuedDialogue.Add("A falling pot appeared above the player!");
                }
                else if (target is 1) //Target is Slime
                {
                    spawnedPot.transform.position = sObject.transform.position + new Vector3(0, 9, 0);
                    typeWriter.queuedDialogue.Add("A falling pot appeared above the slime!");
                }

                break;

            case 3: //Falling Crown
                Debug.Log("Falling Crown");
                GameObject spawnedCrown = Instantiate(crownObject, new Vector3(0, 15, 0), Quaternion.identity);

                if (target is 0) //Target is Player
                {
                    spawnedCrown.transform.position = pObject.transform.position + new Vector3(0, 9, 0);
                    typeWriter.queuedDialogue.Add("A falling crown appeared above the player!");
                }
                else if (target is 1) //Target is Slime
                {
                    spawnedCrown.transform.position = sObject.transform.position + new Vector3(0, 9, 0);
                    typeWriter.queuedDialogue.Add("A falling crown appeared above the slime!");
                }

                break;

            case 4: //Poison
                Debug.Log("Poison");
                IJaSPoisonEffect appliedPoison = new();
                appliedPoison.SetupStatus();

                if (target is 0)
                {
                    pHealth.AddStatus(appliedPoison); //Target is Player

                    typeWriter.queuedDialogue.Add("Player poisoned themselves");
                }

                else if (target is 1)
                {
                    sHealth.AddStatus(appliedPoison); //Target is Slime

                    typeWriter.queuedDialogue.Add("Player poisoned the slime");
                }

                IJaSTypewriter.Instance.DisplayDialogue();

                break;

            case 5: //Meteor Rain
                Debug.Log("Meteor Rain");

                typeWriter.queuedDialogue.Add($"Meteors can be seen falling in the distance...");
                IJaSTypewriter.Instance.DisplayDialogue();

                Instantiate(meteorSpawner); //Create the meteorspawner

                break;

            case 6: //Heal
                Debug.Log("Heal");

                int healAmount = Random.Range(10, 16);

                if (target is 0)
                {
                    pHealth.Heal(healAmount); //Target is Player

                    typeWriter.queuedDialogue.Add($"Player healed themselves for {healAmount}!");
                }

                else if (target is 1)
                {
                    sHealth.Heal(healAmount); //Target is Slime

                    typeWriter.queuedDialogue.Add($"Player healed the slime for {healAmount}!");
                }

                IJaSTypewriter.Instance.DisplayDialogue();

                break;

            case 7: //Max Heal
                Debug.Log("Max Heal");

                if (target is 0)
                {
                    pHealth.Heal(999); //Target is Player

                    typeWriter.queuedDialogue.Add("Player max healed themselves!");
                }

                else if (target is 1)
                {
                    sHealth.Heal(999); //Target is Slime

                    typeWriter.queuedDialogue.Add("Player max healed the slime!");
                }

                IJaSTypewriter.Instance.DisplayDialogue();

                break;

            case 8: //Nothing

                typeWriter.queuedDialogue.Add("Nothing happened...");

                IJaSTypewriter.Instance.DisplayDialogue();

                break;
        }
    }

    //Function to pause the application to let text or other matters display before going to next ronud
    private void QueueNextRound()
    {

    }

    public void DisplayStatusEffects()
    {
        //Run the status list of both Player and Slime
        pHealth.RunStatuses();

        sHealth.RunStatuses();

        typeWriter.DisplayDialogue(); //Display what happened
    }

    public void SetBattleState(bool InBattle)
    {
        bInBattle = InBattle;
    }
}
