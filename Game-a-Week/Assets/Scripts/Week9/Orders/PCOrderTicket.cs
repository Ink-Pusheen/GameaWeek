using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PCOrderTicket : MonoBehaviour
{
    private PCOrder setOrder; //Set order of this ticket

    [SerializeField] Image panel; //Panel bg for blinking

    [SerializeField] Image[] neededItemImages; //Image displaying the needed item
    [SerializeField] TMP_Text[] needAmounts; //Text fields to display the needed items

    [SerializeField] Image timerBar; //Timer bar to represent how much time this order has
    private float startTime; //At what time did this timer start?
    [SerializeField] float timeRemaining; //How much time does this order have remaining?

    private bool bRunTimer; //Should the timer be running?
    private bool bTicketRemoved; //Was the ticket already removed from the manager?

    [SerializeField] List<string> RequiredContents = new(); //Built list to define what the order manager will check for

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!bRunTimer) return; //Don't run if the timer isn't running

        timeRemaining -= Time.deltaTime; //Run the timer

        timerBar.fillAmount = timeRemaining / startTime; //Set the timer bar

        if(timeRemaining <= 0)
        {
            //Stop the timer
            bRunTimer = false;

            Debug.Log("Ticket Expired");
            panel.color = Color.red; //Display red before deletion

            //Subtract score
            PCOrderManager.Instance.UpdateScore(-20); //Lose 20, womp womp

            //Queue Destroy the Gameobject and remove the ticket
            QueueDestroy();
        }
    }

    public void SetupTicket(PCOrder inOrder)
    {
        setOrder = inOrder;

        for (int index = 0; index < 4; index++) //Loop of 4
        {
            if(index < inOrder.orderContent.Length) //Current loop is less than the items needed in this order
            {
                neededItemImages[index].sprite = inOrder.orderContent[index].displaySprite; //Set the sprite of the item

                if (inOrder.orderContent[index].rangedQuantity)
                {
                    int neededAmount = Random.Range(inOrder.orderContent[index].lowerRequest, inOrder.orderContent[index].upperRequest + 1); //Randomize the amount

                    needAmounts[index].text = $"x {neededAmount}"; //Display the needed amount

                    //For loop for adding the contents to the required list
                    for (int addedRequirement = 0; addedRequirement < neededAmount; addedRequirement++)
                    {
                        RequiredContents.Add(inOrder.orderContent[index].itemName);
                    }
                }
                else
                {
                    needAmounts[index].text = "x 1";

                    RequiredContents.Add(inOrder.orderContent[index].itemName);
                }
            }
            else //Current loop is out of requested items for this order
            {
                neededItemImages[index].enabled = false; //Display no sprite
                needAmounts[index].text = ""; //Display no text
            }
        }

        startTime = inOrder.deliveryTimeAllowed; //Set the initial time
        timeRemaining = startTime; //Set the timer

        bRunTimer = true; //Start the timer
    }

    //Function to check if the contents of a box match the order ticket
    public bool FulfilsTicket(PCCardboardBox inBox)
    {
        if (inBox.contents.Count != RequiredContents.Count || !bRunTimer) return false;

        int matches = 0;

        List<string> comparison = new(RequiredContents);

        for(int item = 0; item < inBox.contents.Count; item++)
        {
            if (comparison.Contains(inBox.contents[item]))
            {
                matches++;
                comparison.Remove(inBox.contents[item]); //Remove the index of the item from the comparison list
            }
        }

        if (matches == RequiredContents.Count) return true; //Return true and finish this ticket

        else return false; //Return false and don't complete this ticket
    }

    //Function to grant the rewards
    public int GrantRewards()
    {
        //Change the color of the ticket to green
        panel.color = Color.green;

        if (setOrder.hasRandomizedAmounts) //Return the variable amount of it's random, reward amount being the multiplier base
        {
            return setOrder.rewardAmount * RequiredContents.Count;
        }
        else return setOrder.rewardAmount; //Return the normal amount
    }

    //Function to queueDestroy
    public void QueueDestroy()
    {
        bRunTimer = false; //Stop the timer

        Destroy(gameObject, 1); //Destroy after a second
    }

    //Remove the ticket on destroy
    private void OnDestroy()
    {
        if (bTicketRemoved) return; //Return if the ticket was already removed

        if (PCOrderManager.Instance != null)
        {
            PCOrderManager.Instance.RemoveTicket(this);
        }
    }

    //Blink the ticket red
    public void IncorrectOrderBlink()
    {
        StartCoroutine("wrongBlink");
    }

    IEnumerator wrongBlink()
    {
        panel.color = Color.red;

        yield return new WaitForSeconds(0.15f);

        panel.color = Color.white;
    }
}
