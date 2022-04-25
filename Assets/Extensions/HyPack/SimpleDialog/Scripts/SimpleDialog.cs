using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Variable affix explain(變數詞綴註釋)
// Inp => InputField(Component)
// VX => vBtn(positive) & xBtn(negative)
// (Number)B => btn*(Number)

namespace HyPack
{
    public class SimpleDialog : MonoBehaviour
    {

        #region Singleton
        static SimpleDialog s_main;
        public void Awake()
        {
            if (s_main != null && s_main != this)
            {
                DestroyImmediate(gameObject); return;
            }
            else s_main = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion Singleton

        public Transform dialogPool;
        [Space]
        public DialogBase dialog0B; // 無可視按鈕(實際上有一個全幕範圍按鈕)
        public DialogBase dialog1B; // 單按鈕按鈕(實際上有一個全幕範圍按鈕)
        public DialogBase dialogVX; // 雙按鈕彈框(positive)(negative)
        public DialogBase dialog1InpVX; // 雙按鈕彈框(with 1 InputField)
        public DialogBase dialog2InpVX; // 雙按鈕彈框(with 2 InputField)

        ///<summary>顯示無可視按鈕彈框(實際上有一個全幕範圍按鈕)</summary>
        public static DialogBase ShowDialog0B(string title, string msg, UnityAction clickCb = null)
        {
            var dialog = GetDialogFromPool("Dialog0B", s_main.dialog0B);
            dialog.SetLebals(title, msg);
            dialog.SetButtonsCallbacks(clickCb); // 實際安排了一個全幕範圍鈕(按下關閉)
            dialog.transform.SetAsLastSibling();
            dialog.gameObject.SetActive(true);
            return (dialog);
        }

        ///<summary>顯示單按鈕彈框</summary>
        public static DialogBase ShowDialog1B(string title, string msg, UnityAction btnCB = null, UnityAction bgCB = null)
        {
            var dialog = GetDialogFromPool("Dialog1B", s_main.dialog1B);
            dialog.SetLebals(title, msg);
            dialog.SetButtonsCallbacks(btnCB, bgCB); // 實際安排了2個鈕，最後一個是全幕暗透背景
            dialog.transform.SetAsLastSibling();
            dialog.gameObject.SetActive(true);
            return (dialog);
        }

        ///<summary>顯示雙按鈕彈框(positive)&(negative)</summary>
        public static DialogBase ShowDialogVX(string title, string msg, UnityAction vCB = null, UnityAction xCB = null)
        {
            var dialog = GetDialogFromPool("DialogVX", s_main.dialogVX);
            dialog.SetLebals(title, msg);
            dialog.SetButtonsCallbacks(vCB, xCB, xCB); // 實際安排了3個鈕，最後一個是全幕暗透背景(視為取消)
            dialog.transform.SetAsLastSibling();
            dialog.gameObject.SetActive(true);
            return (dialog);
        }

        ///<summary>顯示雙按鈕彈框(with 1 InputField)</summary>
        public static DialogBase ShowDialog1InpVX(string title, string msg, UnityAction<string> vCB = null, UnityAction<string> xCB = null)
        {
            var dialog = GetDialogFromPool("Dialog1InpVX", s_main.dialog1InpVX);
            var inpFld = dialog.GetComponentInChildren<InputField>();
            inpFld.text = string.Empty;
            dialog.SetLebals(title, msg);
            dialog.SetButtonsCallbacks(() =>
            { // 實際安排了3個鈕，最後一個是全幕暗透背景(視為取消)
                if (vCB != null) vCB.Invoke(inpFld.text);
            }, () =>
            {
                if (xCB != null) xCB.Invoke(inpFld.text);
            }, () =>
            {
                if (xCB != null) xCB.Invoke(inpFld.text);
            });
            dialog.transform.SetAsLastSibling();
            dialog.gameObject.SetActive(true);
            return (dialog);
        }

        ///<summary>顯示雙按鈕彈框(with 2 InputField)</summary>
        public static DialogBase ShowDialog2InpVX(string title, string msg0, string msg1, UnityAction<string, string> vCB = null, UnityAction<string, string> xCB = null)
        {
            var dialog = GetDialogFromPool("Dialog2InpVX", s_main.dialog2InpVX);
            var inpFlds = dialog.GetComponentsInChildren<InputField>();
            inpFlds[0].text = string.Empty;
            inpFlds[1].text = string.Empty;
            dialog.SetLebals(title, msg0, msg1);
            dialog.SetButtonsCallbacks(() =>
            { // 實際安排了3個鈕，最後一個是全幕暗透背景(視為取消)
                if (vCB != null) vCB.Invoke(inpFlds[0].text, inpFlds[1].text);
            }, () =>
            {
                if (xCB != null) xCB.Invoke(inpFlds[0].text, inpFlds[1].text);
            }, () =>
            {
                if (xCB != null) xCB.Invoke(inpFlds[0].text, inpFlds[1].text);
            });
            dialog.transform.SetAsLastSibling();
            dialog.gameObject.SetActive(true);
            return (dialog);
        }

        /// <summary>嘗試從彈框實例池(dialogPool)取Dialog實例，若無則實例化defaultPrefab並回傳該實例</summary>
        static T GetDialogFromPool<T>(string name, T defaultPrefab) where T : DialogBase
        {
            // in pool and is inactive(canUse)
            foreach (Transform childs in s_main.dialogPool)
                if (childs.name == name && !childs.gameObject.activeSelf)
                    return childs.GetComponent<T>();

            // not in pool or is active(canNotUse)
            T clone = Instantiate(defaultPrefab, s_main.dialogPool);
            clone.name = name;
            return clone;
        }
    }
}
