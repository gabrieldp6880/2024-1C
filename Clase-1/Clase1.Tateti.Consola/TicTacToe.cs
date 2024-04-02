namespace Clase1.Tateti.Consola
{
    public class TicTacToe
    {
        private char[,] board;  // Tablero de juego
        private char currentPlayer; // Jugador actual (X o O)
        private bool gameEnd;  // Indica si el juego ha terminado

        public TicTacToe()
        {
            board = new char[3, 3];
            currentPlayer = 'X';
            gameEnd = false;
        }

        public void jugar()
        {
            InitializeBoard();
            PrintBoard();

            while (!gameEnd)
            {
                PlayTurn();
                PrintBoard();
                CheckGameStatus();
                SwitchPlayer();
            }

            Console.ReadLine();
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = '-';
                }
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine("  0 1 2");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PlayTurn()
        {
            Console.WriteLine($"Turno del jugador {currentPlayer}.");
            Console.Write("Ingresa la fila: ");
            int row = int.Parse(Console.ReadLine());
            Console.Write("Ingresa la columna: ");
            int col = int.Parse(Console.ReadLine());

            if (row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == '-')
            {
                board[row, col] = currentPlayer;
            }
            else
            {
                Console.WriteLine("Movimiento inválido. Intenta de nuevo.");
                PlayTurn();
            }
        }

        public void CheckGameStatus()
        {
            // Revisar filas
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                {
                    Console.WriteLine($"El jugador {currentPlayer} ha ganado.");
                    gameEnd = true;
                    return;
                }
            }

            // Revisar columnas
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == currentPlayer && board[1, j] == currentPlayer && board[2, j] == currentPlayer)
                {
                    Console.WriteLine($"El jugador {currentPlayer} ha ganado.");
                    gameEnd = true;
                    return;
                }
            }

            // Revisar diagonales
            if ((board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) ||
                (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer))
            {
                Console.WriteLine($"El jugador {currentPlayer} ha ganado.");
                gameEnd = true;
                return;
            }

            // Revisar empate
            bool tie = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '-')
                    {
                        tie = false;
                        break;
                    }
                }
                if (!tie)
                    break;
            }

            if (tie)
            {
                Console.WriteLine("¡Empate!");
                gameEnd = true;
            }
        }

        public void SwitchPlayer()
        {
            if (currentPlayer == 'X')
                currentPlayer = 'O';
            else
                currentPlayer = 'X';
        }
    }
}
