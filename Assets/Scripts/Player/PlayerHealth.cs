using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    
    private float health;
    private float lerpTimer;

    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 3f;
    [SerializeField] Image frontHealthBar;
    [SerializeField] Image backHealthBar;

    [Header("Health UI")]
    PlayerUI playerUI;

    [Header("Damage Overlay")]
    [SerializeField] Image overlay; // our damage overlay gameobject
    [SerializeField] float duration = 2f;//how long it will stay opaque
    [SerializeField] float fadeSpeed = 2f;//time taken to fade
    private float durationTimer = 0f;

    private void Start() {
        health = maxHealth;
        playerUI = GetComponent<PlayerUI>();
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    private void Update() {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        playerUI.UpdateHealthText(Convert.ToString(health));
        FadeOverlay();
    }

    private void FadeOverlay()
    {
        if(overlay.color.a > 0){
            if(health<30){
                return;
            }
            durationTimer += Time.deltaTime;
            if(durationTimer > duration){
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI(){
        Debug.Log(health);
        float fillA = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health/maxHealth;
        if(hFraction < fillB){
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            // percentComplete = percentComplete* percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(hFraction > fillA){
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            // percentComplete = percentComplete* percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillA, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage){
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g,overlay.color.b, 1);
    }

    public void Restorehealth(float heal){
        health += heal;
        lerpTimer = 0f;
    }
}
