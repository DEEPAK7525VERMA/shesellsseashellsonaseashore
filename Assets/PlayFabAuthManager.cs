using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // For TextMeshPro

public class AadhaarAuthUI : MonoBehaviour
{
    public TMP_InputField InputFieldAadhaar; // Aadhaar Input Field
    public TMP_InputField InputFieldAddress; // Address Input Field
    public Button AuthenticateButton; // Button to authenticate
    public TextMeshProUGUI ResponseText; // Response text to display messages

    // Predefined dummy data (Aadhaar numbers and corresponding addresses)
    private Dictionary<string, string> validUsers = new Dictionary<string, string>
    {
        { "123456789012", "Village A, District X" },
        { "987654321098", "Village B, District Y" },
        { "111122223333", "Village C, District Z" },
        { "444455556666", "Village D, District W" },
        { "555555555555", "Village E, District V" },
        { "666666666666", "Village F, District U" },
        { "777777777777", "Village G, District T" },
        { "798335376806", "31-A,ASHUTOSH VIHAR,SECTOR-11,Lucknow,Uttar Pradesh"},
        { "999999999999", "Village I, District R" },
        { "000000000000", "Village J, District Q" }
    };

    void Start()
    {
        // Attach the AuthenticateUser function to the button's OnClick event
        AuthenticateButton.onClick.AddListener(AuthenticateUser);
    }

    // Authenticate the user based on Aadhaar and Address
    public void AuthenticateUser()
    {
        string aadhaarNumber = InputFieldAadhaar.text.Trim();
        string address = InputFieldAddress.text.Trim();

        // Validate Aadhaar and Address against predefined data
        if (!IsValidUser(aadhaarNumber, address))
        {
            DisplayMessage("Authentication failed. Invalid Aadhaar or Address.");
            return;
        }

        // Proceed to PlayFab login and upload data
        LoginAndSaveData(aadhaarNumber, address);
    }

    // Check if the user is valid (exists in the predefined dictionary)
    private bool IsValidUser(string aadhaar, string address)
    {
        return validUsers.ContainsKey(aadhaar) && validUsers[aadhaar] == address;
    }

    // Log in to PlayFab and save Aadhaar and Address data
    private void LoginAndSaveData(string aadhaar, string address)
    {
        var loginRequest = new LoginWithCustomIDRequest
        {
            CustomId = aadhaar,
            CreateAccount = true // Create an account if one doesn't exist
        };

        PlayFabClientAPI.LoginWithCustomID(loginRequest, result =>
        {
            Debug.Log("Login successful!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            // Save the Aadhaar and Address in PlayFab User Data
            SaveUserData(aadhaar, address);

        }, error =>
        {
            Debug.LogError($"Login failed: {error.GenerateErrorReport()}");
            DisplayMessage("Login failed. Please try again.");
        });
    }

    // Save the Aadhaar and Address in PlayFab User Data
    private void SaveUserData(string aadhaar, string address)
    {
        var updateRequest = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Aadhaar", aadhaar },
                { "Address", address }
            }
        };

        PlayFabClientAPI.UpdateUserData(updateRequest, result =>
        {
            Debug.Log("User data saved successfully!");
            DisplayMessage("Authentication successful! Data saved to PlayFab.");
        }, error =>
        {
            Debug.LogError($"Failed to save user data: {error.GenerateErrorReport()}");
            DisplayMessage("Failed to save user data. Please try again.");
        });
    }

    // Display a message in the UI or Console
    private void DisplayMessage(string message)
    {
        if (ResponseText != null)
        {
            ResponseText.text = message;
        }
        Debug.Log(message);
    }
}
