using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


public class PlayFabAuth : MonoBehaviour
{

    public InputField user;
    public InputField pass;
    public InputField email;
    public Text message;


    //Continue
    public bool IsAuthenticated = false;

    LoginWithPlayFabRequest loginRequest;

    // Start is called before the first frame update
    void Start()
    {
        email.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        loginRequest = new LoginWithPlayFabRequest();
        loginRequest.Username = user.text;
        loginRequest.Password = pass.text;

        PlayFabClientAPI.LoginWithPlayFab(loginRequest, result =>
        {
            //if the account is found
            message.text = "Welcome, " + user.text + " Connecting...";
            IsAuthenticated = true;
            Debug.Log("you are now logged in!");
        }, error =>
        {
            //if the acount is not found
            IsAuthenticated = false; 
            message.text = "Failed to login[" + error.ErrorMessage + "]";
            email.gameObject.SetActive(true);
            Debug.Log(error.ErrorMessage);
        }, null);
    }

    public void Register()
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Email = email.text;
        request.Username = user.text;
        request.Password = pass.text;

        PlayFabClientAPI.RegisterPlayFabUser(request, result => {

            message.text = "Your account has been created";

        }, error => {
            email.gameObject.SetActive(true);
            message.text = "Please enter your email"; //"Failed to create your account["+error.ErrorMessage+"]";

        });
    }
}
