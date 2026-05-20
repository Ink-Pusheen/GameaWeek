using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float destroyTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
