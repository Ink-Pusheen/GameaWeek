using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloralMagicRecipe", menuName = "FloralMagic/Recipe")]
public class RecipeFormulaSO : ScriptableObject
{
    //List of needed materials in the specified order
    public List<string> requiredMaterials = new();

    //Other details [Flower Sprite and Details]
    public Sprite flowerSprite;
    public int index; //Index of this plant for completion purposes
}
