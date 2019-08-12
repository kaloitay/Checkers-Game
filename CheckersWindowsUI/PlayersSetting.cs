using System;
using System.Windows.Forms;

namespace CheckersWindowsUI
{
    internal class PlayersSetting
    {
        // Defines
        private const int k_SizeOptionsMarginFromTopLabel = 30;
        private const int k_SizeOptionsMarginEachOther = 100;

        // Attributes
        private Label playersLabel = new Label();
        private Label player1Label = new Label();
        private Label player2Label = new Label();
        private TextBox player1TextBox = new TextBox();
        private TextBox player2TextBox = new TextBox();
        private CheckBox player2HumanCheckBox = new CheckBox();

        // Properties
        internal string Player1Name
        {
            get
            {
                return player1TextBox.Text;
            }
        }

        internal string Player2Name
        {
            get
            {
                return player2TextBox.Text;
            }
        }

        internal bool Player2Human
        {
            get
            {
                return player2HumanCheckBox.Checked;
            }
        }

        internal int Top
        {
            get
            {
                return playersLabel.Top;
            }

            set
            {
                playersLabel.Top = value;
                updateControlsTopMargins();
            }
        }

        internal int Left
        {
            get
            {
                return playersLabel.Left;
            }

            set
            {
                playersLabel.Left = value;
                updateControlsLeftMargins();
            }
        }

        // Methods
        internal PlayersSetting()
        {
            setControlsText();
            updateControlsTopMargins();
            updateControlsLeftMargins();

            player2HumanCheckBox.Click += isPlayer2Human_Click;
        }

        private void setControlsText()
        {
            playersLabel.Text = "Players:";
            player1Label.Text = "Player 1:";
            player2Label.Text = "Player 2:";
            player2TextBox.Text = "[Computer]";

            player1Label.AutoSize = true;
            player2Label.AutoSize = true;
            player2HumanCheckBox.AutoSize = true;

            player2TextBox.Enabled = false;
            player2HumanCheckBox.Checked = false;
        }

        private void updateControlsTopMargins()
        {
            player1Label.Top = playersLabel.Top + k_SizeOptionsMarginFromTopLabel;
            player1TextBox.Top = player1Label.Top;

            player2Label.Top = player1Label.Top + k_SizeOptionsMarginFromTopLabel;
            player2TextBox.Top = player2Label.Top;
            player2HumanCheckBox.Top = player2Label.Top;
        }

        private void updateControlsLeftMargins()
        {
            player1Label.Left = playersLabel.Left + 10;
            player1TextBox.Left = player1Label.Left + k_SizeOptionsMarginEachOther;

            player2HumanCheckBox.Left = player1Label.Left;
            player2Label.Left = player2HumanCheckBox.Left + 20;
            player2TextBox.Left = player1Label.Left + k_SizeOptionsMarginEachOther;
        }

        internal void addControlsToForm(Form i_Form)
        {
            i_Form.Controls.Add(playersLabel);
            i_Form.Controls.Add(player1Label);
            i_Form.Controls.Add(player2Label);
            i_Form.Controls.Add(player1TextBox);
            i_Form.Controls.Add(player2TextBox);
            i_Form.Controls.Add(player2HumanCheckBox);
        }

        private void isPlayer2Human_Click(object sender, EventArgs e)
        {
            player2TextBox.Enabled = !player2TextBox.Enabled;

            if (player2TextBox.Enabled)
            {
                player2TextBox.Text = string.Empty;
            }
            else
            {
                player2TextBox.Text = "[Computer]";
            }
        }
    }
}