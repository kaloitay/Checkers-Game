using System;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersWindowsUI
{
    public partial class GameSettingsForm : Form
    {
        // Attributes
        private int m_BoardSize;
        private ControlPanel m_ControlPanel;

        // Methods
        public GameSettingsForm(ControlPanel i_ControlPanel)
        {
            m_ControlPanel = i_ControlPanel;
            InitializeComponent();
        }

        private void RadioButton6x6_Click(object sender, EventArgs e)
        {
            m_BoardSize = 6;
        }

        private void RadioButton8x8_Click(object sender, EventArgs e)
        {
            m_BoardSize = 8;
        }

        private void RadioButton10x10_Click(object sender, EventArgs e)
        {
            m_BoardSize = 10;
        }

        private void Player2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Player2TextBox.Enabled = Player2CheckBox.Checked;
        }

        private void Done_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            initializeGameSettings();
            this.Close();
        }

        private void initializeGameSettings()
        {
            m_ControlPanel.InitializeBoard(m_BoardSize);
            m_ControlPanel.InitializePlayer(ePlayerID.Player1, ePlayerType.Human, Player1TextBox.Text);

            if (Player2CheckBox.Checked)
            {
                m_ControlPanel.InitializePlayer(ePlayerID.Player2, ePlayerType.Human, Player2TextBox.Text);
            }
            else
            {
                m_ControlPanel.InitializePlayer(ePlayerID.Player2, ePlayerType.Computer, "Computer");
            }
        }
    }
}
