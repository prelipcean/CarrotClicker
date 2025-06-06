using UnityEngine;
using UnityEngine.Pool;

public class BonusParticleManager : MonoBehaviour
{
    [Header(" Elements ")]
    [Tooltip("Prefab for the bonus particle effect that appears on carrot click.")]
    [SerializeField] private GameObject bonusParticlePrefab; // Prefab for the bonus particle effect
    [Tooltip("Reference to the CarrotManager to access the carrot increment value.")]
    [SerializeField] private CarrotManager carrotManager; // Reference to the CarrotManager to access the carrot increment value

    [Header(" Pooling ")]
    private ObjectPool<GameObject> bonusParticlePool; // Object pool for managing bonus particle instances


    private void Awake()
    {
        InputManager.onCarrotClickPosition += OnCarrotClicked; // Subscribe to the carrot click action
    }

    private void Start()
    {
        bonusParticlePool = new ObjectPool<GameObject>(CreateFunciton, ActionOnGet,
            ActionOnRelease, ActionOnDestroy);
    }

    private GameObject CreateFunciton()
    {
        if (bonusParticlePrefab == null)
        {
            Debug.LogWarning("BonusParticleManager: bonusParticlePrefab is not assigned.");
            return null;
        }

        return Instantiate(bonusParticlePrefab, transform);
    }

    private void ActionOnGet(GameObject particle)
    {
        particle.SetActive(true); // Activate the particle when it is retrieved from the pool
    }

    private void ActionOnRelease(GameObject particle)
    {
        particle.SetActive(false); // Deactivate the particle when it is released back to the pool
    }

    private void ActionOnDestroy(GameObject particle)
    {
        Destroy(particle); // Destroy the particle when it is no longer needed
    }

    private void OnDestroy()
    {
        InputManager.onCarrotClickPosition -= OnCarrotClicked; // Unsubscribe from the carrot click action to prevent memory leaks
    }

    private void OnCarrotClicked(Vector2 position)
    {
        GameObject particle = bonusParticlePool.Get(); // Get a particle from the pool

        if (particle != null)
        {
            particle.transform.position = position; // Set the position of the particle to the click position

            var bonusParticle = particle.GetComponent<BonusParticle>();
            if (bonusParticle != null)
            {
                bonusParticle.Configure(carrotManager.GetCurrentMultiplier()); // Configure the bonus particle with the current multiplier
            }
            else
            {
                Debug.LogWarning("BonusParticleManager: Instantiated prefab does not have a BonusParticle component.");
            }

            LeanTween.delayedCall(0.7f, () =>
            {
                bonusParticlePool.Release(particle); // Release the particle back to the pool after 0.5 seconds
            });
            
        }

        /*
        if (bonusParticlePrefab == null)
        {
            Debug.LogWarning("BonusParticleManager: bonusParticlePrefab is not assigned.");
            return;
        }
        if (carrotManager == null)
        {
            Debug.LogWarning("BonusParticleManager: carrotManager is not assigned.");
            return;
        }

        GameObject particle = Instantiate(bonusParticlePrefab, position, Quaternion.identity, transform);

        var bonusParticle = particle.GetComponent<BonusParticle>();
        if (bonusParticle != null)
        {
            bonusParticle.Configure(carrotManager.GetCurrentMultiplier());
        }
        else
        {
            Debug.LogWarning("BonusParticleManager: Instantiated prefab does not have a BonusParticle component.");
        }

        // Destroy the instantiated particle effect after 0.5 seconds
        Destroy(particle, 0.5f); // Adjust the duration as needed
        */
    }
}
