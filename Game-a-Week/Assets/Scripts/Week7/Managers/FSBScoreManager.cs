using TMPro;
using UnityEngine;

public class FSBScoreManager : MonoBehaviour
{
    public static FSBScoreManager instance;

    [SerializeField] int PositiveReviews; //Amount of positive reviews
    [SerializeField] TMP_Text PositiveReviewsText; //Display of positive reviews

    [SerializeField] int NegativeReviews; //Amount of negative reviews
    [SerializeField] TMP_Text NegativeReviewsText; //Display of negative reviews

    private void Awake()
    {
        instance = this;
    }

    //Function to update the display of the board
    private void UpdateStatisticsBoard()
    {
        PositiveReviewsText.text = $"{PositiveReviews}";

        NegativeReviewsText.text = $"{NegativeReviews}";
    }

    //Increment satisfactory orders and the board
    public void IncrementPositiveReviews()
    {
        PositiveReviews++; //Increment positive reviews

        UpdateStatisticsBoard(); //Update the board
    }

    //Increment unsatisfactory orders and the board
    public void IncrementNegativeReviews()
    {
        NegativeReviews++; //Increment negative reviews

        UpdateStatisticsBoard(); //Update the board
    }
}
