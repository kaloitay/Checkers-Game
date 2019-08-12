using System;

namespace CheckersLogic
{
    public class Square
    {
        // Delegates
        public event Action<Square> SquarePawnChanged;
        
        // Attributes
        private int m_Row;
        private int m_Col;
        private Pawn m_Pawn = null;

        // Properties
        public int Row
        {
            get
            {
                return m_Row;
            }

            internal set
            {
                m_Row = value;
            }
        }

        public int Col
        {
            get
            {
                return m_Col;
            }

            internal set
            {
                m_Col = value;
            }
        }

        public Pawn Pawn
        {
            get
            {
                return m_Pawn;
            }

            internal set
            {
                m_Pawn = value;
                OnSquarePawnChanged();
            }
        }

        // Methods
        internal Square(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public bool IsEmpty()
        {
            return m_Pawn == null;
        }

        protected virtual void OnSquarePawnChanged()
        {
            if (SquarePawnChanged != null)
            {
                SquarePawnChanged.Invoke(this);
            }
        }
    }
}
