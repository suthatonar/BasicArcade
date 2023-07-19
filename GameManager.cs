using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] float Score;

    [Header("Settings")]
    [SerializeField] float TimeSet;
    private float time;
    public bool GameStart;

    [Header("Recipe")]
    [HideInInspector]
    public Recipe mainRecipe;

    [Tooltip("Set Recipe")]
    public List<Recipe> RecipeList = new List<Recipe>();

    [Header("Mouse Drag")]
    [Tooltip("height from the floor")]
    public float objectHeight;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI TimeCount;
    [SerializeField] TextMeshProUGUI Status;
    [SerializeField] TextMeshProUGUI ScoreCount;
    [SerializeField] Image[] ingredientImage;
    [SerializeField] GameObject StartPopup;
    [SerializeField] GameObject GameOverPopup;


    [Header("Sprite item")]
    [SerializeField] Sprite[] spriteItemList;

    public static GameManager instance;

    // Start is called before the first frame update
    void Start() => instance = this;
    
    // Update is called once per frame
    void FixedUpdate() => Timer();
    
    public void StartButton()
    {
        RandomRecipe();

        time = TimeSet;
        GameStart = true;
        StartPopup.SetActive(false);
    }

    public void AddScore(int score)
    {
        ScoreCount.text = "SCORE: " + (Score += score).ToString("F0");
        StartCoroutine(AlertStatus(Color.green, "Mix successfully!, move on"));
    }

    public void WrongRecipe() => StartCoroutine(AlertStatus(Color.red,"Wrong Recipe!"));

    IEnumerator AlertStatus(Color color,string text)
    {
        Status.text = text;
        Status.color = color;

        yield return new WaitForSeconds(3);

        Status.text = "Finish mixing the recipe before the monster eats it all!";
        Status.color = Color.white;
    }

    void Timer()
    {
        if (!GameStart) return;

        TimeCount.text = "TIME: " + (time -= Time.fixedDeltaTime).ToString("F0");
        if (time < 0)
        {
            GameOver();
            time = TimeSet;
        }
    }

    public void Restart()
    {
        GameStart = true;
        TimeCount.text = "Time: 0";
        GameOverPopup.SetActive(false);

        ItemSpawner.Instance.RemoveAll();
    }

    void GameOver()
    {
        GameStart = false;
        TimeCount.text = "Game Over";
        GameOverPopup.SetActive(true);
    }

    //random recipe ingredients
    public void RandomRecipe()
    {
        int rand = Random.Range(0, RecipeList.Count);
        mainRecipe = RecipeList[rand];
        
        foreach(Image image in ingredientImage)
            image.gameObject.SetActive(false);

        for (int i = 0; i < mainRecipe.type.Count; i++)
        {
            ingredientImage[i].gameObject.SetActive(true);
            ingredientImage[i].sprite = spriteItemList[(int)mainRecipe.type[i]];
        }

        Debug.Log("Random Recipe" + rand);
    }
}
