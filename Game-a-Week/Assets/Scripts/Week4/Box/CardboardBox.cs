using System.Collections.Generic;
using UnityEngine;

public class CardboardBox : MonoBehaviour
{
    [SerializeField] Material[] boxColors;

    public List<string> identifiers = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeBox(); //Randomize box elements   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomizeBox()
    {
        int randomColor = Random.Range(0, 3);
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        Material[] curMaterials = renderer.materials;

        switch (randomColor)
        {
            case 0: //Case red

                curMaterials[0] = boxColors[randomColor];
                identifiers.Add("Red");

                break;

            case 1: //Case green

                curMaterials[0] = boxColors[randomColor];
                identifiers.Add("Green");

                break;

            case 2: //Case blue

                curMaterials[0] = boxColors[randomColor];
                identifiers.Add("Blue");

                break;
        }

        renderer.materials = curMaterials;
    }
}
