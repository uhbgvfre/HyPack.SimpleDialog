namespace HyPack
{
    public class BasicDialog : IDialog
    {
        private Context m_Context = new Context();

        public override void Init(string title, string message)
        {
            base.Init(title, message);
            InitContextData(title, message);
        }

        private void InitContextData(string title, string message)
        {
            m_Context.title = title;
            m_Context.message = message;
        }

        public override IDialogContext GetContext() => m_Context;
        public class Context : IDialogContext
        {
            public string title;
            public string message;
        }
    }
}
