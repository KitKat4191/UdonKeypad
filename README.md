
<div align=center>
  <a href="https://github.com/KitKat4191/UdonKeypad/releases/latest/"><img alt="GitHub release (latest by date)" src="https://img.shields.io/github/v/release/KitKat4191/UdonKeypad?logo=unity&style=for-the-badge"></a>
  <a href="https://github.com/KitKat4191/UdonKeypad/releases/latest/"><img alt="GitHub all releases" src="https://img.shields.io/github/downloads/KitKat4191/UdonKeypad/total?color=blue&style=for-the-badge"></a>
</div>

<h2 align="center">🔒 Keypad Prefab made with Udon for VRChat worlds 🔒</h2>

<img src="https://raw.githubusercontent.com/KitKat4191/UdonKeypad/main/Resources/PhysicalKeypad.png" alt="VRChat Udon Keypad/Passcode" width="400"/>

___

<a href='https://discord.gg/7xJdWNk' target="_blank"><img alt='Discord' src='https://img.shields.io/badge/Keypad_Laboratory-100000?style=flat&logo=Discord&logoColor=FFFFFF&labelColor=5662F6&color=272935' width="400"/></a>

___

This is a drag-and-drop Keypad / Passcode Prefab for VRChat worlds made in Unity **2019.4.29f** for **SDK3** with **Udon**. Using this prefab requires no coding from your part and is very easy to setup. Password and target door are both easily configurable. The keypad also has a small API for more advanced users, which can be used alongside custom scripting if more advanced behaviour is desired. The API documentation can be found below.

___

## **📥 Download:**

**Note:** _If you are working with a legacy project, please [migrate the project](https://vcc.docs.vrchat.com/vpm/migrating/) to the Creator Companion._
Download the Keypad `.unitypackage` from the [latest release](https://github.com/KitKat4191/UdonKeypad/releases/latest).

___

## **✨ Setup Tutorial**

**In Unity:** Drag the Keypad prefab into your world.
The prefab can be found in `Assets\Resources\Foorack\Keypad`.

### Settings

This is the `Keypad` script located on the Keypad prefab object.

![Settings available in the Keypad prefab](https://raw.githubusercontent.com/KitKat4191/UdonKeypad/main/Resources/AvailableSettings.png "Settings available in the Keypad prefab")

### Settings Documentation

* `Door Object` accepts any _GameObject_. The linked object will be set active or inactive depending on if the entered passcode was right or wrong.

* `Solution` is where you specify what the passcode / password will be. It accepts any numeric code up to 8 digits.

* `Allow List` is an optional list of usernames that will always be allowed no matter what code they enter.

* `Hide Door On Granted` When the correct passcode is entered it will either hide or show the door object.

* `Disable Debugging` will make the keypad less verbose in the console. Feel free to disable it if you want!

* `Additional Solutions` are additional codes that will also be accepted. By default all the codes will unlock all the doors. If you have more than one door object you can link them in `Additional Door Objects`.

* `Key Separation` is an alternative mode which requires you to have the same amount of solutions as doors. When enabled it pairs each solution to its own unique door. This means solution 1 will only open door 1, solution nr. 2 will open door 2, etc.
`Solution` pairs with `DoorObject` in this mode.

If you have any problems, please feel free to reach out on [Discord](https://discord.gg/7xJdWNk)! I'm always willing to help with any _Keypad-related_ issues!

___

## **🖌️ Customization!**

This Keypad supports many customization features. For example translating the Keypad into your own language, by changing the status texts. You can also change the values of the buttons to letters, add more buttons (make a whole keyboard if you want!), change the design, etc. they're just Unity cubes after all. People have made some really pretty keypads over the years! You can see pictures of them in the discord server.

___

## **⚙️ API Documentation**

This is optional, and only recommended for people who are interested in doing Udon programming.

There are three callbacks which are sent via `SendCustomEvent` to the `UdonBehaviour` referenced in the fields `Program Closed`, `Program Denied`, and `Program Granted`. If you wish to listen to all the callbacks on one `UdonBehaviour` you'll need to link it in all three slots.

You can optionally declare a string variable named `keypadCode`. This will be set via `SetProgramVariable` and contains the code that was entered by the user.

| UdonBehaviour  |   Event Name    | Description                |
| -------------- | :-------------: | -------------------------- |
| programGranted | "keypadGranted" | Runs at correct code       |
| programDenied  | "keypadDenied"  | Runs at wrong code         |
| programClosed  | "keypadClosed"  | Runs at pressing Clear/CLR |

___

## **💙 Hope you enjoy it!**

Feel free to use this prefab without crediting either me (KitKat) or Foorack (the original creator). But if you do use it, I would love it if you sent a picture of it in use in the discord server! It really gives me motivation to update and improve this prefab, as well as continue making other stuff. Thank you!

<a href='https://discord.gg/7xJdWNk' target="_blank"><img alt='Discord' src='https://img.shields.io/badge/Keypad_Laboratory-100000?style=flat&logo=Discord&logoColor=FFFFFF&labelColor=5662F6&color=272935'/></a>
