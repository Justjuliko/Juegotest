using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class ButtonResetPassword : MonoBehaviour
{
    [SerializeField]
    private Button resetPasswordButton;
    [SerializeField]
    private TMP_InputField _emailInputField;
    private void Reset()
    {
        resetPasswordButton = GetComponent<Button>();
        _emailInputField = GameObject.Find("InputFieldEmail").GetComponent<TMP_InputField>();
    }
    void Start()
    {
        resetPasswordButton.onClick.AddListener(HandleResetPasswordButton);
    }

    private void HandleResetPasswordButton()
    {
        string emailAddress = _emailInputField.text;
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SendPasswordResetEmailAsync(emailAddress).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("Password reset email sent successfully.");
            });
        }

    }
}
