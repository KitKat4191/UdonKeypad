
using UdonSharp;
using UnityEngine;
using VRC.Udon;
using UnityEngine.UI;
// ReSharper disable MemberCanBeMadeStatic.Local

// ReSharper disable once CheckNamespace
public class Keypad : UdonSharpBehaviour
{

    private readonly string AUTHOR = "Foorack";
    private readonly string VERSION = "3.1";
    
    public string solution = "2580";
    public GameObject doorObject = null;

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

    // Debugging
    private string _keypadId;
    private string _prefix;
    
    // Keypad data storage
    private string _buffer;
    
    #region Util Functions
    private void Log(string value)
    {
        if (disableDebugging != true)
        {
            Debug.Log(_prefix + value);
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
        ((string) null).ToString();
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
            LogError("Door object was null! Resetting to default value!");
            doorObject = gameObject;
        }

        if (internalKeypadDisplay == null)
        {
            LogError("Display is not set! This is not supported! If you do not want a display then just disable the display object. Dying...");
            Die();
        }

        internalKeypadDisplay.text = translationPasscode;

        Log("Keypad started!");
    }

    // ReSharper disable once InconsistentNaming
    private void CLR()
    {
        Log("Passcode CLEAR!");
        _buffer = "";
        internalKeypadDisplay.text = translationPasscode;
        
        if (doorObject != gameObject)
        {
            doorObject.SetActive(hideDoorOnGranted);
        }
        
        if (programDenied != null)
        {
            programClosed.SendCustomEvent("keypadClosed");
        }
    }

    // ReSharper disable once InconsistentNaming
    private void OK()
    {
        if (_buffer == solution)
        {
            Log("Passcode GRANTED!");
            _buffer = "";
            internalKeypadDisplay.text = translationGranted;
            
            if (doorObject != gameObject)
            {
                doorObject.SetActive(!hideDoorOnGranted);
            }

            if (programGranted != null)
            {
                programGranted.SendCustomEvent("keypadGranted");
            }
        }
        else
        {
            Log("Passcode DENIED!");
            _buffer = "";
            internalKeypadDisplay.text = translationDenied;
            
            if (doorObject != gameObject)
            {
                doorObject.SetActive(hideDoorOnGranted);
            }
            
            if (programDenied != null)
            {
                programDenied.SendCustomEvent("keypadDenied");
            }
        }
    }

    private void PrintPassword()
    {
        string pass = "*";
        for (int i = 1; i < _buffer.Length; i++)
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
        } else if (inputValue == "OK")
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
            }
        }
    }
    
}
