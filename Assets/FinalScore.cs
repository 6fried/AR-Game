using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    TMP_Text Text;

    private void OnEnable()
    {
        if (Text == null)
            Text = GetComponent<TMP_Text>();

        Text.text = gameManager.score.ToString();
    }
}
