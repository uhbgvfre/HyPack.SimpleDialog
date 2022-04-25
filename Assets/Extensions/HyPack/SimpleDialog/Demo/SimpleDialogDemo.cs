using UnityEngine;
using HyPack;

namespace HyPack.Demo
{
    public class SimpleDialogDemo : MonoBehaviour
    {
        public void ShowDialog_0B()
        {
            SimpleDialog.ShowDialog0B("TestTitle_0B", "TestMsg_0B", () => print("bgClicked"));
        }

        public void ShowDialog_1B()
        {
            SimpleDialog.ShowDialog1B("TestTitle_1B", "TestMsg_1B",
                () => print("btnClicked"),
                () => print("bgClicked")
                );
        }

        public void ShowDialog_VX()
        {
            SimpleDialog.ShowDialogVX("TestTitle_VX", "TestMsg_VX",
                () => print("vCallback"),
                () => print("xCallback")
                );
        }

        public void ShowDialog_1InpVX()
        {
            SimpleDialog.ShowDialog1InpVX("TestTitle_1InpVX", "TestMsg_1InpVX",
                (inp) => print("vCallback: " + inp),
                (inp) => print("xCallback: " + inp)
                );
        }

        public void ShowDialog_2InpVX()
        {
            SimpleDialog.ShowDialog2InpVX("TestTitle_2InpVX", "TestMsg_2InpVX_0", "TestMsg_2InpVX_1",
                (inp0, inp1) => print("vCallback: " + string.Join(",", inp0, inp1)),
                (inp0, inp1) => print("xCallback: " + string.Join(",", inp0, inp1))
                );
        }
    }

}
