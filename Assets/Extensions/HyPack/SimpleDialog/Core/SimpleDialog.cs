using UnityEngine;

namespace HyPack
{
    public class SimpleDialog : MonoBehaviour
    {
        #region Singleton 
        // ? You can remove this region, then use your way to intergrate in your app.
        private static SimpleDialog s_Instance;
        private void Awake()
        {
            if (s_Instance != null)
            {
                print("[Warning] Instance of SimpleDialog can exist one only.");
                DestroyImmediate(this);
                return;
            }

            s_Instance = this;
            DontDestroyOnLoad(this);
        }

        #endregion Singleton

        public Transform dialogParentCanvas;
        public DialogRepo dialogRepo;

        private void Start()
        {
            dialogRepo.Initialize();
        }

        public static T GetDialogInstance<T>(string title = null, string message = null) where T : IDialog
        {
            T template = s_Instance.dialogRepo.GetTemplate<T>();
            T newDialog = Instantiate(template, s_Instance.dialogParentCanvas);
            newDialog.Init(title, message);
            return newDialog;
        }
    }
}