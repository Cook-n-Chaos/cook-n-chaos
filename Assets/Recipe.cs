using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    public float initialTime = 60f;
    public Slider timer;

    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI pointsLabel;

    public List<GameObject> ingredients = new();

    // Start is called before the first frame update
    void Start()
    {
        SetupTimer();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    void SetupTimer()
    {
        timer.maxValue = initialTime;
        timer.value = initialTime;
    }

    void UpdateTimer()
    {
        initialTime -= Time.deltaTime;
        if (initialTime < 0) {
            Destroy(gameObject);
        } else {
            timer.value = initialTime;
        }
    }

    public void SetTime(float time)
    {
        initialTime = time > 0 ? time : initialTime;
        SetupTimer();
    }

    public void SetLabel(string label)
    {
        nameLabel.text = label;
    }

    public void SetPoints(int points)
    {
        pointsLabel.text = points.ToString();
    }

    public void SetIngredients(List<GameObject> ingreds)
    {
        for (int i = 0; i < ingreds.Count; i++)
        {
            ingredients[i].SetActive(true);
            var parent = ingredients[i].transform.GetChild(0);
            Instantiate(ingreds[i], parent);
        }
    }
}
