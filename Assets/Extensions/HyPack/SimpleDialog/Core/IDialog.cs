using System;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace HyPack
{
    public abstract class IDialog : MonoBehaviour
    {
        public bool useOverlayToClose = true;

        [Tooltip("List[0] Need ref to origin footer botton sample")]
        [SerializeField] protected List<Button> m_Buttons = new List<Button>();

        [Header("Required")]
        [SerializeField] private Button m_OverlayBtn;
        [SerializeField] private TextMeshProUGUI m_TitleText;
        [SerializeField] private TextMeshProUGUI m_MessageText;

        private Action m_OnEveryUpdateInDialog = null;
        private Action m_OnOverlayClick = null;

        protected CompositeDisposable m_EventsRxHandle;

        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ => m_OnEveryUpdateInDialog?.Invoke());

            m_OverlayBtn.OnClickAsObservable()
                .Do(_ => m_OnOverlayClick?.Invoke())
                .Where(_ => useOverlayToClose)
                .Subscribe(_ => Close());
        }

        public virtual void Init(string title, string message)
        {
            ClearAllEvents(true);
            SetTitleAndMessage(title, message);
            Open();
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            ClearAllEvents();
            Destroy(gameObject);
        }

        private void ClearAllEvents(bool newReactiveHandle = false)
        {
            m_OnEveryUpdateInDialog = null;
            m_OnOverlayClick = null;
            m_EventsRxHandle?.Dispose();
            if (newReactiveHandle) m_EventsRxHandle = new CompositeDisposable();
        }

        public void SetTitleAndMessage(string title, string message)
        {
            m_TitleText.text = string.IsNullOrEmpty(title) ? default : title;
            m_MessageText.text = string.IsNullOrEmpty(message) ? default : message;
        }

        public void SetButtonQty(int qty)
        {
            if (qty == 0)
            {
                m_Buttons.ForEach(x =>
                {
                    x.gameObject.SetActive(false);
                    x.onClick.RemoveAllListeners();
                });
            }
            else
            {
                var rootBtn = m_Buttons[0];
                while (m_Buttons.Count < qty)
                {
                    m_Buttons.Add(Instantiate(rootBtn, rootBtn.transform.parent));
                }
            }
        }

        public void SetButtonActions(params Action<IDialogContext>[] actions)
        {
            var oActions = actions.ToObservable();
            var oButtons = m_Buttons.ToObservable();

            Observable.Zip(oActions, oButtons, (a, b) => (a, b))
                .Subscribe(x =>
                {
                    x.b.onClick.RemoveAllListeners();
                    x.b.onClick.AddListener(() => x.a?.Invoke(GetContext()));
                });
        }

        public void SetButtonTexts(params string[] texts)
        {
            var oTexts = texts.ToObservable();
            var oButtons = m_Buttons.ToObservable();

            Observable.Zip(oTexts, oButtons, (t, b) => (t, b))
                .Subscribe(x =>
                {
                    var text = string.IsNullOrWhiteSpace(x.t) ? string.Empty : x.t;
                    x.b.GetComponentInChildren<TextMeshProUGUI>().text = text;
                });
        }

        public void AddEveryUpdateAction(Action action)
        {
            m_OnEveryUpdateInDialog += action;
        }

        public void AddOverlayClickAction(Action action)
        {
            m_OnOverlayClick += action;
        }

        public abstract IDialogContext GetContext();
    }
}