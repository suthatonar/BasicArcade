using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData", menuName = "CreateRecipe/RecipeData", order = 1)]
public class Recipe: ScriptableObject
{
    public List<item.itemType> type = new List<item.itemType>();
}