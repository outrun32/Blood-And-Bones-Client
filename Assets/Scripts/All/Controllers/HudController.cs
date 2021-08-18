using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [SerializeField]private Image HeathImage, ManaImage;
    public bool ManaIsVizible = true;
    public bool HealthIsVizible = true;
    private float _maxHealth, _maxMana;
    
    public void Init(float maxhealth, float maxMana, float health, float mana)
    {
        _maxHealth = maxhealth;
        _maxMana = maxMana;
        if (HealthIsVizible)HeathImage.fillAmount = health / _maxHealth;
        if (ManaIsVizible)ManaImage.fillAmount = mana / _maxMana;
    }

    public void UpdateImages(float health, float mana)
    {
        if (HealthIsVizible)HeathImage.fillAmount = health / _maxHealth;
        if (ManaIsVizible)ManaImage.fillAmount = mana / _maxMana;
    }
}
