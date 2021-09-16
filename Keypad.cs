
using UdonSharp;
using UnityEngine;
using VRC.Udon;
using UnityEngine.UI;
using VRC.SDKBase;

// ReSharper disable MemberCanBeMadeStatic.Local

// ReSharper disable once CheckNamespace
public class Keypad : UdonSharpBehaviour
{

    private readonly string AUTHOR = "Foorack";
    private readonly string VERSION = "3.5";

    public string solution = "2580";
    public GameObject doorObject = null;

    public string[] allowList = new string[0];
    public string[] denyList = new string[0];
    
    public AudioSource soundDenied = null;
    public AudioSource soundGranted = null;
    public AudioSource soundButton = null;
    
    // ReSharper disable once InconsistentNaming
    public string translationPasscode = "PASSCODE";
    // ReSharper disable once InconsistentNaming
    public string translationDenied = "DENIED";
    // ReSharper disable once InconsistentNaming
    public string translationGranted = "GRANTED";

    public bool hideDoorOnGranted = true;
    public bool disableDebugging = false;

    public UdonBehaviour programClosed;
    public UdonBehaviour programDenied;
    public UdonBehaviour programGranted;

    public Text internalKeypadDisplay = null;

    public string[] additionalSolutions = new string[0];
    public GameObject[] additionalDoorObjects = new GameObject[0];

    public bool additionalKeySeparation = false;

    // Debugging
    private string _keypadId;
    private string _prefix;

    // Keypad data storage
    private string _buffer;
    private string[] _solutions;
    private GameObject[] _doors;

    #region Util Functions
    private void Log(string value)
    {
        if (disableDebugging != true)
        {
            Debug.Log(_prefix + value);
        }
    }
    private void LogWarning(string value)
    {
        if (disableDebugging != true)
        {
            Debug.LogWarning(_prefix + value);
        }
    }
    private void LogError(string value)
    {
        if (disableDebugging != true)
        {
            Debug.LogError(_prefix + value);
        }
    }
    private void Die()
    {
        // Crash.
        // ReSharper disable once PossibleNullReferenceException
        // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
        ((string)null).ToString();
    }
    #endregion Util Functions

    public void Start()
    {
        // ReSharper disable once SpecifyACultureInStringConversionExplicitly
        _keypadId = Random.value.ToString();
        _prefix = "[UdonKeypad] [K-" + _keypadId + "] ";
        // Override disableDebugging here
        Debug.Log(_prefix + "Starting Keypad... Made by @" + AUTHOR + ". Version " + VERSION + ".");

        _buffer = "";

        if (solution == null)
        {
            LogError("Solution was null! Resetting to default value!");
            solution = "2580";
        }

        if (solution.Length < 1 || solution.Length > 8)
        {
            LogError("Solution was shorter than 1 or longer than 8 in length! Resetting to default value!");
            solution = "2580";
        }

        if (doorObject == null)
        {
            LogWarning("Door object was null! Resetting to default value!");
            doorObject = gameObject;
        }

        if (internalKeypadDisplay == null)
        {
            LogError("Display is not set! This is not supported! If you do not want a display then just disable the display object. Dying...");
            Die();
        }

        if (allowList == null)
        {
            LogError("Allow list was null, setting to empty list...");
            allowList = new string[0];
        }
        if (denyList == null)
        {
            LogError("Deny list was null, setting to empty list...");
            denyList = new string[0];
        }

        if (allowList.Length > 9999)
        {
            LogError("Allow list was larger than 9999, this is most likely unintentional, resetting to 0.");
            allowList = new string[0];
        }
        if (denyList.Length > 9999)
        {
            LogError("Allow list was larger than 9999, this is most likely unintentional, resetting to 0.");
            denyList = new string[0];
        }

        if (additionalSolutions == null)
        {
            LogError("Additional Solutions list was null, setting to empty list...");
            additionalSolutions = new string[0];
        }
        if (additionalDoorObjects == null)
        {
            LogError("Additional Doors list was null, setting to empty list...");
            additionalDoorObjects = new GameObject[0];
        }

        if (additionalSolutions.Length > 9999)
        {
            LogError("Additional Solutions list was larger than 9999, this is most likely unintentional, resetting to 0.");
            additionalSolutions = new string[0];
        }
        if (additionalDoorObjects.Length > 9999)
        {
            LogError("Additional Doors list was larger than 9999, this is most likely unintentional, resetting to 0.");
            additionalDoorObjects = new GameObject[0];
        }

        if (additionalKeySeparation && additionalSolutions.Length != additionalDoorObjects.Length)
        {
            LogError("Key separation was enabled, but the number of additional solutions is not equal to the number of additional doors, " +
                "resetting to False. Please read the documentation what this setting does or contact for help.");
            additionalKeySeparation = false;
        }
        Log("Additional key separation is: " + additionalKeySeparation);

        // Merge primary solution/door with additional solutions/doors.
        // This makes coding and loops more streamlined.
        _solutions = new string[additionalSolutions.Length + 1];
        _doors = new GameObject[additionalDoorObjects.Length + 1];
        _solutions[0] = solution;
        _doors[0] = doorObject;
        for (var i = 0; i != additionalSolutions.Length; i++)
        {
            _solutions[i + 1] = additionalSolutions[i];
        }
        for (var i = 0; i != additionalDoorObjects.Length; i++)
        {
            _doors[i + 1] = additionalDoorObjects[i];
        }

        internalKeypadDisplay.text = translationPasscode;

        Log("Keypad started!");
    }

    // ReSharper disable once InconsistentNaming
    private void CLR()
    {
        Log("Passcode CLEAR!");
        internalKeypadDisplay.text = translationPasscode;
        
        foreach (var door in _doors) {
            if (door != gameObject)
            {
                door.SetActive(hideDoorOnGranted);
            }
        }

        if (programDenied != null)
        {
            programClosed.SetProgramVariable("keypadCode", _buffer);
            programClosed.SendCustomEvent("keypadClosed");
        }

        _buffer = "";
    }

    // ReSharper disable once InconsistentNaming
    private void OK()
    {
        var isOnAllow = false;
        var isOnDeny = false;
        var username = Networking.LocalPlayer == null ? "UnityEditor" : Networking.LocalPlayer.displayName;
        // Check if user is on allow list
        foreach (var entry in allowList)
        {
            if (entry == username)
            {
                isOnAllow = true;
            }
        }
        // Check if user is on deny list
        foreach (var entry in denyList)
        {
            if (entry == username)
            {
                isOnDeny = true;
            }
        }

        var isCorrect = false;
        GameObject correctDoor = null;
        for (var i = 0; i != _solutions.Length; i++)
        {
            if (_solutions[i] != _buffer) continue;
            isCorrect = true;
            if (i < _doors.Length)
            {
                correctDoor = _doors[i];
            }
        }
        // Check if pass is correct and not on deny, or if is on allow list.
        if ((isCorrect && !isOnDeny) || isOnAllow)
        {
            Log(isOnAllow ? "GRANTED through allow list!" : "Passcode GRANTED!");
            internalKeypadDisplay.text = translationGranted;
            
            foreach (var door in _doors)
            {
                if (door == gameObject) continue;
                if (additionalKeySeparation)
                {
                    if (door == correctDoor)
                    {
                        door.SetActive(!hideDoorOnGranted);                            
                    }
                    else
                    {
                        door.SetActive(hideDoorOnGranted);
                    }
                }
                else
                {
                    door.SetActive(!hideDoorOnGranted);                        
                }
            }

            if (soundGranted != null)
            {
                soundGranted.Play();
            }
            
            if (programGranted != null)
            {
                programGranted.SetProgramVariable("keypadCode", _buffer);
                programGranted.SendCustomEvent("keypadGranted");
            }

            _buffer = "";
        }
        else
        {
            // Do not announce to user that they are on deny list.
            Log("Passcode DENIED!");
            internalKeypadDisplay.text = translationDenied;

            foreach (var door in _doors)
            {
                if (door == gameObject) continue;
                door.SetActive(hideDoorOnGranted);
            }

            if (soundDenied != null)
            {
                soundDenied.Play();
            }
            
            if (programDenied != null)
            {
                programDenied.SetProgramVariable("keypadCode", _buffer);
                programDenied.SendCustomEvent("keypadDenied");
            }

            _buffer = "";
        }
    }

    private void PrintPassword()
    {
        var pass = "*";
        for (var i = 1; i < _buffer.Length; i++)
        {
            pass += " *";
        }

        internalKeypadDisplay.text = pass;
    }

    public void ButtonInput(string inputValue)
    {
        if (inputValue == "CLR")
        {
            CLR();
        }
        else if (inputValue == "OK")
        {
            OK();
        }
        else
        {
            if (_buffer.Length == 8)
            {
                Log("Limit reached!");
            }
            else
            {
                _buffer += inputValue;
                PrintPassword();
                Log("Buffer appended: " + inputValue);
                if (soundButton != null)
                {
                    soundButton.Play();
                }
            }
        }
    }

}
