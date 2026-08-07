using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PCOrderManagerPolished : MonoBehaviour
{
    public static PCOrderManagerPolished Instance;

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

    [SerializeField] GameObject[] conveyorRouteLeft; //Route boxes take going left
    [SerializeField] GameObject[] conveyorRouteRight; //Route boxes take going right

    [SerializeField] GameObject shipObject; //Object to spawn when a box is sent

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
        if (bRunTimer)
        {
            if (activeOrders.Count == maxOrders) //Don't proceed if the player has the max amount of orders
            {
                lastOrderTime = Time.time; //Reset the timer
                return;
            }

            if (Time.time > lastOrderTime + timeBetweenOrders)
            {
                AddOrder(); //Add a new order to the list

                lastOrderTime = Time.time; //Set the new latest documented time
            }
        }
    }

    //Function to check if the contents of the box match any of the requested ticket orders
    public void CompareInputOrder(PCCardboardBox inBox)
    {
        Quaternion randomBoxRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0f, 360f), 0);

        GameObject sentBox = Instantiate(shipObject, conveyorRouteLeft[0].transform.position, randomBoxRotation); //Spawn the shipment box

        PCConveyorSendout sendoutBox = sentBox.GetComponent<PCConveyorSendout>(); //Grab the sendout component

        int path = UnityEngine.Random.Range(0, 2); //Randomize which way the box goes

        if (sendoutBox is not null) //Ensure this isn't null
        {
            if (path is 0) sendoutBox.SetWaypoints(conveyorRouteLeft); //Set the path leftwards

            else if (path is 1) sendoutBox.SetWaypoints(conveyorRouteRight); //Set the path rightwards
        }

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

        if (!match)
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

        //Instantiate the ticket
        GameObject spawnedTicket = Instantiate(Ticket, Vector3.zero, Quaternion.identity, TicketCanvas.transform);

        //Set the position according to the amount of tickets that exist
        if (activeOrders.Count is 0) spawnedTicket.transform.position = TicketStartPostiion.transform.position; //Set the initial position if this is the only ticket

        else spawnedTicket.transform.position = activeOrders[activeOrders.Count - 1].newTicketPosition.transform.position;

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
            if (order is 0) activeOrders[order].gameObject.transform.position = TicketStartPostiion.transform.position; //Set to the initial ticket spawn position of it's the first ticket in line

            else activeOrders[order].gameObject.transform.position = activeOrders[order - 1].newTicketPosition.transform.position; //Set the new ticket position to the previous ticket
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


    //Public function to force remove the remaining tickets
    public void RemoveTickets()
    {
        foreach (var ticket in activeOrders)
        {
            ticket.ForceCloseTicket();
        }
    }
}
