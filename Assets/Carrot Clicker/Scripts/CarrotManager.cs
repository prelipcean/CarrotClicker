using UnityEngine;
using TMPro;

public class CarrotManager : MonoBehaviour
{
    [Header (" Elements ")]
    [SerializeField] private TextMeshProUGUI carrotCountText; // UI element to display the carrot count
    [Header(" Data ")]
    ulong carrotCount = 0; // Number of carrots collected
    private ulong carrotIncrement = 1; // Amount of carrots to increment on each click
    private int frenzyModeMultiplier = 15; // Multiplier for frenzy mode, if implemented in the future

    void Awake()
    {
        LoadData(); // Load the carrot count from PlayerPrefs when the script is initialized
        InputManager.onCarrotClick  += HandleCarrotClick; // Subscribe to the carrot click action
        Carrot.onFrenzyModeStarted  += FrenzyModeStartedCallback; // Subscribe to the frenzy mode started action
        Carrot.onFrenzyModeEnded    += FrenzyModeEndedCallback; // Subscribe to the frenzy mode ended action
    }

    void OnDestroy()
    {
        // Unsubscribe from the carrot click action to prevent memory leaks
        InputManager.onCarrotClick  -= HandleCarrotClick;
        Carrot.onFrenzyModeStarted  -= FrenzyModeStartedCallback;
        Carrot.onFrenzyModeEnded    -= FrenzyModeEndedCallback;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60; // Set the target frame rate to 60 FPS
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnApplicationQuit()
    {
        SaveData();
    }

    private void HandleCarrotClick()
    {
        carrotCount += carrotIncrement; // Increment the carrot count when a carrot is clicked

        UpdateCarrotsText(); // Update the UI text to reflect the new carrot count
        //SaveData(); // Save the updated carrot count to PlayerPrefs
        DebugLogger.Log("Carrot clicked! Total carrots: " + carrotCount); // Log the current carrot count
    }

    private void UpdateCarrotsText()
    {
        if (carrotCountText != null)
        {
            carrotCountText.text = $"{carrotCount} Carrots!"; // Update the UI text to display the current carrot count

            // Requires LeanTween: https://assetstore.unity.com/packages/tools/animation/leantween-3595
            LeanTween.scale(carrotCountText.gameObject, Vector3.one * 1.2f, 0.2f) // Scale the text up to 1.2 times its size over 0.2 seconds
                .setEase(LeanTweenType.easeOutElastic) // Use an elastic easing function for the scaling effect
                .setOnComplete(() =>
                {
                    LeanTween.scale(carrotCountText.gameObject, Vector3.one, 0.2f); // Scale the text back down to its original size
                });
        }
    }

    private void FrenzyModeStartedCallback()
    {
        carrotIncrement *= (ulong)frenzyModeMultiplier; // Increase the carrot increment when frenzy mode starts
        DebugLogger.Log("Frenzy mode started! Carrot increment: " + carrotIncrement); // Log the new carrot increment
    }

    private void FrenzyModeEndedCallback()
    {
        carrotIncrement = 1;
    }

    private void SaveData()
    {
        /* Unity’s PlayerPrefs does not support unsigned 64-bit integers (ulong) directly.
         It only supports int, float, and string. */
        //PlayerPrefs.SetInt("CarrotCount", carrotCount);


        PlayerPrefs.SetString("CarrotCount", carrotCount.ToString());
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        //carrotCount = PlayerPrefs.GetInt("CarrotCount", 0);
        string saved = PlayerPrefs.GetString("CarrotCount", "0");
        ulong.TryParse(saved, out carrotCount);
        UpdateCarrotsText();
    }
}
