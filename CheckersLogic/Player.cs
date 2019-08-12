using System.Collections.Generic;

namespace CheckersLogic
{
    public class Player
    {
        // Attributes
        private ePlayerID m_ID;
        private ePlayerType m_Type;
        private string m_Name;
        private int m_CurrentGameRank = 0;
        private int m_TotalRank = 0;
        private List<Pawn> m_PawnList;
        private List<LegalJump> m_LegalJumpList;
        private bool m_IsCaptureAvailable = false;
        private Pawn m_LastActivatedPawn;

        // Properties
        public ePlayerID ID
        {
            get
            {
                return m_ID;
            }

            internal set
            {
                m_ID = value;
            }
        }

        public ePlayerType Type
        {
            get
            {
                return m_Type;
            }

            internal set
            {
                m_Type = value;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }

            internal set
            {
                m_Name = value;
            }
        }

        public int CurrentGameRank
        {
            get
            {
                return m_CurrentGameRank;
            }

            internal set
            {
                m_CurrentGameRank = value;
            }
        }

        public int TotalRank
        {
            get
            {
                return m_TotalRank;
            }

            internal set
            {
                m_TotalRank = value;
            }
        }

        internal List<Pawn> PawnList
        {
            get
            {
                return m_PawnList;
            }
        }

        internal List<LegalJump> LegalJumpList
        {
            get
            {
                return m_LegalJumpList;
            }
        }

        internal bool IsCaptureAvailable
        {
            get
            {
                return m_IsCaptureAvailable;
            }

            set
            {
                m_IsCaptureAvailable = value;
            }
        }

        internal Pawn LastActivatedPawn
        {
            get
            {
                return m_LastActivatedPawn;
            }

            set
            {
                m_LastActivatedPawn = value;
            }
        }

        // Methods
        internal Player(ePlayerID i_ID, ePlayerType i_Type, string i_Name, int i_NumberOfPawns)
        {
            m_ID = i_ID;
            m_Type = i_Type;
            m_Name = i_Name;
            m_PawnList = new List<Pawn>(i_NumberOfPawns);
            m_LegalJumpList = new List<LegalJump>();
        }

        internal void AddPawn(Pawn i_Pawn)
        {
            m_CurrentGameRank += ControlPanel.k_RegularPawnRank;
            m_PawnList.Add(i_Pawn);
        }

        internal void RemovePawn(Pawn i_Pawn)
        {
            if (i_Pawn.Type == ePawnType.King)
            {
                m_CurrentGameRank -= ControlPanel.k_KingPawnRank;
            }
            else if (i_Pawn.Type == ePawnType.Regular)
            {
                m_CurrentGameRank -= ControlPanel.k_RegularPawnRank;
            }

            m_PawnList.Remove(i_Pawn);
        }

        internal void RemoveAllPawns()
        {
            foreach (Pawn pawn in m_PawnList)
            {
                pawn.OnSquare = null;
            }

            m_PawnList.Clear();
        }
        
        internal bool IsLegalJump(Pawn i_Pawn, Square i_Dest)
        {
            bool isLegalJump = false;

            foreach (LegalJump legalJump in m_LegalJumpList)
            {
                if (i_Pawn == legalJump.Pawn && i_Dest == legalJump.SquareDest)
                {
                    isLegalJump = true;
                }
            }

            return isLegalJump;
        }
    }
}