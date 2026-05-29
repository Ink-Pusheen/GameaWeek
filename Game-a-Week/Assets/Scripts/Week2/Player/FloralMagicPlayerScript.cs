using UnityEngine;

public class FloralMagicPlayerScript : MonoBehaviour
{
    public static FloralMagicPlayerScript instance;

    [SerializeField] ClickInteractable selectedInteractable; //What the currently selected item is

    [SerializeField] GameObject HeldItemSpot; //Where to held item will be displayed

    public GameObject CameraObj; //Reference to the camera object

    private void Awake()
    {
        instance = this;
    }

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

        if (newInteractable != null)
        {
            selectedInteractable.gameObject.transform.position = HeldItemSpot.transform.position;
            selectedInteractable.gameObject.transform.rotation = HeldItemSpot.transform.rotation;
            selectedInteractable.transform.SetParent(HeldItemSpot.transform, true);

            selectedInteractable.GetComponent<Collider>().enabled = false;
        }
    }

    public ClickInteractable ReturnCurrentInteractable()
    {
        return selectedInteractable;
    }
}
