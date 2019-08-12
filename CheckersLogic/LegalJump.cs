namespace CheckersLogic
{
    internal class LegalJump
    {
        // Attributes
        private Pawn m_Pawn;
        private Square m_SquareDest;

        // Properties
        internal Pawn Pawn
        {
            get
            {
                return m_Pawn;
            }

            set
            {
                m_Pawn = value;
            }
        }

        internal Square SquareDest
        {
            get
            {
                return m_SquareDest;
            }

            set
            {
                m_SquareDest = value;
            }
        }

        // Methods
        internal LegalJump(Pawn i_Pawn, Square i_Dest)
        {
            m_Pawn = i_Pawn;
            m_SquareDest = i_Dest;
        }
    }
}
