using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PCOrderManager : MonoBehaviour
{
    public static PCOrderManager Instance;

    [SerializeField] PCOrder[] roundOrders; //Array of orders that can appear in this level

    [SerializeField] GameObject Ticket; //Gameobject reference to the order ticket
    [SerializeField] GameObject TicketCanvas; //Canvas that the tickets will be placed on
    [SerializeField] GameObject TicketStartPostiion; //Position where tickets will start

    [SerializeField] List<PCOrderTicket> activeOrders = new(); //List of currently active orders within the round
    [SerializeField] int maxOrders; //Max amount of orders that can be active at once

    public bool bRunTimer; //Should the timer be running?

    [SerializeField] float lastOrderTime; //Last documented time an order came in
    [SerializeField] float timeBetweenOrders; //What is the time between orders?

    [SerializeField] int gainedPoints; //How many points the player gained this round
    [SerializeField] TMP_Text pointsText; //Display how much score the player has

    private void Awake()
    {
        //Set the instance
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastOrderTime = Time.time; //Set the initial time this is to start
    }

    // Update is called once per frame
    void Update()
    {
        if(bRunTimer)
        {
            if (activeOrders.Count == maxOrders) //Don't proceed if the player has the max amount of orders
            {
                lastOrderTime = Time.time; //Reset the timer
                return;
            }

            if(Time.time > lastOrderTime + timeBetweenOrders)
            {
                AddOrder(); //Add a new order to the list

                lastOrderTime = Time.time; //Set the new latest documented time
            }
        }
    }

    //Function to check if the contents of the box match any of the requested ticket orders
    public void CompareInputOrder(PCCardboardBox inBox)
    {
        bool match = false; //Indicator if a match was found

        for (int order = 0; order < activeOrders.Count; order++)
        {
            match = activeOrders[order].FulfilsTicket(inBox);

            //If there is a match, fulfil the ticket, and remove it
            if (match)
            {
                UpdateScore(activeOrders[order].GrantRewards()); //Gain score

                activeOrders[order].QueueDestroy(); //Queue destroy the ticket

                break; //Get out of the for loop
            }
        }

        if(!match)
        {
            Debug.Log("No match, womp womp");

            //Flash all tickets wrong or play a bad sound
            foreach (var order in activeOrders)
            {
                order.IncorrectOrderBlink();
            }

        }
    }

    //Function to add an order to the active orders list
    void AddOrder()
    {
        PCOrder newOrder = roundOrders[UnityEngine.Random.Range(0, roundOrders.Length)]; //Get a random order from the round order array

        //Find the spawn position for the new ticket
        //Vector3 startPosition = new Vector3(150, -135, 0);
        Vector3 startPosition = TicketStartPostiion.transform.position;

        int offset = 0; //Offset of the starting position

        for(int order = 0; order < activeOrders.Count; order++)
        {
            offset += 500;
        }


        Vector3 offsetVector = new Vector3(offset, 0, 0);


        //Instantiate the ticket
        GameObject spawnedTicket = Instantiate(Ticket, Vector3.zero, Quaternion.identity, TicketCanvas.transform);
        spawnedTicket.transform.position = startPosition + offsetVector;
        PCOrderTicket ticket = spawnedTicket.GetComponent<PCOrderTicket>();

        if (ticket is null) throw new Exception("Where is mah ticket???");

        ticket.SetupTicket(newOrder);

        activeOrders.Add(ticket); //Add the new order
    }

    //Function to remove a specified ticket from the list
    public void RemoveTicket(PCOrderTicket inTicketToRemove)
    {
        int deadIndex = activeOrders.IndexOf(inTicketToRemove);

        activeOrders.Remove(inTicketToRemove);

        ShiftTickets(deadIndex); //Shift the tickets down
    }

    //Function to move all the tickets once one is destroyed
    void ShiftTickets(int shiftIndex)
    {
        for (int order = shiftIndex; order < activeOrders.Count; order++)
        {
            Vector3 newPosition = activeOrders[order].gameObject.transform.position - new Vector3(500, 0, 0);

            activeOrders[order].transform.position = newPosition;
        }
    }

    //Function to update the players points
    public void UpdateScore(int inChangedScore)
    {
        gainedPoints += inChangedScore; //Update the score

        //Update the score text
        UpdateScoreDisplay();
    }


    //Function to update the points text
    public void UpdateScoreDisplay()
    {
        pointsText.text = gainedPoints.ToString();
    }

    //Function to grab the score
    public int CurrentScore()
    {
        return gainedPoints;
    }


    //Public function to start all workings and reset the first ticket time
    public void StartGameFunctionality()
    {
        lastOrderTime = Time.time; //Reset the time

        bRunTimer = true; //Start the timer
    }
}
