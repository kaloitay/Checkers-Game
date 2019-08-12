using System.Collections.Generic;
using System;

namespace CheckersLogic
{
    public class Pawn
    {
        // Enums
        internal enum eDirection
        {
            Down = 1,
            Up = -1
        }

        // Attributes
        private ePawnType m_Type;
        private ePlayerID m_Belong;
        private Square m_OnSquare;
        private eDirection m_Direction;
        private List<LegalJump> m_LegalJumps;

        // Properties
        public ePawnType Type
        {
            get
            {
                return m_Type;
            }

            set
            {
                m_Type = value;
            }
        }

        public ePlayerID Belong
        {
            get
            {
                return m_Belong;
            }

            internal set
            {
                m_Belong = value;
            }
        }

        public Square OnSquare
        {
            get
            {
                return m_OnSquare;
            }

            internal set
            {
                m_OnSquare = value;
            }
        }

        internal eDirection Direction
        {
            get
            {
                return m_Direction;
            }

            set
            {
                m_Direction = value;
            }
        }

        internal List<LegalJump> LegalJumps
        {
            get
            {
                return m_LegalJumps;
            }
        }

        // Methods
        internal Pawn(ePawnType i_Type, ePlayerID i_Belong, Square i_OnSquare)
        {
            m_Type = i_Type;
            m_Belong = i_Belong;
            m_OnSquare = i_OnSquare;
            m_Direction = i_Belong == ePlayerID.Player1 ? eDirection.Down : eDirection.Up;
            m_LegalJumps = new List<LegalJump>();
        }

        internal void AddLegalJump(LegalJump i_LegalJump)
        {
            m_LegalJumps.Add(i_LegalJump);
        }

        internal void ClearLegalJumpList()
        {
            m_LegalJumps.Clear();
        }
    }
}
