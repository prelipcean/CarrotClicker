using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Carrot : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform carrotRendererTransform; // Transform of the carrot's visual representation
    [SerializeField] private Image fillImage;

    [Header(" Settings ")]
    [SerializeField] private float fillRate;
    private bool isFrenzyModeActive = false; // Flag to check if frenzy mode is active

    [Header(" Actions ")]
    public static Action onFrenzyModeStarted;
    public static Action onFrenzyModeEnded;


    private void Awake()
    {
        InputManager.onCarrotClick += OnCarrotClicked; // Subscribe to the carrot click action       
    }

    private void OnDestroy()
    {
        InputManager.onCarrotClick -= OnCarrotClicked; // Subscribe to the carrot click action       
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCarrotClicked()
    {
        DebugLogger.Log("Carrot clicked: " + gameObject.name); // Log the name of the carrot that was clicked

        Animate(); // Call the method to animate the carrot

        if (!isFrenzyModeActive)
        {
            // Fill the carrot image
            FillCarrotImage();
        }
    }

    private void Animate()
    {
        if (carrotRendererTransform == null) return;
        carrotRendererTransform.localScale = Vector3.one * 0.8f;
        LeanTween.cancel(carrotRendererTransform.gameObject);
        LeanTween.scale(carrotRendererTransform.gameObject, Vector3.one * 0.7f, 0.15f).setLoopPingPong(1);
    }

    private void FillCarrotImage()
    {
        if (fillImage == null) return;
        fillImage.fillAmount += fillRate;
        if (fillImage.fillAmount >= 1f)
        {
            StartFrenzyMode();
        }
    }
    
    private void StartFrenzyMode()
    {
        isFrenzyModeActive = true;
        onFrenzyModeStarted?.Invoke(); // Call once at the start
        LeanTween.value(1, 0, 5).setOnUpdate((float value) =>
        {
            fillImage.fillAmount = value;
        }).setOnComplete(() =>
        {
            isFrenzyModeActive = false;
            fillImage.fillAmount = 0;
            onFrenzyModeEnded?.Invoke();
            DebugLogger.Log("Frenzy mode ended for carrot: " + gameObject.name);
        });
    }
}
