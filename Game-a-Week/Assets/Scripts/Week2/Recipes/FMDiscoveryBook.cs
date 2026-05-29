using TMPro;
using UnityEngine;

public class FMDiscoveryBook : MonoBehaviour
{
    public static FMDiscoveryBook instance;

    public bool[] IndexDiscovered;
    [SerializeField] int totalDiscovered;
    [SerializeField] int totalToDiscover;

    [SerializeField] TMP_Text discoveredAmountText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        totalToDiscover = IndexDiscovered.Length;

        discoveredAmountText.text = $"{totalDiscovered}/{totalToDiscover}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IdentifyCheck(int inIndex)
    {
        bool wasDiscovered = IsIdentified(inIndex);

        if (wasDiscovered)
        {
            Debug.Log("Already Discovered");
        }
        else
        {
            UpdateIdentityBook(inIndex);
        }
    }

    bool IsIdentified(int inIndex)
    {
        return IndexDiscovered[inIndex];
    }

    private void UpdateIdentityBook(int inIndex)
    {
        IndexDiscovered[inIndex] = true;

        totalDiscovered++;

        discoveredAmountText.text = $"{totalDiscovered}/{totalToDiscover}";

        if (totalToDiscover == totalDiscovered)
        {
            discoveredAmountText.color = Color.yellow;
        }

        else discoveredAmountText.color = Color.black;
    }
}
