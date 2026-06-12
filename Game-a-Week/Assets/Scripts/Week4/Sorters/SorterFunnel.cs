using System.Collections;
using TMPro;
using UnityEngine;

public class SorterFunnel : MonoBehaviour
{
    public string CheckForIdentifier; //What string is this sorter looking for?

    [SerializeField] TMP_Text IdentifierDisplay; //Temp text for now to display what needs to be sorted

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIdentifier(string identifier)
    {
        CheckForIdentifier = identifier;

        IdentifierDisplay.text = identifier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            CardboardBox box = other.GetComponent<CardboardBox>();

            if (box.identifiers.Contains(CheckForIdentifier))
            {
                //Debug.Log("Correct, add points"); //Testing
                CCScoreManager.instance.GainPoints();
            }
            else
            {
                CCScoreManager.instance.HurtCombo();
            }

            Destroy(other.gameObject);
        }
    }

    public void DisplayMomentarySwap()
    {
        StartCoroutine("EnumSwapSorting");
    }

    IEnumerator EnumSwapSorting()
    {
        string curDisplay = IdentifierDisplay.text;

        IdentifierDisplay.text = "";

        yield return new WaitForSeconds(0.25f);

        IdentifierDisplay.text = curDisplay;

        yield return new WaitForSeconds(0.25f);

        IdentifierDisplay.text = "";

        yield return new WaitForSeconds(0.25f);

        IdentifierDisplay.text = curDisplay;

        yield return new WaitForSeconds(0.25f);

        IdentifierDisplay.text = "";

    }
}
