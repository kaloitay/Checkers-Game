using System.Windows.Forms;

namespace CheckersWindowsUI
{
    internal class EndGameMessageBox
    {
        // Attributes
        private string m_Message;
        private string m_Caption;
        private MessageBoxButtons m_Buttons = MessageBoxButtons.YesNo;
        private DialogResult m_Result;
        private IWin32Window m_Owner;

        // Properties
        internal string Message
        {
            get
            {
                return m_Message;
            }

            set
            {
                m_Message = value;
            }
        }

        internal string Caption
        {
            get
            {
                return m_Caption;
            }

            set
            {
                m_Caption = value;
            }
        }

        internal DialogResult DialogResult
        {
            get
            {
                return m_Result;
            }
        }

        internal IWin32Window Owner
        {
            get
            {
                return m_Owner;
            }

            set
            {
                m_Owner = value;
            }
        }

        // Methods
        internal void Show()
        {
            m_Result = MessageBox.Show(m_Owner, m_Message, m_Caption, m_Buttons);
        }
    }
}
