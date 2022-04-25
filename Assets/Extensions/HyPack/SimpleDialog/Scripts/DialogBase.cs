using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HyPack {

    public class DialogBase : MonoBehaviour
    {
        public Text[] labels;
        public Button[] btns;

        public DialogBase SetLebals(params string[] txts)
        {
            ClearAllLabelsTxts();
            for (int i = 0; i < labels.Length; i++)
            {
                if (labels[i] == null || string.IsNullOrEmpty(txts[i])) continue;
                labels[i].text = txts[i];
            }
            return this;
        }

        public DialogBase SetButtonsCallbacks(params UnityAction[] callbacks)
        {
            RemoveAllCBs();
            for (int i = 0; i < btns.Length; i++)
            {
                if (btns[i] == null || callbacks[i] == null) continue;
                btns[i].onClick.AddListener(callbacks[i]);
            }

            return this;
        }

        void ClearAllLabelsTxts()
        {
            foreach (var lb in labels)
                if (lb != null) lb.text = string.Empty;
        }

        void RemoveAllCBs()
        {
            foreach (var btn in btns)
                if (btn != null) btn.onClick.RemoveAllListeners();
        }
    }
}
