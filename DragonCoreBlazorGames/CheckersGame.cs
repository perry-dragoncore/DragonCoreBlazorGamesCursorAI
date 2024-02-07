//namespace DragonCoreBlazorGames;

//public class CheckersGame
//{
//    private const int BOARD_SIZE = 8;

//    public CheckersPiece[][] Board { get; private set; }

//    public CheckersPlayer CurrentPlayer { get; private set; }

//    public CheckersPiece SelectedPiece { get; private set; }

//    public List<CheckersMove> AvailableMoves { get; private set; }

//    public bool IsOver { get; private set; }

//    public string EndMessage { get; private set; }

//    public CheckersGame()
//    {
//        InitializeBoard();
//        CurrentPlayer = CheckersPlayer.Red;
//    }

//    public void MovePiece(CheckersSquare square)
//    {
//        var move = AvailableMoves.FirstOrDefault(m => m.EndRow == square.Piece.Row && m.EndColumn == square.Piece.Column);

//        if (move != null)
//        {
//            PerformMove(move);
//            SwitchPlayer();
//        }
//    }

//    //private List<CheckersMove> GetAvailableMovesForPiece(CheckersPiece piece)
//    //{
//    //    var moves = GetAvailableJumpMovesForPiece(piece);

//    //    if (moves.Count == 0)
//    //    {
//    //        moves = GetAvailableSimpleMovesForPiece(piece);
//    //    }

//    //    return moves;
//    //}
//    public List<CheckersMove> GetAvailableMovesForPiece(CheckersPiece piece)
//    {
//        var moves = new List<CheckersMove>();

//        if (piece != null)
//        {
//            var jumpMoves = GetAvailableJumpMovesForPiece(piece);
//            if (jumpMoves.Count > 0)
//            {
//                moves = jumpMoves;
//            }
//            else
//            {
//                moves = GetAvailableSimpleMovesForPiece(piece);
//            }
//        }

//        return moves;
//    }

//    public void Reset()
//    {
//        InitializeBoard();
//        CurrentPlayer = CheckersPlayer.Red;
//        SelectedPiece = null;
//        AvailableMoves = null;
//        IsOver = false;
//        EndMessage = null;
//    }

//    private void InitializeBoard()
//    {
//        Board = new CheckersPiece[BOARD_SIZE][];
//        for (int row = 0; row < BOARD_SIZE; row++)
//        {
//            Board[row] = new CheckersPiece[BOARD_SIZE];
//            for (int col = 0; col < BOARD_SIZE; col++)
//            {
//                if ((row + col) % 2 == 0)
//                {
//                    if (row < 3)
//                    {
//                        Board[row][col] = new CheckersPiece(CheckersPlayer.Black, row, col);
//                    }
//                    else if (row > 4)
//                    {
//                        Board[row][col] = new CheckersPiece(CheckersPlayer.Red, row, col);
//                    }
//                }
//            }
//        }
//    }

//    private void PerformMove(CheckersMove move)
//    {
//        var startSquare = Board[move.StartRow][move.StartColumn];
//        var endSquare = Board[move.EndRow][move.EndColumn];

//        endSquare.Player = startSquare.Player;
//        startSquare.Player = CheckersPlayer.None;

//        if (move.IsJump)
//        {
//            var capturedSquare = Board[move.CapturedRow][move.CapturedColumn];
//            capturedSquare.Piece = null;

//            var availableJumpMoves = GetAvailableJumpMovesForPiece(endSquare.Piece);
//            if (availableJumpMoves.Count > 0)
//            {
//                AvailableMoves = availableJumpMoves;
//                SelectedPiece = endSquare.Piece;
//                endSquare.IsSelected = true;
//                return;
//            }
//        }

//        // Check for promotion
//        if (endSquare.Piece.IsKingCandidate)
//        {
//            endSquare.Piece.IsKingCandidate = false;
//            endSquare.Piece.IsKing = true;
//        }

//        // Check for game over
//        var opponentPlayer = CurrentPlayer == CheckersPlayer.Red ? CheckersPlayer.Black : CheckersPlayer.Red;
//        //if    {
//        //    IsOver = true;
//        //    EndMessage = $"{CurrentPlayer} wins!";
//        //}
//        //else
//        //{
//        //    AvailableMoves = null;
//        //    SelectedPiece = null;
//        //}
//    }

//    private void SwitchPlayer()
//    {
//        CurrentPlayer = CurrentPlayer == CheckersPlayer.Red ? CheckersPlayer.Black : CheckersPlayer.Red;
//        if (!HasAvailableMoves())
//        {
//            IsOver = true;
//            EndMessage = $"{CurrentPlayer} wins!";
//        }
//    }

//    private bool HasAvailableMoves()
//    {
//        for (int row = 0; row < BOARD_SIZE; row++)
//        {
//            for (int col = 0; col < BOARD_SIZE; col++)
//            {
//                var piece = Board[row][col]?.Piece;
//                if (piece != null && piece.Player == CurrentPlayer)
//                {
//                    var moves = GetAvailableMovesForPiece(piece);
//                    if (moves.Count > 0)
//                    {
//                        return true;
//                    }
//                }
//            }
//        }

//        return false;
//    }

//    private List<CheckersMove> GetAvailableSimpleMovesForPiece(CheckersPiece piece)
//    {
//        var moves = new List<CheckersMove>();

//        int row = piece.Row;
//        int col = piece.Column;

//        int forwardRow = CurrentPlayer == CheckersPlayer.Red ? row - 1 : row + 1;
//        int backwardRow = CurrentPlayer == CheckersPlayer.Red ? row + 1 : row - 1;

//        if (IsValidSquare(forwardRow, col))
//        {
//            if (Board[forwardRow][col] == null)
//            {
//                moves.Add(new CheckersMove(row, col, forwardRow, col, false));
//            }
//            else if (IsValidSquare(backwardRow, col) && Board[forwardRow][col].Piece.Player != CurrentPlayer && Board[backwardRow][col] == null)
//            {
//                moves.Add(new CheckersMove(row, col, backwardRow, col, true, forwardRow, col));
//            }
//        }

//        if (piece.IsKing)
//        {
//            if (IsValidSquare(backwardRow, col))
//            {
//                if (Board[backwardRow][col] == null)
//                {
//                    moves.Add(new CheckersMove(row, col, backwardRow, col, false));
//                }
//                else if (IsValidSquare(forwardRow, col) && Board[backwardRow][col].Piece.Player != CurrentPlayer && Board[forwardRow][col] == null)
//                {
//                    moves.Add(new CheckersMove(row, col, forwardRow, col, true, backwardRow, col));
//                }
//            }

//            int leftCol = col - 1;
//            int rightCol = col + 1;

//            if (IsValidSquare(forwardRow, leftCol))
//            {
//                if (Board[forwardRow][leftCol] == null)
//                {
//                    moves.Add(new CheckersMove(row, col, forwardRow, leftCol, false));
//                }
//                else if (IsValidSquare(backwardRow, rightCol) && Board[forwardRow][leftCol].Piece.Player != CurrentPlayer && Board[backwardRow][rightCol] == null)
//                {
//                    moves.Add(new CheckersMove(row, col, backwardRow, rightCol, true, forwardRow, leftCol));
//                }
//            }

//            if (IsValidSquare(forwardRow, rightCol))
//            {
//                if (Board[forwardRow][rightCol] == null)
//                {
//                    moves.Add(new CheckersMove(row, col, forwardRow, rightCol, false));
//                }
//                else if (IsValidSquare(backwardRow, leftCol) && Board[forwardRow][rightCol].Piece.Player != CurrentPlayer && Board[backwardRow][leftCol]
//                                    == null)
//                {
//                    moves.Add(new CheckersMove(row, col, backwardRow, leftCol, true, forwardRow, rightCol));
//                }
//            }
//        }

//        return moves;
//    }

//    private List<CheckersMove> GetAvailableJumpMovesForPiece(CheckersPiece piece)
//    {
//        var moves = new List<CheckersMove>();

//        int row = piece.Row;
//        int col = piece.Column;

//        int forwardRow = CurrentPlayer == CheckersPlayer.Red ? row - 2 : row + 2;
//        int backwardRow = CurrentPlayer == CheckersPlayer.Red ? row + 2 : row - 2;

//        int leftCol = col - 2;
//        int rightCol = col + 2;

//        if (IsValidSquare(forwardRow, leftCol))
//        {
//            if (Board[forwardRow][leftCol] == null && Board[row - 1][col - 1]?.Piece?.Player == GetOpponent(CurrentPlayer))
//            {
//                moves.Add(new CheckersMove(row, col, forwardRow, leftCol, true, row - 1, col - 1));
//            }
//        }

//        if (IsValidSquare(forwardRow, rightCol))
//        {
//            if (Board[forwardRow][rightCol] == null && Board[row - 1][col + 1]?.Piece?.Player == GetOpponent(CurrentPlayer))
//            {
//                moves.Add(new CheckersMove(row, col, forwardRow, rightCol, true, row - 1, col + 1));
//            }
//        }

//        if (piece.IsKing)
//        {
//            if (IsValidSquare(backwardRow, leftCol))
//            {
//                if (Board[backwardRow][leftCol] == null && Board[row + 1][col - 1]?.Piece?.Player == GetOpponent(CurrentPlayer))
//                {
//                    moves.Add(new CheckersMove(row, col, backwardRow, leftCol, true, row + 1, col - 1));
//                }
//            }

//            if (IsValidSquare(backwardRow, rightCol))
//            {
//                if (Board[backwardRow][rightCol] == null && Board[row + 1][col + 1]?.Piece?.Player == GetOpponent(CurrentPlayer))
//                {
//                    moves.Add(new CheckersMove(row, col, backwardRow, rightCol, true, row + 1, col + 1));
//                }
//            }
//        }

//        return moves;
//    }

//    //private List<CheckersMove> GetAvailableMovesForPiece(CheckersPiece piece)
//    //{
//    //    var moves = GetAvailableJumpMovesForPiece(piece);

//    //    if (moves.Count == 0)
//    //    {
//    //        moves = GetAvailableSimpleMovesForPiece(piece);
//    //    }

//    //    return moves;
//    //}

//    private bool IsValidSquare(int row, int col)
//    {
//        return row >= 0 && row < BOARD_SIZE && col >= 0 && col < BOARD_SIZE;
//    }

//    private CheckersPlayer GetOpponent(CheckersPlayer player)
//    {
//        return player == CheckersPlayer.Red ? CheckersPlayer.Black : CheckersPlayer.Red;
//    }
//}

//public enum CheckersPlayer
//{
//    None,
//    Red,
//    Black
//}

//public class CheckersPiece
//{
//    public CheckersPlayer Player { get; set; }
//    public bool IsKing { get; set; }
//    public int Row { get; set; }
//    public int Column { get; set; }
//}

//public class CheckersSquare
//{
//    public CheckersPiece Piece { get; set; }
//    public bool IsBlack { get; set; }
//}

//public class CheckersMove
//{
//    public int StartRow { get; set; }
//    public int StartColumn { get; set; }
//    public int EndRow { get; set; }
//    public int EndColumn { get; set; }
//    public bool IsJump { get; set; }
//    public int JumpRow { get; set; }
//    public int JumpColumn { get; set; }

//    public CheckersMove(int startRow, int startColumn, int endRow, int endColumn, bool isJump, int jumpRow = -1, int jumpColumn = -1)
//    {
//        StartRow = startRow;
//        StartColumn = startColumn;
//        EndRow = endRow;
//        EndColumn = endColumn;
//        IsJump = isJump;
//        JumpRow = jumpRow;
//        JumpColumn = jumpColumn;
//    }
//}