using System.Windows.Forms;
using System.Drawing;

namespace CheckersWindowsUI
{
    internal class SquareButton : Button
    {
        // Defines
        internal const int k_SquareButtonWidthSize = 45;
        internal const int k_SquareButtonHeightSize = 45;

        // Attributes
        private int m_Row;
        private int m_Col;
        private Color m_DefaultBackColor;

        // Properties
        internal int Row
        {
            get
            {
                return m_Row;
            }
        }

        internal int Col
        {
            get
            {
                return m_Col;
            }
        }

        internal Color DefaultColor
        {
            get
            {
                return m_DefaultBackColor;
            }

            set
            {
                m_DefaultBackColor = value;
            }
        }

        // Methods
        internal SquareButton(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
            this.Size = new Size(k_SquareButtonWidthSize, k_SquareButtonHeightSize);
        }
    }
}
