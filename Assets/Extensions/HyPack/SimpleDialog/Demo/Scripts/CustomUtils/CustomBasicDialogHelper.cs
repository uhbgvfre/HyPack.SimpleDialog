using UnityEngine;
using HyPack;

// Create custom dialog intermediary for project
public static class CustomBasicDialogHelper
{
    public static void ShowDialog_SayHi()
    {
        var d = SimpleDialog.GetDialogInstance<BasicDialog>("", "Hi");
        d.SetButtonQty(0);
        d.useOverlayToClose = true;
        d.AddOverlayClickAction(() => Debug.Log("OverlayPanel Clicked!"));
    }

    public static void ShowDialog_SayHello()
    {
        var d = SimpleDialog.GetDialogInstance<BasicDialog>("Hello", "world~");
        d.SetButtonQty(2);
        d.SetButtonTexts("^_^", "Close");
        d.SetButtonActions(_ => Debug.Log("OuO"), _ => { Debug.Log("QwQ"); d.Close(); });
    }
}
