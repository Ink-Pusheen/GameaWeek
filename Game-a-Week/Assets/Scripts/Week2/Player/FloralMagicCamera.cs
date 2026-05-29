using UnityEngine;
using UnityEngine.InputSystem;

public class FloralMagicCamera : MonoBehaviour
{
    [SerializeField] Quaternion newRotation; //What the new rotation will be
    Quaternion camRotation; //Current rotation

    [SerializeField] bool rotating;

    float newRotationY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotating)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            if (mousePos.x <= 75)
            {
                UpdateRotation(true);

                rotating = true;
            }

            if (mousePos.x >= Screen.width - 70)
            {
                UpdateRotation(false);

                rotating = true;
            }
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, 0.65f);

            if (transform.rotation == newRotation)
            {
                rotating = false;
            }
        }
    }

    void UpdateRotation(bool left)
    {
        if (left)
        {
            switch (newRotationY)
            {
                case 0:

                    newRotationY = 270;
                    newRotation = Quaternion.Euler(0, newRotationY, 0);

                    break;

                case 270:

                    newRotationY = 180;
                    newRotation = Quaternion.Euler(0, newRotationY, 0);

                    break;

                case 180:

                    newRotationY = 90;
                    newRotation = Quaternion.Euler(0, newRotationY, 0);

                    break;

                case 90:

                    newRotationY = 0;
                    newRotation = Quaternion.Euler(0, newRotationY, 0);

                    break;
            }
        }
        else
        {
            switch (newRotationY)
            {
                case 0:

                    newRotationY = 90;
                    newRotation = Quaternion.Euler(0, newRotationY, 0);

                    break;

                case 90:

                    newRotationY = 180;
                    newRotation = Quaternion.Euler(0, newRotationY, 0);

                    break;

                case 180:

                    newRotationY = 270;
                    newRotation = Quaternion.Euler(0, newRotationY, 0);

                    break;

                case 270:

                    newRotationY = 0;
                    newRotation = Quaternion.Euler(0, newRotationY, 0);

                    break;
            }
        }
    }
}
