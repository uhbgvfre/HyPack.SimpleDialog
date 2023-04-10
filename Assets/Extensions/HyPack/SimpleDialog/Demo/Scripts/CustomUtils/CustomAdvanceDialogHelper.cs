using System;
using UnityEngine;
using HyPack;

// Create custom dialog intermediary for project
public static class CustomAdvanceDialogHelper
{
    public static void ShowDialog_SelectColor(Action<ColorSelectorDialog.Context> oAction, Action<ColorSelectorDialog.Context> xAction)
    {
        var d = SimpleDialog.GetDialogInstance<ColorSelectorDialog>();
        d.Init("SelectColor", "", Color.red, Color.green, Color.blue);
        d.SetButtonQty(2);
        d.SetButtonTexts("O", "X");
        d.SetButtonActions(ctx =>
        {
            oAction?.Invoke((ColorSelectorDialog.Context)ctx);
            d.Close();
        }, ctx =>
        {
            xAction?.Invoke((ColorSelectorDialog.Context)ctx);
            d.Close();
        });

        d.AddOverlayClickAction(() =>
        {
            xAction?.Invoke((ColorSelectorDialog.Context)d.GetContext());
            d.Close();
        });
    }

    public static void ShowDialog_Select2Colors(Action<TwoColorSelectorDialog.Context> oAction, Action<TwoColorSelectorDialog.Context> xAction)
    {
        var d = SimpleDialog.GetDialogInstance<TwoColorSelectorDialog>();
        var colorPlatte1 = new Color[] { Color.cyan, Color.magenta, Color.yellow, Color.black };
        var colorPlatte2 = new Color[] { Color.red, Color.green, Color.blue, Color.white };
        d.Init("Select2Colors", "", colorPlatte1, colorPlatte2);
        d.SetButtonQty(2);
        d.SetButtonTexts("O", "X");
        d.SetButtonActions(ctx =>
        {
            oAction?.Invoke((TwoColorSelectorDialog.Context)ctx);
            d.Close();
        }, ctx =>
        {
            xAction?.Invoke((TwoColorSelectorDialog.Context)ctx);
            d.Close();
        });

        d.AddOverlayClickAction(() =>
        {
            xAction?.Invoke((TwoColorSelectorDialog.Context)d.GetContext());
            d.Close();
        });
    }
}
