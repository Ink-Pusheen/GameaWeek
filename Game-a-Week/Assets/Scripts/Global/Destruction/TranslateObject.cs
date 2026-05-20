using UnityEngine;

public class TranslateObject : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float speedZ;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedZ * Time.deltaTime);
    }
}
