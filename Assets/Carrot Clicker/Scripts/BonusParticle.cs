using UnityEngine;
using TMPro;

public class BonusParticle : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshPro bonusText;

    public void Configure(int carrotMultiplier)
    {
        if (bonusText != null)
        {
            bonusText.text = "+" + carrotMultiplier;
            DebugLogger.Log("BonusParticle: Configured with multiplier: " + carrotMultiplier);
        }
    }
}
