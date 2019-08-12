using System;

namespace CheckersLogic
{
    public class Board
    {
        // Attributes
        private readonly int m_BoardSize;
        private readonly Square[,] m_Board;

        // Properties
        internal int Size
        {
            get
            {
                return m_BoardSize;
            }
        }

        // Methods
        internal Board(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_Board = new Square[i_BoardSize, i_BoardSize];
            initializeBoard();
            SetIntialPawnsPositionOnSquare();
        }

        public Square GetSquareByPosition(int i_Row, int i_Col)
        {
            Square square = null;

            if (IsValidRowColBoardIndexs(i_Row, i_Col))
            {
                square = m_Board[i_Row, i_Col];
            }

            return square;
        }

        public Pawn GetPawnByPosition(int i_Row, int i_Col)
        {
            Pawn pawn = null;

            if (IsValidRowColBoardIndexs(i_Row, i_Col))
            {
                pawn = m_Board[i_Row, i_Col].Pawn;
            }

            return pawn;
        }

        public bool IsValidRowColBoardIndexs(int i_Row, int i_Col)
        {
            return (i_Row >= 0 && i_Row < m_BoardSize) && (i_Col >= 0 && i_Col < m_BoardSize);
        }

        private void initializeBoard()
        {
            for (int row = 0; row < m_BoardSize; row++)
            {
                for (int col = 0; col < m_BoardSize; col++)
                {
                    m_Board[row, col] = new Square(row, col);
                }
            }
        }

        internal void ClearAllPawnsOnBoard()
        {
            foreach (Square square in m_Board)
            {
                square.Pawn = null;
            }
        }

        internal void SetIntialPawnsPositionOnSquare()
        {
            for (int row = 0; row < m_BoardSize; row++)
            {
                for (int col = 0; col < m_BoardSize; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        if (row < (m_BoardSize / 2) - 1)
                        {
                            // player1 pawn on the square
                            m_Board[row, col].Pawn = new Pawn(ePawnType.Regular, ePlayerID.Player1, m_Board[row, col]);
                        }
                        else if (row > (m_BoardSize / 2))
                        {
                            // player2 pawn on the square
                            m_Board[row, col].Pawn = new Pawn(ePawnType.Regular, ePlayerID.Player2, m_Board[row, col]);
                        }
                    }
                }
            }
        }
    }
}