using UnityEngine;
using UnityEngine.UI;

public class CustomUI_Advance : MonoBehaviour
{
    [SerializeField] Button m_ChooseColorBtn;
    [SerializeField] Graphic m_ColorPreview1;
    [SerializeField] Graphic m_ColorPreview2;

    [SerializeField] Button m_Choose2ColorsBtn;
    [SerializeField] Graphic m_ColorPreview3;
    [SerializeField] Graphic m_ColorPreview4;

    private void Start()
    {
        m_ChooseColorBtn.onClick.AddListener(ShowDialog_ChooseColor);
        m_Choose2ColorsBtn.onClick.AddListener(ShowDialog_Choose2Colors);
    }

    private void ShowDialog_ChooseColor()
    {
        CustomAdvanceDialogHelper.ShowDialog_SelectColor(ctx =>
        {
            print("O > ctx.selectedColor:" + ctx.selectedColor);
            m_ColorPreview1.color = m_ColorPreview2.color = ctx.selectedColor ?? m_ColorPreview1.color;
        }, ctx =>
        {
            print("X > ctx.selectedColor:" + ctx.selectedColor);
        });
    }

    private void ShowDialog_Choose2Colors()
    {
        CustomAdvanceDialogHelper.ShowDialog_Select2Colors(ctx =>
        {
            print("O > color0:" + ctx.selectedColor_First + " color1:" + ctx.selectedColor_Second);
            m_ColorPreview3.color = ctx.selectedColor_First ?? m_ColorPreview3.color;
            m_ColorPreview4.color = ctx.selectedColor_Second ?? m_ColorPreview4.color;
        }, ctx =>
        {
            print("X > Cancel!");
        });
    }
}
