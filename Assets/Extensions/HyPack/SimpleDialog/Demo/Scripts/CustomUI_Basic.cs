using UnityEngine;
using UnityEngine.UI;

public class CustomUI_Basic : MonoBehaviour
{
    [SerializeField] Button m_SayHiBtn;
    [SerializeField] Button m_SayHelloBtn;

    private void Start()
    {
        m_SayHiBtn.onClick.AddListener(ShowDialog_SayHi);
        m_SayHelloBtn.onClick.AddListener(ShowDialog_SayHello);
    }

    private void ShowDialog_SayHi()
    {
        CustomBasicDialogHelper.ShowDialog_SayHi();
    }
    private void ShowDialog_SayHello()
    {
        CustomBasicDialogHelper.ShowDialog_SayHello();
    }
}
