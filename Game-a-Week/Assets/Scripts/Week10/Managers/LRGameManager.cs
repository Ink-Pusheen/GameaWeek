using UnityEngine;

public class LRGameManager : MonoBehaviour
{
    public static LRGameManager instance;

    [SerializeField] string[] PossibleSentences; //Array of sentences that the game can randomly select from

    private void Awake()
    {
        instance = this; //Set the instance
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ReturnRandomSentence()
    {
        int index = Random.Range(0, PossibleSentences.Length);

        return PossibleSentences[index];
    }
}
