using System;
using System.Windows.Forms;

namespace CheckersWindowsUI
{
    internal class BoardSizeSetting
    {
        // Defines
        private const int k_SizeOptionsMarginFromLabel = 20;
        private const int k_SizeOptionsMarginEachOther = 60;

        // Attributes
        private int m_BoardSize = 6;
        private Label boardSizeLabel = new Label();
        private RadioButton size6 = new RadioButton();
        private RadioButton size8 = new RadioButton();
        private RadioButton size10 = new RadioButton();

        // Properties
        internal int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        internal int Top
        {
            get
            {
                return boardSizeLabel.Top;
            }

            set
            {
                boardSizeLabel.Top = value;
                updateControlsTopMargins();
            }
        }

        internal int Left
        {
            get
            {
                return boardSizeLabel.Left;
            }

            set
            {
                boardSizeLabel.Left = value;
                updateControlsLeftMargins();
            }
        }

        // Methods
        internal BoardSizeSetting()
        {
            setControlsText();
            updateControlsTopMargins();
            updateControlsLeftMargins();
            size6.Click += new EventHandler(radioButton_Click);
            size8.Click += new EventHandler(radioButton_Click);
            size10.Click += new EventHandler(radioButton_Click);
        }

        private void setControlsText()
        {
            boardSizeLabel.Text = "Board Size:";
            size6.Text = " 6 x 6";
            size8.Text = " 8 x 8";
            size10.Text = " 10 x 10";

            size6.Name = "6";
            size8.Name = "8";
            size10.Name = "10";

            size6.AutoSize = true;
            size8.AutoSize = true;
            size10.AutoSize = true;

            size6.Checked = true;
        }

        private void updateControlsTopMargins()
        {
            size6.Top = boardSizeLabel.Top + k_SizeOptionsMarginFromLabel;
            size8.Top = boardSizeLabel.Top + k_SizeOptionsMarginFromLabel;
            size10.Top = boardSizeLabel.Top + k_SizeOptionsMarginFromLabel;
        }

        private void updateControlsLeftMargins()
        {
            size6.Left = boardSizeLabel.Left + 10;
            size8.Left = size6.Left + k_SizeOptionsMarginEachOther;
            size10.Left = size8.Left + k_SizeOptionsMarginEachOther;
        }

        internal void addControlsToForm(Form i_Form)
        {
            i_Form.Controls.Add(boardSizeLabel);
            i_Form.Controls.Add(size6);
            i_Form.Controls.Add(size8);
            i_Form.Controls.Add(size10);
        }

        private void radioButton_Click(object sender, EventArgs e)
        {
            RadioButton clickedRadioButton = sender as RadioButton;
            m_BoardSize = int.Parse(clickedRadioButton.Name);
        }
    }
}