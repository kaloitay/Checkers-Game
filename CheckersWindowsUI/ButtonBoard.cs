using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System;
using CheckersLogic;

namespace CheckersWindowsUI
{
    internal class ButtonBoard
    {
        // Attributes
        private readonly SquareButton[,] m_ButtonBoard;
        private int m_LeftMargin = 0;
        private int m_TopMargin = 0;
        private SquareButton lastClickedButton;
        private bool m_isPawnOnBoardSelected = false;
        private Timer m_ComputerTurnDelay;
        private ControlPanel m_ControlPanel;

        // Properties
        internal Size Size
        {
            get
            {
                return new Size(
                    m_ControlPanel.BoardSize * m_ButtonBoard[0, 0].Size.Width,
                    m_ControlPanel.BoardSize * m_ButtonBoard[0, 0].Size.Height);
            }
        }

        internal int Left
        {
            get
            {
                return m_LeftMargin;
            }

            set
            {
                m_LeftMargin = value;
                setBoardButtonLocation();
            }
        }

        internal int Top
        {
            get
            {
                return m_TopMargin;
            }

            set
            {
                m_TopMargin = value;
                setBoardButtonLocation();
            }
        }

        // Methods
        internal ButtonBoard(ControlPanel i_ControlPanel)
        {
            m_ControlPanel = i_ControlPanel;
            m_ButtonBoard = new SquareButton[m_ControlPanel.BoardSize, m_ControlPanel.BoardSize];
            initializeButtonBoard();
            setBoardButtonLocation();
            startGame();
        }

        private void startGame()
        {
            if (m_ControlPanel.Player2.Type == ePlayerType.Computer)
            {
                m_ComputerTurnDelay = new Timer();
                m_ComputerTurnDelay.Tick += computerTurnDelay_Tick;
                m_ComputerTurnDelay.Interval = 1000;
            }

            gameManager();
        }

        private void gameManager()
        {
            if (m_ControlPanel.ActivePlayer.Type == ePlayerType.Computer)
            {
                setAllButtonsEnabled(false);
                m_ComputerTurnDelay.Start();
            }
            else
            {
                setAllButtonsEnabled(false);
                setPlayerPawnButtonsEnabled(m_ControlPanel.ActivePlayer.ID, true);
            }
        }

        private ePlayerID getNotActivePlayer()
        {
            return m_ControlPanel.ActivePlayer.ID == ePlayerID.Player1 ? ePlayerID.Player2 : ePlayerID.Player1;
        }

        private void initializeButtonBoard()
        {
            for (int row = 0; row < m_ControlPanel.BoardSize; row++)
            {
                for (int col = 0; col < m_ControlPanel.BoardSize; col++)
                {
                    m_ButtonBoard[row, col] = new SquareButton(row, col);
                    m_ButtonBoard[row, col].Click += squareButton_Click;
                    m_ButtonBoard[row, col].Font = new Font("Consolas", 12.0F);

                    if ((row + col) % 2 == 0)
                    {
                        m_ButtonBoard[row, col].BackColor = Color.Gray;
                        m_ButtonBoard[row, col].DefaultColor = Color.Gray;
                        m_ButtonBoard[row, col].Enabled = false;
                    }
                    else
                    {
                        m_ControlPanel.Board.GetSquareByPosition(row, col).SquarePawnChanged += setButton_SquarePawnChanged;
                        m_ButtonBoard[row, col].BackColor = Color.White;
                        m_ButtonBoard[row, col].DefaultColor = Color.White;
                    }

                    if (!m_ControlPanel.Board.GetSquareByPosition(row, col).IsEmpty())
                    {
                        setButtonTextByPlayerID(m_ButtonBoard[row, col]);
                    }
                }
            }
        }

        private void setButtonTextByPlayerID(SquareButton i_SquareButton)
        {
            ePawnType pawnType = m_ControlPanel.Board.GetPawnByPosition(i_SquareButton.Row, i_SquareButton.Col).Type;
            ePlayerID playerID = m_ControlPanel.Board.GetPawnByPosition(i_SquareButton.Row, i_SquareButton.Col).Belong;

            if (playerID == ePlayerID.Player1)
            {
                if (pawnType == ePawnType.Regular)
                {
                    i_SquareButton.Text = "O";
                }
                else
                {
                    i_SquareButton.Text = "U";
                }
            }
            else
            {
                if (pawnType == ePawnType.Regular)
                {
                    i_SquareButton.Text = "X";
                }
                else
                {
                    i_SquareButton.Text = "K";
                }
            }
        }

        private void onPawnSelect(SquareButton i_ButtonClicked)
        {
			lastClickedButton = i_ButtonClicked;
            setAllButtonsEnabled(true);
            setPlayerPawnButtonsEnabled(m_ControlPanel.ActivePlayer.ID, false);
            i_ButtonClicked.Enabled = true;
            i_ButtonClicked.BackColor = Color.CornflowerBlue;
            m_isPawnOnBoardSelected = true;
            setPawnPossibleButtonsColored(m_ControlPanel.Board.GetPawnByPosition(i_ButtonClicked.Row, i_ButtonClicked.Col));
        }

        private void onSameButtonClicked(SquareButton i_ButtonClicked)
        {
            ePlayerID notActivePlayer = getNotActivePlayer();

            setAllButtonsEnabled(true);
            i_ButtonClicked.BackColor = i_ButtonClicked.DefaultColor;
            setPlayerPawnButtonsEnabled(notActivePlayer, false);
            m_isPawnOnBoardSelected = false;
            lastClickedButton = null;
        }

        private void setPawnPossibleButtonsColored(Pawn i_Pawn)
        {
            List<Square> pawnPossiblesJumps = m_ControlPanel.GetPawnLegalJumpsSquares(i_Pawn.OnSquare.Row, i_Pawn.OnSquare.Col);

            foreach (Square square in pawnPossiblesJumps)
            {
                m_ButtonBoard[square.Row, square.Col].BackColor = Color.Bisque;
            }
        }

        private void setAllButtonsEnabled(bool i_Enabled)
        {
            foreach (SquareButton button in m_ButtonBoard)
            {
                if (i_Enabled)
                {
                    if ((button.Row + button.Col) % 2 != 0)
                    {
                        setButton(button, i_Enabled);
                    }
                }
                else
                {
                    setButton(button, i_Enabled);
                }
            }
        }

        private void setPlayerPawnButtonsEnabled(ePlayerID i_PlayerID, bool i_Enabled)
        {
            foreach (Square square in m_ControlPanel.GetPawnListSquares(i_PlayerID))
            {
                SquareButton button = m_ButtonBoard[square.Row, square.Col];
                setButton(button, i_Enabled);
            }
        }

        private void setButton(SquareButton i_Button, bool i_Enabled)
        {
            i_Button.Enabled = i_Enabled;
            i_Button.BackColor = i_Button.DefaultColor;
            i_Button.Font = new Font("Consolas", 12.0F, i_Enabled ? FontStyle.Bold : FontStyle.Regular);
        }

        internal void setAllButtonsEmptyText()
        {
            foreach (SquareButton button in m_ButtonBoard)
            {
                button.Text = string.Empty;
            }
        }

        private void setBoardButtonLocation()
        {
            for (int row = 0; row < m_ControlPanel.BoardSize; row++)
            {
                for (int col = 0; col < m_ControlPanel.BoardSize; col++)
                {
                    m_ButtonBoard[row, col].Top = (row * SquareButton.k_SquareButtonHeightSize) + m_TopMargin;
                    m_ButtonBoard[row, col].Left = (col * SquareButton.k_SquareButtonWidthSize) + m_LeftMargin;
                }
            }
        }

        private void squareButton_Click(object sender, EventArgs e)
        {
            SquareButton buttonClicked = sender as SquareButton;

            if (buttonClicked == lastClickedButton)
            {
                onSameButtonClicked(buttonClicked);
            }
            else
            {
                if (m_isPawnOnBoardSelected)
                {
                    bool isValidJump = m_ControlPanel.TryToExecuteJump(
                        lastClickedButton.Row,
                        lastClickedButton.Col,
                        buttonClicked.Row,
                        buttonClicked.Col);

                    if (!isValidJump)
                    {
                        MessageBox.Show("Invalid jump", "Error", MessageBoxButtons.OK);
                    }

                    m_isPawnOnBoardSelected = false;
					lastClickedButton = null;
                    gameManager();
                }
                else
                {
                    onPawnSelect(buttonClicked);
                }
            }
        }

        private void setButton_PawnTypeChanged(Pawn i_Pawn)
        {
            SquareButton button = m_ButtonBoard[i_Pawn.OnSquare.Row, i_Pawn.OnSquare.Col];

            if (i_Pawn.Belong == ePlayerID.Player1)
            {
                button.Text = "U";
            }
            else
            {
                button.Text = "K";
            }
        }

        private void setButton_SquarePawnChanged(Square i_Square)
        {
            SquareButton button = m_ButtonBoard[i_Square.Row, i_Square.Col];

            if (i_Square.IsEmpty())
            {
                button.Text = string.Empty;
            }
            else
            {
                setButtonTextByPlayerID(button);
            }
        }

        private void computerTurnDelay_Tick(object sender, EventArgs e)
        {
            m_ComputerTurnDelay.Stop();
            m_ControlPanel.ComputerJump();
            gameManager();
            lastClickedButton = null;
        }

        internal void AddControlsToForm(Form i_Form)
        {
            foreach (SquareButton button in m_ButtonBoard)
            {
                i_Form.Controls.Add(button);
            }
        }
    }
}