using UnityEngine;

public class BonusParticleManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject bonusParticlePrefab; // Prefab for the bonus particle effect

    private void Awake()
    {
        InputManager.onCarrotClickPosition += OnCarrotClicked; // Subscribe to the carrot click action
    }

    private void OnDestroy()
    {
        InputManager.onCarrotClickPosition -= OnCarrotClicked; // Unsubscribe from the carrot click action to prevent memory leaks
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCarrotClicked(Vector2 position)
    {
        if (bonusParticlePrefab == null)
        {
            Debug.LogWarning("BonusParticleManager: bonusParticlePrefab is not assigned.");
            return;
        }

        GameObject particle = Instantiate(bonusParticlePrefab, position, Quaternion.identity, transform); // Instantiate the bonus particle effect at the clicked position

        // Destroy the instantiated particle effect after 0.5 seconds
        Destroy(particle, 0.5f); // Adjust the duration as needed
    }
}
