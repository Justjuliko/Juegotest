using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLogout : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad = "Main";
    public void SignOutMethod()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        Debug.Log("Signed Out");
        SceneManager.LoadScene(sceneToLoad);
    }
}
