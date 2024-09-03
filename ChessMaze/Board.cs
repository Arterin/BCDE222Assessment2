using ChessMaze.Enums;
using System;

public class Board : IBoard
{
    private readonly int rows;
    private readonly int columns;
    private readonly IPiece[,] cells;

    public Board(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
        cells = new IPiece[rows, columns];

        // Initialize all cells to be empty
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                cells[row, col] = new Piece(PieceType.Empty);
            }
        }
    }

    public int Rows => rows;

    public int Columns => columns;

    public IPiece[,] Cells => cells;

    public IPiece GetPieceAt(IPosition position)
    {
        if (!IsValidPosition(position))
        {
            throw new ArgumentException("Invalid position");
        }
        return cells[position.Row, position.Column];
    }

    public bool IsMoveLegal(IPosition from, IPosition to)
    {
        if (!IsValidPosition(from) || !IsValidPosition(to))
        {
            return false;
        }

        IPiece piece = GetPieceAt(from);
        if (piece.Type == PieceType.Empty)
        {
            return false;
        }

        switch (piece.Type)
        {
            case PieceType.King:
                return IsKingMoveLegal(from, to);
            case PieceType.Rook:
                return IsRookMoveLegal(from, to);
            case PieceType.Bishop:
                return IsBishopMoveLegal(from, to);
            case PieceType.Knight:
                return IsKnightMoveLegal(from, to);
            case PieceType.Pawn:
                return IsPawnMoveLegal(from, to);
            default:
                return false;
        }
    }

    private bool IsKingMoveLegal(IPosition from, IPosition to)
    {
        int rowDiff = Math.Abs(from.Row - to.Row);
        int colDiff = Math.Abs(from.Column - to.Column);
        return rowDiff <= 1 && colDiff <= 1;
    }

    private bool IsRookMoveLegal(IPosition from, IPosition to)
    {
        if (from.Row != to.Row && from.Column != to.Column)
        {
            return false;
        }

        if (from.Row == to.Row)
        {
            int colStep = from.Column < to.Column ? 1 : -1;
            for (int col = from.Column + colStep; col != to.Column; col += colStep)
            {
                if (GetPieceAt(new Position(from.Row, col)).Type != PieceType.Empty)
                {
                    return false;
                }
            }
        }
        else
        {
            int rowStep = from.Row < to.Row ? 1 : -1;
            for (int row = from.Row + rowStep; row != to.Row; row += rowStep)
            {
                if (GetPieceAt(new Position(row, from.Column)).Type != PieceType.Empty)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool IsBishopMoveLegal(IPosition from, IPosition to)
    {
        int rowDiff = Math.Abs(from.Row - to.Row);
        int colDiff = Math.Abs(from.Column - to.Column);
        if (rowDiff != colDiff)
        {
            return false;
        }

        int rowStep = from.Row < to.Row ? 1 : -1;
        int colStep = from.Column < to.Column ? 1 : -1;
        for (int i = 1; i < rowDiff; i++)
        {
            if (GetPieceAt(new Position(from.Row + i * rowStep, from.Column + i * colStep)).Type != PieceType.Empty)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsKnightMoveLegal(IPosition from, IPosition to)
    {
        int rowDiff = Math.Abs(from.Row - to.Row);
        int colDiff = Math.Abs(from.Column - to.Column);
        return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
    }

    private bool IsPawnMoveLegal(IPosition from, IPosition to)
    {
        int rowDiff = to.Row - from.Row;
        int colDiff = Math.Abs(from.Column - to.Column);

        if (colDiff == 0)
        {
            // Moving forward
            if (rowDiff == 1 && GetPieceAt(to).Type == PieceType.Empty)
            {
                return true;
            }
            if (rowDiff == 2 && from.Row == 1 && GetPieceAt(to).Type == PieceType.Empty && GetPieceAt(new Position(from.Row + 1, from.Column)).Type == PieceType.Empty)
            {
                return true;
            }
        }
        else if (colDiff == 1 && rowDiff == 1)
        {
            // Capturing diagonally
            if (GetPieceAt(to).Type != PieceType.Empty)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsValidPosition(IPosition position)
    {
        return position.Row >= 0 && position.Row < rows && position.Column >= 0 && position.Column < columns;
    }

    public void MovePiece(IPosition from, IPosition to)
    {
        if (!IsMoveLegal(from, to))
        {
            throw new InvalidOperationException("Move is not legal");
        }

        IPiece piece = GetPieceAt(from);
        cells[to.Row, to.Column] = piece;
        cells[from.Row, from.Column] = new Piece(PieceType.Empty);
    }

    public void PlacePiece(IPiece piece, IPosition position)
    {
        if (!IsValidPosition(position))
        {
            throw new ArgumentException("Invalid position");
        }

        cells[position.Row, position.Column] = piece;
    }

    public void RemovePiece(IPosition position)
    {
        if (!IsValidPosition(position))
        {
            throw new ArgumentException("Invalid position");
        }

        cells[position.Row, position.Column] = new Piece(PieceType.Empty);
    }
}
