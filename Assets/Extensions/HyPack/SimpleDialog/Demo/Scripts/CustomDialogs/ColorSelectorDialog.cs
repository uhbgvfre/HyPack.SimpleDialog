using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace HyPack
{
    public class ColorSelectorDialog : IDialog
    {
        [Header("ColorSelector")]
        [SerializeField] private ToggleGroup m_ToggleGroup;
        [SerializeField] private Toggle m_FirstToggle;

        private Context m_Context = new Context();

        public void Init(string title, string massage, params Color[] colorPlatte)
        {
            base.Init(title, massage);
            InitColorPlatte(colorPlatte);
            BindUIReaction();
        }

        private void InitColorPlatte(params Color[] colors)
        {
            while (m_ToggleGroup.transform.childCount < colors.Length)
            {
                Instantiate(m_FirstToggle, m_FirstToggle.transform.parent);
            }

            SetColorToggles(colors);
        }

        private void SetColorToggles(params Color[] colors)
        {
            m_Context.colorPlatte = colors;
            var qty = colors.Length;

            if (qty == 0)
            {
                var oTgs = m_ToggleGroup.transform.GetComponentsInChildren<Toggle>(true).ToObservable();
                oTgs.Subscribe(tg => tg.gameObject.SetActive(false));
            }
            else
            {
                while (m_ToggleGroup.transform.childCount < qty)
                {
                    Instantiate(m_FirstToggle, m_ToggleGroup.transform);
                }

                var oTgs = m_ToggleGroup.transform.GetComponentsInChildren<Toggle>(true).ToObservable();
                var oColors = colors.ToObservable();
                Observable.Zip(oTgs, oColors, (t, c) => (t, c))
                    .Subscribe(x =>
                    {
                        var fill = x.t.GetComponentInChildren<Graphic>();
                        fill.color = x.c == null ? default : x.c;
                    });
            }

            m_ToggleGroup.SetAllTogglesOff();
        }

        private void BindUIReaction()
        {
            m_ToggleGroup.ObserveEveryValueChanged(x => x.GetFirstActiveToggle())
                .Subscribe(x => { m_Context.selectedColor = (x == null) ? null : m_Context.colorPlatte[x.transform.GetSiblingIndex()]; })
                .AddTo(m_EventsRxHandle);
        }

        public override IDialogContext GetContext() => m_Context;

        public class Context : IDialogContext
        {
            public Color[] colorPlatte;
            public Color? selectedColor = null;
        }
    }
}