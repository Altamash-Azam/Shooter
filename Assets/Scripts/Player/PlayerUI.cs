using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private TextMeshProUGUI healthText;

    public void UpdateText(string promptmessage){
        promptText.text = promptmessage;
    }

    public void UpdateHealthText(string health){
        healthText.text = health + "/100";
    }
}
