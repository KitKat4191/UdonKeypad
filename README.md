# 🔒 **Keypad Prefab made with Udon for VRChat worlds**

![VRChat Udon Keypad/Passcode](https://blog.foorack.com/content/images/2020/01/keypad2.PNG)

Feel free to join the discord if you have any bugs or questions!
<a href='https://discord.gg/7xJdWNk' target="_blank"><img alt='Discord' src='https://img.shields.io/badge/Keypad_Laboratory-100000?style=flat&logo=Discord&logoColor=FFFFFF&labelColor=5662F6&color=272935'/></a>

This is a drag-and-drop Keypad/Passcode Prefab for VRChat worlds made in Unity **2019.4.29f** and **SDK3** with **Udon**. This prefab requires no coding from your part and is very easy to setup. Password and target door are both easily configurable, with optional support for custom activation scripts if wanting more advanced activations.

___

## **📥 Download:**

**Note:** In the latest release the Keypad has been rewritten into UdonSharp. Don't worry! You don't have to touch UdonSharp code I promise!

**Install UdonSharp** through the [Creator Companion](https://vcc.docs.vrchat.com/, "https://vcc.docs.vrchat.com/").
_If you are working with a legacy project, please try [migrating the project](https://vcc.docs.vrchat.com/vpm/migrating/) to the Creator Companion._

After UdonSharp is successfully installed, download [UdonKeypad v.2021.09.16.18.43](https://storage.foorack.com/download.php?id=21&token=ZinqfqvOjuhEqEuTwJU3LCHHf4bRqB3g)

___

## **✨ Setup Tutorial**

**In Unity:** Drag the Keypad prefab into your world.

**Settings:** Look at the settings provided on the **main Keypad object:**

![Settings available in the Keypad prefab](https://blog.foorack.com/content/images/2021/04/bild.png "Settings available in the Keypad prefab")

The main focus is "Door Object" (marked in green) which accepts any GameObject and will toggle active status depending on passcode status, and "Solution" (marked in yellow) which accepts any numeric passcode up to 8 numbers long.

"Allow List" means the usernames on that list will always be allowed no matter what code they press or no code at all.

"Additional Solutions" are additional codes that will also be accepted, and will unlock all doors. "Additional Door Objects" is a way to provide if you have more than 1 door object, and you want to open them all at the same time.

"Key Separation" is a special mode which requires you to have the same amount of solutions as doors. When enabled it pairs each solution to its own unique door. This means solution 1 will open only door 1, solutions 2 will open only door 2, etc...

If you have any problems, please feel free to reach out on [Discord](https://discord.gg/7xJdWNk)! I would love to help with any _Keypad-related_ problems!

___

## **🖌️ Customisation!**

The new version of Keypad supports many customization features. For example translating the Keypad into your own language, by changing the status texts. You can also make the door show the door instead of hiding the door by de-selecting the "Hide Door On Granted" checkbox.

You can disable debugging if you know what you are doing. This will make it less verbose in console, but it is recommended to leave this on. You can also change the values of the buttons to letters, add more buttons, or change the design, they are just Unity cubes...

___

## **⚙️ Advanced: Solution Scripting**

This is optional, and only recommended for people who are interested in doing Udon programming. You should at least have watched Tupper's tutorial on cube-rotation before attempting this!

There are 3 possible programs which are run at different stages: at success, at failure, and at reset. Each program calls a custom event. An optional variable `keypadCode` will be set with the entered code on the target program.

| Setting name   | Event name      | Description                |
| -------------- |:---------------:| -------------------------- |
| programGranted | "keypadGranted" | Runs at successful code    |
| programDenied  | "keypadDenied"  | Runs at wrong code         |
| programClosed  | "keypadClosed"  | Runs at pressing Clear/CLR |

___

## **💙 Hope you enjoy it!**

You are free to use this prefab without having to credit me. But if you do use it, I would love it if you sent a quick screenshot. It really gives motivation to continuously update and improve this, as well as continue making other stuff public. Thank you!

<a href='https://discord.gg/7xJdWNk' target="_blank"><img alt='Discord' src='https://img.shields.io/badge/Keypad_Laboratory-100000?style=flat&logo=Discord&logoColor=FFFFFF&labelColor=5662F6&color=272935'/></a>
