using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace HyPack
{
    public class TwoColorSelectorDialog : IDialog
    {
        [Header("ColorSelector")]
        [SerializeField] private ToggleGroup m_ToggleGroup_First;
        [SerializeField] private Toggle m_HeadToggle_First;
        [SerializeField] private ToggleGroup m_ToggleGroup_Second;
        [SerializeField] private Toggle m_HeadToggle_Second;

        private Context m_Context = new Context();

        public void Init(string title, string massage, Color[] colorPlatte_First, Color[] colorPlatte_Second)
        {
            base.Init(title, massage);
            InitContextData(colorPlatte_First, colorPlatte_Second);
            InitColorPlatte(m_ToggleGroup_First, m_HeadToggle_First, colorPlatte_First);
            InitColorPlatte(m_ToggleGroup_Second, m_HeadToggle_Second, colorPlatte_Second);
            BindUIReaction();
        }

        private void InitColorPlatte(ToggleGroup targetTgGrp, Toggle targetHeadTg, params Color[] colorPlatte)
        {
            while (targetTgGrp.transform.childCount < colorPlatte.Length)
            {
                Instantiate(targetHeadTg, targetHeadTg.transform.parent);
            }

            SetColorToggles(targetTgGrp, targetHeadTg, colorPlatte);
        }

        private void SetColorToggles(ToggleGroup targetTgGrp, Toggle targetHeadTg, params Color[] colors)
        {
            var qty = colors.Length;

            if (qty == 0)
            {
                var oTgs = targetTgGrp.transform.GetComponentsInChildren<Toggle>(true).ToObservable();
                oTgs.Subscribe(tg => tg.gameObject.SetActive(false));
            }
            else
            {
                while (targetTgGrp.transform.childCount < qty)
                {
                    Instantiate(targetHeadTg, targetTgGrp.transform);
                }

                var oTgs = targetTgGrp.transform.GetComponentsInChildren<Toggle>(true).ToObservable();
                var oColors = colors.ToObservable();
                Observable.Zip(oTgs, oColors, (t, c) => (t, c))
                    .Subscribe(x =>
                    {
                        var fill = x.t.GetComponentInChildren<Graphic>();
                        fill.color = x.c == null ? default : x.c;
                    });
            }

            targetTgGrp.SetAllTogglesOff();
        }

        private void InitContextData(Color[] colorPlatte_First, Color[] colorPlatte_Second)
        {
            m_Context.colorPlatte_First = colorPlatte_First;
            m_Context.colorPlatte_Second = colorPlatte_Second;
        }

        private void BindUIReaction()
        {
            m_ToggleGroup_First.ObserveEveryValueChanged(x => x.GetFirstActiveToggle())
                .Subscribe(x => { m_Context.selectedColor_First = (x == null) ? null : m_Context.colorPlatte_First[x.transform.GetSiblingIndex()]; })
                .AddTo(m_EventsRxHandle);

            m_ToggleGroup_Second.ObserveEveryValueChanged(x => x.GetFirstActiveToggle())
                .Subscribe(x => { m_Context.selectedColor_Second = (x == null) ? null : m_Context.colorPlatte_Second[x.transform.GetSiblingIndex()]; })
                .AddTo(m_EventsRxHandle);
        }

        public override IDialogContext GetContext() => m_Context;

        public class Context : IDialogContext
        {
            public Color[] colorPlatte_First;
            public Color[] colorPlatte_Second;
            public Color? selectedColor_First = null;
            public Color? selectedColor_Second = null;
        }
    }
}