using System.Collections;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;

    public void SetText(int health)
    {
        _healthText.SetText(health.ToString());
    }
}
