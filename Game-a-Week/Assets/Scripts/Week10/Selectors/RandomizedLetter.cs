using TMPro;
using UnityEngine;

public class RandomizedLetter : MonoBehaviour
{
    [SerializeField] TMP_Text letterDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Public function to set the letter of the display
    public void SetLetter(string inLetter)
    {
        letterDisplay.text = inLetter;
    }
}
