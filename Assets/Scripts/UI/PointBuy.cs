using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointBuy : MonoBehaviour
{
    public int points;
    private TMP_Text pointsText;
    // Start is called before the first frame update
    void Start()
    {
        pointsText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = points.ToString();
    }
}
