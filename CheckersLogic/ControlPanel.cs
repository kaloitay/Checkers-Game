using System;
using System.Collections.Generic;

namespace CheckersLogic
{
    public class ControlPanel
    {
        // Delegates
        public event Action GameStatusChanged;

        // Defines
        public const int k_KingPawnRank = 4;
        public const int k_RegularPawnRank = 1;
        private const int k_QuitValue = -1;

        // Attributes
        private eGameStatus m_GameStatus;
        private Board m_Board;
        private Player m_CheckersPlayer1;
        private Player m_CheckersPlayer2;
        private Player m_ActivePlayer;
        private eJumpType m_LastJumpType;
        private bool m_IsPlayerInCapturingSequence = false;
        private bool m_IsLastJumpPawnBecomesKing = false;

        // Properties
        internal eJumpType LastJumpType
        {
            get
            {
                return m_LastJumpType;
            }

            set
            {
                m_LastJumpType = value;
            }
        }

        public Player Player1
        {
            get
            {
                return m_CheckersPlayer1;
            }
        }

        public Player Player2
        {
            get
            {
                return m_CheckersPlayer2;
            }
        }

        public Player ActivePlayer
        {
            get
            {
                return m_ActivePlayer;
            }
        }

        public eGameStatus GameStatus
        {
            get
            {
                return m_GameStatus;
            }

            set
            {
                m_GameStatus = value;
                OnGameStatusChanged();
            }
        }

        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_Board.Size;
            }
        }

        // Methods
        public void InitializeBoard(int i_BoardSize)
        {
            m_Board = new Board(i_BoardSize);
        }

        public void InitializePlayer(ePlayerID i_ID, ePlayerType i_Type, string i_Name)
        {
            int numberOfPawns = ((BoardSize / 2) - 1) * (BoardSize / 2);

            if (i_ID == ePlayerID.Player1)
            {
                m_CheckersPlayer1 = new Player(i_ID, i_Type, i_Name, numberOfPawns);
                associatePawnsToPlayer(m_CheckersPlayer1);
            }
            else
            {
                m_CheckersPlayer2 = new Player(i_ID, i_Type, i_Name, numberOfPawns);
                associatePawnsToPlayer(m_CheckersPlayer2);
            }

            GameStatus = eGameStatus.Playing;
        }

        public void RandomFirstTurnPlayer()
        {
            Random randomNumber = new Random();
            ePlayerID randomPlayer = (ePlayerID)randomNumber.Next(1, 3);

            m_ActivePlayer = (randomPlayer == ePlayerID.Player1) ? m_CheckersPlayer1 : m_CheckersPlayer2;
            updateLegalJumpList();
        }

        public bool TryToExecuteJump(int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            bool isJumpSucceed = false;

            if (isSourceAndDestSquaresAreValid(i_RowSource, i_ColSource, i_RowDest, i_ColDest))
            {
                Pawn pawn = m_Board.GetPawnByPosition(i_RowSource, i_ColSource);
                Square dest = m_Board.GetSquareByPosition(i_RowDest, i_ColDest);

                if (m_ActivePlayer.IsLegalJump(pawn, dest))
                {
                    executeJump(pawn, dest);
                    isJumpSucceed = true;
                }
            }

            return isJumpSucceed;
        }

        public void PlayNewRound()
        {
            updateEndGamePlayersTotalRank();
            initializeCheckersPlayer(m_CheckersPlayer1);
            initializeCheckersPlayer(m_CheckersPlayer2);
            m_Board.ClearAllPawnsOnBoard();
            m_Board.SetIntialPawnsPositionOnSquare();
            m_LastJumpType = eJumpType.Illegal;
            m_IsPlayerInCapturingSequence = false;
            m_IsLastJumpPawnBecomesKing = false;
            associatePawnsToPlayer(m_CheckersPlayer1);
            associatePawnsToPlayer(m_CheckersPlayer2);
            RandomFirstTurnPlayer();
            GameStatus = eGameStatus.Playing;
        }

        private void initializeCheckersPlayer(Player i_CheckersPlayer)
        {
            i_CheckersPlayer.RemoveAllPawns();
            i_CheckersPlayer.CurrentGameRank = 0;
            i_CheckersPlayer.LastActivatedPawn = null;
        }

        public bool TryToQuit()
        {
            bool quitSuccess = false;

            if (m_CheckersPlayer1.CurrentGameRank >= m_CheckersPlayer2.CurrentGameRank && m_ActivePlayer.ID == ePlayerID.Player2)
            {
                GameStatus = eGameStatus.Player1Won;
                updateEndGamePlayersTotalRank();
                quitSuccess = true;
            }
            else if (m_CheckersPlayer2.CurrentGameRank >= m_CheckersPlayer1.CurrentGameRank && m_ActivePlayer.ID == ePlayerID.Player1)
            {
                GameStatus = eGameStatus.Player2Won;
                updateEndGamePlayersTotalRank();
                quitSuccess = true;
            }

            return quitSuccess;
        }

        public void ComputerJump()
        {
            Pawn pawn;
            Square destSquare;

            randomComputerJump(out pawn, out destSquare);
            executeJump(pawn, destSquare);
        }

        public List<Square> GetPawnLegalJumpsSquares(int i_PawnRow, int i_PawnCol)
        {
            List<Square> pawnLegalJumpsSquares = new List<Square>();
            Pawn pawn = m_Board.GetPawnByPosition(i_PawnRow, i_PawnCol);

            foreach (LegalJump legalJump in pawn.LegalJumps)
            {
                pawnLegalJumpsSquares.Add(legalJump.SquareDest);
            }

            return pawnLegalJumpsSquares;
        }

        public List<Square> GetPawnListSquares(ePlayerID i_PlayerID)
        {
            List<Square> pawnListSquares = new List<Square>();
            Player player = i_PlayerID == ePlayerID.Player1 ? m_CheckersPlayer1 : m_CheckersPlayer2;

            foreach (Pawn pawn in player.PawnList)
            {
                pawnListSquares.Add(pawn.OnSquare);
            }

            return pawnListSquares;
        }

        private bool isValidJump(Pawn i_Pawn, Square i_Dest)
        {
            bool isValidJump = false;

            if (i_Pawn != null && i_Dest != null && m_ActivePlayer.ID == i_Pawn.Belong)
            {
                eJumpType jumpType = pawnJumpType(i_Pawn, i_Dest);

                if ((m_IsPlayerInCapturingSequence == true && i_Pawn == m_ActivePlayer.LastActivatedPawn && jumpType == eJumpType.Capture) ||
                    (m_IsPlayerInCapturingSequence == false &&
                    (jumpType == eJumpType.Capture || (m_LastJumpType != eJumpType.Capture && m_ActivePlayer.IsCaptureAvailable == false && jumpType == eJumpType.Normal))))
                {
                    isValidJump = true;
                }
            }

            return isValidJump;
        }

        private eJumpType pawnJumpType(Pawn i_Pawn, Square i_Dest)
        {
            eJumpType jumpType = eJumpType.Illegal;
            int rowNormalStepSize = (int)i_Pawn.Direction;

            if (isNormalJump(i_Pawn, i_Dest))
            {
                jumpType = eJumpType.Normal;
            }
            else if (isCaptureJump(i_Pawn, i_Dest))
            {
                jumpType = eJumpType.Capture;
            }

            return jumpType;
        }

        private void randomComputerJump(out Pawn i_Pawn, out Square i_DestSquare)
        {
            int randomLegalMove = new Random().Next(0, ActivePlayer.LegalJumpList.Count);
            Pawn pawn = ActivePlayer.LegalJumpList[randomLegalMove].Pawn;
            Square sourceSquare = pawn.OnSquare;
            Square destSquare = ActivePlayer.LegalJumpList[randomLegalMove].SquareDest;

            i_Pawn = pawn;
            i_DestSquare = destSquare;
        }

        private void checkAndChangeTurn()
        {
            if (m_LastJumpType == eJumpType.Normal || m_ActivePlayer.LegalJumpList.Count == 0 || m_IsLastJumpPawnBecomesKing == true)
            {
                m_LastJumpType = eJumpType.Illegal;
                m_IsPlayerInCapturingSequence = false;
                m_IsLastJumpPawnBecomesKing = false;
                updateLegalJumpList();
                toggleActivePlayer();
                updateLegalJumpList();
                updateGameStatus();
            }
        }

        private void toggleActivePlayer()
        {
            m_ActivePlayer = (m_ActivePlayer.ID == ePlayerID.Player1) ? m_CheckersPlayer2 : m_CheckersPlayer1;
        }

        private bool isSourceAndDestSquaresAreValid(int i_RowSource, int i_ColSource, int i_RowDest, int i_ColDest)
        {
            return m_Board.IsValidRowColBoardIndexs(i_RowSource, i_ColSource) &&
                    m_Board.IsValidRowColBoardIndexs(i_RowDest, i_ColDest) &&
                    m_Board.GetSquareByPosition(i_RowSource, i_ColSource).IsEmpty() == false;
        }

        private void updateEndGamePlayersTotalRank()
        {
            if (m_GameStatus == eGameStatus.Player1Won)
            {
                m_CheckersPlayer1.TotalRank += m_CheckersPlayer1.CurrentGameRank - m_CheckersPlayer2.CurrentGameRank;
            }
            else if (m_GameStatus == eGameStatus.Player2Won)
            {
                m_CheckersPlayer2.TotalRank += m_CheckersPlayer2.CurrentGameRank - m_CheckersPlayer1.CurrentGameRank;
            }
        }

        private void updateGameStatus()
        {
            if (m_GameStatus == eGameStatus.Playing)
            {
                if (m_CheckersPlayer1.LegalJumpList.Count == 0 && m_CheckersPlayer2.LegalJumpList.Count == 0)
                {
                    GameStatus = eGameStatus.Tie;
                }
                else if (m_CheckersPlayer1.PawnList.Count == 0 || m_CheckersPlayer1.LegalJumpList.Count == 0)
                {
                    GameStatus = eGameStatus.Player2Won;
                    updateEndGamePlayersTotalRank();
                }
                else if (m_CheckersPlayer2.PawnList.Count == 0 || m_CheckersPlayer2.LegalJumpList.Count == 0)
                {
                    GameStatus = eGameStatus.Player1Won;
                    updateEndGamePlayersTotalRank();
                }
            }
        }

        private bool isCaptureJump(Pawn i_Pawn, Square i_Dest)
        {
            Square squareBetween;
            bool isCaptureingOccurred = false;
            int rowCaptureingStepSize = (int)i_Pawn.Direction * 2;

            if (i_Dest.IsEmpty() == true && Math.Abs(i_Pawn.OnSquare.Col - i_Dest.Col) == 2)
            {
                if ((i_Pawn.Type == ePawnType.King && Math.Abs(i_Pawn.OnSquare.Row - i_Dest.Row) == 2) ||
                    (i_Pawn.Type == ePawnType.Regular && i_Pawn.OnSquare.Row + rowCaptureingStepSize == i_Dest.Row))
                {
                    squareBetween = findSquareBetween(i_Pawn.OnSquare, i_Dest);
                    isCaptureingOccurred = (!squareBetween.IsEmpty()) && (i_Pawn.Belong != squareBetween.Pawn.Belong);
                }
            }

            return isCaptureingOccurred;
        }

        private bool isNormalJump(Pawn i_Pawn, Square i_Dest)
        {
            bool isNormalJump = false;

            if ((i_Dest.IsEmpty() == true) && (Math.Abs(i_Pawn.OnSquare.Col - i_Dest.Col) == 1))
            {
                if (i_Pawn.Type == ePawnType.King)
                {
                    isNormalJump = Math.Abs(i_Pawn.OnSquare.Row - i_Dest.Row) == 1;
                }
                else
                {
                    isNormalJump = i_Pawn.OnSquare.Row + (int)i_Pawn.Direction == i_Dest.Row;
                }
            }

            return isNormalJump;
        }

        private void associatePawnsToPlayer(Player i_Player)
        {
            int rowFrom, rowTo;

            if (i_Player.ID == ePlayerID.Player1)
            {
                rowFrom = 0;
                rowTo = (m_Board.Size / 2) - 1;
            }
            else
            {
                rowFrom = (m_Board.Size / 2) + 1;
                rowTo = m_Board.Size;
            }

            for (int row = rowFrom; row < rowTo; row++)
            {
                for (int col = (row + 1) % 2; col < m_Board.Size; col += 2)
                {
                    i_Player.AddPawn(m_Board.GetPawnByPosition(row, col));
                }
            }
        }

        private void executeJump(Pawn i_Pawn, Square i_Dest)
        {
            eJumpType jumpType = pawnJumpType(i_Pawn, i_Dest);

            if (jumpType == eJumpType.Normal)
            {
                m_LastJumpType = eJumpType.Normal;
            }
            else if (jumpType == eJumpType.Capture)
            {
                m_LastJumpType = eJumpType.Capture;
                m_IsPlayerInCapturingSequence = true;
            }

            m_ActivePlayer.LastActivatedPawn = i_Pawn;
            jumpPawn(i_Pawn, i_Dest, jumpType);
            updateLegalJumpList();
            checkAndChangeTurn();
        }

        private void jumpPawn(Pawn i_Pawn, Square i_Dest, eJumpType i_StepType)
        {
            if (i_StepType == eJumpType.Capture)
            {
                Square capturedSquare = findSquareBetween(i_Pawn.OnSquare, i_Dest);
                Pawn capturedPawn = capturedSquare.Pawn;

                if (capturedPawn.Belong == ePlayerID.Player1)
                {
                    m_CheckersPlayer1.RemovePawn(capturedPawn);
                }
                else
                {
                    m_CheckersPlayer2.RemovePawn(capturedPawn);
                }

                capturedSquare.Pawn = null;
            }

            i_Pawn.OnSquare.Pawn = null;
            i_Pawn.OnSquare = i_Dest;
            checkAndBecomesKingPawn(m_ActivePlayer.LastActivatedPawn);
            i_Dest.Pawn = i_Pawn;
        }

        private void updateLegalJumpList()
        {
            int[] rowOffsetsArr, colOffsetsArr;
            Square dest;
            LegalJump legalJump;

            m_ActivePlayer.LegalJumpList.Clear();
            updateCaptureIsAvailable();

            foreach (Pawn pawn in m_ActivePlayer.PawnList)
            {
                pawn.ClearLegalJumpList();
                setRowAndColOffsetsByPawnType(pawn, out rowOffsetsArr, out colOffsetsArr);

                for (int index = 0; index < rowOffsetsArr.Length; index++)
                {
                    dest = m_Board.GetSquareByPosition(
                        pawn.OnSquare.Row + rowOffsetsArr[index],
                        pawn.OnSquare.Col + colOffsetsArr[index]);

                    if (isValidJump(pawn, dest))
                    {
                        legalJump = new LegalJump(pawn, dest);
                        pawn.AddLegalJump(legalJump);
                        m_ActivePlayer.LegalJumpList.Add(legalJump);
                    }
                }
            }
        }

        private void updateCaptureIsAvailable()
        {
            m_ActivePlayer.IsCaptureAvailable = false;

            foreach (Pawn pawn in m_ActivePlayer.PawnList)
            {
                if (isPossibleToCapture(pawn) == true)
                {
                    m_ActivePlayer.IsCaptureAvailable = true;
                }
            }
        }

        private bool isPossibleToCapture(Pawn i_Pawn)
        {
            int[] rowOffsetsArr, colOffsetsArr;
            bool isPossibleToCapture = false;
            Square dest;

            setRowAndColOffsetsByPawnType(i_Pawn, out rowOffsetsArr, out colOffsetsArr);

            for (int index = 0; index < rowOffsetsArr.Length; index++)
            {
                dest = m_Board.GetSquareByPosition(
                    i_Pawn.OnSquare.Row + rowOffsetsArr[index],
                    i_Pawn.OnSquare.Col + colOffsetsArr[index]);

                if (dest != null && pawnJumpType(i_Pawn, dest) == eJumpType.Capture)
                {
                    isPossibleToCapture = true;
                }
            }

            return isPossibleToCapture;
        }

        private void checkAndBecomesKingPawn(Pawn i_Pawn)
        {
            if (i_Pawn.Type == ePawnType.Regular && ((i_Pawn.Belong == ePlayerID.Player1 && i_Pawn.OnSquare.Row == BoardSize - 1) ||
                (i_Pawn.Belong == ePlayerID.Player2 && i_Pawn.OnSquare.Row == 0)))
            {
                i_Pawn.Type = ePawnType.King;
                m_ActivePlayer.CurrentGameRank += k_KingPawnRank - k_RegularPawnRank;
                m_IsLastJumpPawnBecomesKing = true;
            }
        }

        private void setRowAndColOffsetsByPawnType(Pawn i_Pawn, out int[] i_RowOffsetsArr, out int[] i_ColOffsetsArr)
        {
            if (i_Pawn.Type == ePawnType.King)
            {
                i_RowOffsetsArr = new int[] { 1, 1, 2, 2, -1, -1, -2, -2 };
                i_ColOffsetsArr = new int[] { 1, -1, 2, -2, 1, -1, 2, -2 };
            }
            else if (m_ActivePlayer.ID == ePlayerID.Player1)
            {
                i_RowOffsetsArr = new int[] { 1, 1, 2, 2 };
                i_ColOffsetsArr = new int[] { 1, -1, 2, -2 };
            }
            else
            {
                i_RowOffsetsArr = new int[] { -1, -1, -2, -2 };
                i_ColOffsetsArr = new int[] { 1, -1, 2, -2 };
            }
        }

        private Square findSquareBetween(Square i_Source, Square i_Dest)
        {
            int row = Math.Abs((i_Source.Row + i_Dest.Row) / 2);
            int col = Math.Abs((i_Source.Col + i_Dest.Col) / 2);

            return m_Board.GetSquareByPosition(row, col);
        }

        protected virtual void OnGameStatusChanged()
        {
            if (GameStatusChanged != null)
            {
                GameStatusChanged.Invoke();
            }
        }
    }
}