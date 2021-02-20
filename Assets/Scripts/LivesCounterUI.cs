using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LivesCounterUI : MonoBehaviour
{
    [SerializeField] private Text livesText;

    private void Awake()
    {
        livesText = GetComponent<Text>();
    }

    private void Update()
    {
        livesText.text = "LIVES: " + GameMaster.RemainingLives;
    }
}
