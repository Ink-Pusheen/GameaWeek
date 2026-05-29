using UnityEngine;

public class PlayerTeleportInteraction : ClickInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interaction(ClickInteractable inPreviousInteractable)
    {
        FloralMagicPlayerScript.instance.CameraObj.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
    }
}
