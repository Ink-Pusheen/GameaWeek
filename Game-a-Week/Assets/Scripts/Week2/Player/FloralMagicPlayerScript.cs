using UnityEngine;

public class FloralMagicPlayerScript : MonoBehaviour
{
    [SerializeField] ClickInteractable selectedInteractable; //What the currently selected item is


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSelectedInteractable(ClickInteractable newInteractable)
    {
        selectedInteractable = newInteractable;
    }

    public ClickInteractable ReturnCurrentInteractable()
    {
        return selectedInteractable;
    }
}
