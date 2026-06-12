using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SorterManager : MonoBehaviour
{
    public static SorterManager instance;

    [SerializeField] SorterFunnel[] funnels;

    [SerializeField] List<string> activeIdentifiers = new(); //List of what the sorters have

    [SerializeField] List<string> sizeIdentifiers = new(); //List of possible sizes
    [SerializeField] List<string> colorIdentifiers = new(); //List of possible colors


    [SerializeField] float timeToSwap; //Time remaining before a swap
    [SerializeField] float timeBetweenSwaps; //Time between a swap
    [SerializeField] bool bSwapping; //Is it currently swapping?

    public bool bRunTimer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeToSwap = timeBetweenSwaps;

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bRunTimer) return;

        if (bSwapping) return;

        timeToSwap -= Time.deltaTime;

        if (timeToSwap <= 0)
        {
            bSwapping = true;

            foreach (var funnel in funnels)
            {
                funnel.DisplayMomentarySwap();
            }

            Invoke("ReturnTimer", 1.15f);
        }
    }

    private void ReturnTimer()
    {
        if (!bRunTimer) return; //Case that time runs out whilst trying to reorganize

        timeToSwap = timeBetweenSwaps;

        bSwapping = false;

        ChangeSorting();
    }


    public void ChangeSorting()
    {
        activeIdentifiers.Clear(); //Clear current identifiers

        // 0 - Sort by Size, 1 - Sort by Color
        int sortBy = Random.Range(0, 2);

        List<string> identifiers = new();

        if (sortBy is 0) identifiers = new(sizeIdentifiers); //Sort by size

        else if (sortBy is 1) identifiers = new(colorIdentifiers); //Sort by color

        for (int sorter = 0; sorter < funnels.Length; sorter++)
        {
            string appliedIdentifier = identifiers[Random.Range(0, identifiers.Count)];

            if (activeIdentifiers.Contains(appliedIdentifier))
            {
                sorter--;
                continue;
            }

            //Apply the appropraite info and remove/add to respective lists
            funnels[sorter].SetIdentifier(appliedIdentifier);

            identifiers.Remove(appliedIdentifier);
            activeIdentifiers.Add(appliedIdentifier);
        }
    }

    public void StopSorting()
    {
        bRunTimer = false;

        activeIdentifiers.Clear(); //Clear all identifiers

        foreach (var funnel in funnels) //Set all funnels to have no identifier
        {
            funnel.SetIdentifier("");
        }
    }

    public void StartSorter()
    {
        bRunTimer = true; //Start the timer

        ChangeSorting(); //Start by choosing what it will sort by
    }
}
