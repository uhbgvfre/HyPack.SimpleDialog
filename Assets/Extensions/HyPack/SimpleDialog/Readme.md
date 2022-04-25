# SimpleDialog Guide
## Setup
1. Download SimpleDialog.unitypackage.
2. Import package to your unity project.

## Usage
1. Drag Prefab("SimpleDialogUI") to Hierarchy.
2. Make sure EventSystem is in current scene.(or create one)
3. Then you can write syntax anywhere like this:
```csharp
HyPack.SimpleDialog.ShowDialogVX("OuO", "QAQ", () => print("leftBtnCallBackMsg"), () => print("rightBtnCallBackMsg"));
```
4. Click background or any button to close

## Function List
- ShowDialog0B
  - Display title & message.
- ShowDialog1B
  - Display title & message, ok button.
- ShowDialogVX
  - Display title & message, v button & x button.
- ShowDialog1InpVX
  - Display title & message, 1 input field, v button & x button.
- ShowDialog2InpVX
  - Display title & message0 & message1, 2 input field, v button & x button.
