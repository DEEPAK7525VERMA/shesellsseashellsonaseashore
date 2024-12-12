using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabAuthManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        // Handle successful registration here (e.g., move to another scene)
        Debug.Log("Registration Successful!");
    }

    private void OnError(PlayFabError error)
    {
        // Handle errors here (e.g., show a message or log it)
        Debug.LogError("Error: " + error.GenerateErrorReport());
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        // Handle successful login here (e.g., move to another scene)
        Debug.Log("Login Successful!");
    }

    // Optional: If you want to add Reset Password functionality
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordResetSuccess, OnError);
    }

    private void OnPasswordResetSuccess(SendAccountRecoveryEmailResult result)
    {
        // Handle password reset success
        Debug.Log("Password reset email sent!");
    }
}
