using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersWindowsUI
{
    internal class ScoreLabel
    {
        // Defines
        private const int k_NamesLabelsMargin = 60;

        // Attributes
        private Label player1NameLabel = new Label();
        private Label player1ScoreLabel = new Label();
        private Label player2NameLabel = new Label();
        private Label player2ScoreLabel = new Label();

        // Properties
        internal int Top
        {
            get
            {
                return player1NameLabel.Top;
            }

            set
            {
                player1NameLabel.Top = value;
                SetAllLabelsPosition();
            }
        }

        internal int Left
        {
            get
            {
                return player1NameLabel.Left;
            }

            set
            {
                player1NameLabel.Left = value;
                SetAllLabelsPosition();
            }
        }

        internal int Width
        {
            get
            {
                return player1NameLabel.Width + player1ScoreLabel.Width + player2NameLabel.Width + player2ScoreLabel.Width + k_NamesLabelsMargin;
            }
        }

        internal int Height
        {
            get
            {
                return player1NameLabel.Height;
            }
        }

        internal string Player1Name
        {
            set
            {
                player1NameLabel.Text = value;
            }
        }

        internal string Player2Name
        {
            set
            {
                player2NameLabel.Text = value;
            }
        }
        
        internal int Player1Score
        {
            get
            {
                return int.Parse(player1ScoreLabel.Text);
            }

            set
            {
                player1ScoreLabel.Text = value.ToString();
            }
        }

        internal int Player2Score
        {
            get
            {
                return int.Parse(player2ScoreLabel.Text);
            }

            set
            {
                player2ScoreLabel.Text = value.ToString();
            }
        }

        // Methods
        internal ScoreLabel()
        {
            player1NameLabel.AutoSize = true;
            player1ScoreLabel.AutoSize = true;
            player2NameLabel.AutoSize = true;
            player2ScoreLabel.AutoSize = true;
        }

        internal void AddControlsToForm(Form i_Form)
        {
            i_Form.Controls.Add(player1NameLabel);
            i_Form.Controls.Add(player1ScoreLabel);
            i_Form.Controls.Add(player2NameLabel);
            i_Form.Controls.Add(player1NameLabel);
            i_Form.Controls.Add(player2NameLabel);
            i_Form.Controls.Add(player2ScoreLabel);
        }

        public void SetAllLabelsPosition()
        {
            player1ScoreLabel.Location = new Point(player1NameLabel.Right, player1NameLabel.Top);
            player2NameLabel.Location = new Point(player1ScoreLabel.Right + k_NamesLabelsMargin, player1NameLabel.Top);
            player2ScoreLabel.Location = new Point(player2NameLabel.Right, player1NameLabel.Top);
        }
    }
}
