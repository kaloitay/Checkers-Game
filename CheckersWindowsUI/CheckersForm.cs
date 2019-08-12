using System.Windows.Forms;
using System.Drawing;
using System;
using CheckersLogic;

namespace CheckersWindowsUI
{
    public class CheckersForm : Form
    {
        // Defines
        internal const string k_GameName = "Damka";
        private const string k_AnotherRound = "Another Round?";
        private const int k_LeftMargin = 10;
        private const int k_RightMargin = 10;
        private const int k_TopMargin = 30;
        private const int k_BottomMargin = 15;

        // Attributes
        private GameSettingsForm m_GameSettingsForm;
        private ScoreLabel scoreLabel;
        private ButtonBoard m_Board;
        private EndGameMessageBox m_PlayAgainMessageBox = new EndGameMessageBox();
        private ControlPanel m_ControlPanel;

        // Methods
        public CheckersForm()
        {
            this.Text = k_GameName;
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void setScoreLabelLocation()
        {
            scoreLabel.Top = k_TopMargin;
            scoreLabel.Left = (this.Width - scoreLabel.Width) / 2;
        }

        private void setBoardLocation()
        {
            m_Board.Left = k_LeftMargin;
            m_Board.Top = k_TopMargin + scoreLabel.Top;
        }

        private void setBoardFormSize()
        {
            int clientWidth = m_Board.Size.Width + k_LeftMargin + k_RightMargin;
            int clientHeight = m_Board.Size.Height + scoreLabel.Height + k_TopMargin + k_BottomMargin;
            this.ClientSize = new Size(clientWidth, clientHeight);
        }

        private void setEndGameMessageBox()
        {
            m_PlayAgainMessageBox.Caption = k_GameName;
            m_PlayAgainMessageBox.Owner = this;
        }

        protected override void OnShown(EventArgs e)
        {
            m_ControlPanel = new ControlPanel();
            m_GameSettingsForm = new GameSettingsForm(m_ControlPanel);

            m_GameSettingsForm.ShowDialog();
            base.OnShown(e);

            if (m_GameSettingsForm.DialogResult == DialogResult.OK)
            {
                initializeBoardForm();
            }
            else
            {
                this.Close();
            }
        }

        private void initializeBoardForm()
        {
            m_ControlPanel.RandomFirstTurnPlayer();
            scoreLabel = new ScoreLabel();

            scoreLabel.Player1Name = string.Format("{0}:", m_ControlPanel.Player1.Name);
            scoreLabel.Player1Score = m_ControlPanel.Player1.TotalRank;
            scoreLabel.Player2Name = string.Format("{0}:", m_ControlPanel.Player2.Name);
            scoreLabel.Player2Score = m_ControlPanel.Player2.TotalRank;
            scoreLabel.SetAllLabelsPosition();

            m_Board = new ButtonBoard(m_ControlPanel);
            setBoardFormSize();
            scoreLabel.AddControlsToForm(this);
            m_Board.AddControlsToForm(this);

            setScoreLabelLocation();
            setBoardLocation();
            setEndGameMessageBox();
            m_ControlPanel.GameStatusChanged += endGame_GameStatusChanged;
        }

        private void endGame_GameStatusChanged()
        {
            switch (m_ControlPanel.GameStatus)
            {
                case eGameStatus.Player1Won:
                    m_PlayAgainMessageBox.Message = string.Format(
@"{0} Won!
{1}",
m_ControlPanel.Player1.Name,
k_AnotherRound);

                    m_PlayAgainMessageBox.Show();
                    handlePlayAgainDialogResult();
                    break;

                case eGameStatus.Player2Won:
                    m_PlayAgainMessageBox.Message = string.Format(
@"{0} Won!
{1}",
m_ControlPanel.Player2.Name,
k_AnotherRound);

                    m_PlayAgainMessageBox.Show();
                    handlePlayAgainDialogResult();
                    break;

                case eGameStatus.Tie:
                    m_PlayAgainMessageBox.Message = string.Format(
@"Tie!
{0}",
k_AnotherRound);

                    m_PlayAgainMessageBox.Show();
                    handlePlayAgainDialogResult();
                    break;
            }
        }

        private void handlePlayAgainDialogResult()
        {
            if (m_PlayAgainMessageBox.DialogResult == DialogResult.Yes)
            {
                m_Board.setAllButtonsEmptyText();
                m_ControlPanel.PlayNewRound();
                scoreLabel.Player1Score = m_ControlPanel.Player1.TotalRank;
                scoreLabel.Player2Score = m_ControlPanel.Player2.TotalRank;
            }
            else
            {
                this.Close();
            }
        }
    }
}