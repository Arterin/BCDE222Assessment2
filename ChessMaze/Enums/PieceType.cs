namespace ChessMaze.Enums
{

    /// <summary>
    /// Represents the types of chess pieces in the game.
    /// </summary>
    public enum PieceType
    {
        // Use ASCII values for the chess pieces

        /// <summary>
        /// Represents an empty square on the chessboard.
        /// </summary>
        Empty = 'E',

        /// <summary>
        /// Represents the King chess piece.
        /// </summary>
        King = 'K',

        /// <summary>
        /// Represents the Rook chess piece.
        /// </summary>
        Rook = 'R',

        /// <summary>
        /// Represents the Bishop chess piece.
        /// </summary>
        Bishop = 'B',

        /// <summary>
        /// Represents the Knight chess piece.
        /// </summary>
        Knight = 'N',

        /// <summary>
        /// Represents the Pawn chess piece.
        /// </summary>
        Pawn = 'P'
    }
}