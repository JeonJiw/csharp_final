using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;

namespace ConnectFour
{

    public interface IPlayer
    {
        string GetName();
        int GetPosition();
    }

    public abstract class Player : IPlayer
    {
        public string Name { get; protected set; }

        public abstract string GetName();
        public abstract int GetPosition();
    }

    public class HumanPlayer : Player
    {

        public HumanPlayer(string playerName)
        {
            Name = playerName;
        }

        public override string GetName()
        {
            return Name;
        }

        public override int GetPosition()
        {
            Console.Write($"Player {Name}, enter the column number: ");
            int column = int.Parse(Console.ReadLine());
            return column;
            
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }

    
    public class AIPlayer : Player
    {
        public string Name { get; set; }

        public AIPlayer()
        {
            Name = "AI";
        }

        public override string GetName()
        {
            return Name;
        }

        public override int GetPosition()
        {
            Random random = new Random();
            return random.Next(1, 8);
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
    

    public class ConnectFourGame
    {
        public static List<IPlayer> _playersList;
        private static int _curPlayer;
        public static char[,] gameBoard;
        private static int rows;
        private static int columns;

        static ConnectFourGame()
        {
            _playersList = new List<IPlayer>();
            _curPlayer = 0;
            rows = 6;
            columns = 7;
            gameBoard = new char[rows, columns];
        }

        public ConnectFourGame()
        {
        }

        public static void AddPlayer(IPlayer player)
        {
            _playersList.Add(player);
        }

        public static bool PlayConnectFour()
        {
            bool gameOver = false;
            do
            {
                
                IPlayer currentPlayer = _playersList[_curPlayer];
                Console.WriteLine($"Now Palying Player:{currentPlayer.GetName()}");
                int column = currentPlayer.GetPosition() - 1;
                if (IsValidMove(column))
                {
                    int row = PutToken(column);
                    if (CheckResult(row, column) == true)
                    {
                        gameOver = true;
                    }
                    DisplayBoard();
                    if (!gameOver)
                    {
                        _curPlayer = (_curPlayer + 1) % _playersList.Count;
                    }
                }
                else{
                    Console.WriteLine("Invalid move, try again!");
                }

            } while (!gameOver);

            return gameOver;
        }
        

        private static int PutToken(int column)
        {
            int row = FindRow(column);
            if (_curPlayer == 0)
            {
                gameBoard[row, column] = 'O';
            }
            else
            {
                gameBoard[row, column] = 'X';
            }

            return row;
        }

        private static int FindRow(int column)
        {
            for (int row = rows - 1; row >= 0; row--)
            {
                if (gameBoard[row, column] == '\0')
                {
                    return row;
                }
            }
            return -1;
        }

        private static bool IsValidMove(int column)
        {
            Console.WriteLine("Checking the validation.");
            if(column >= gameBoard.GetLength(1))
            {
                return false;
            }
            return true;
        }

        private static bool CheckResult(int row, int column)
        {
            bool result = false;
            Console.WriteLine("Checking the result.");
            //vertical
            if (CheckVertical(row, column))
            {
                result = true;
                return result;
            }
            //horizontal
            if (CheckHorizontal(row, column))
            {
                result = true;
                return result;
            }
            //diagonal
            if (CheckDiagonal(row, column))
            {
                result = true;
                return result;
            }
            return result;

        }

        private static bool CheckDiagonal(int row, int column)
        {
            char symbol = gameBoard[row, column];

            // Check diagonal /
            int count = 1;
            int r = row - 1;
            int c = column + 1;
            while (r >= 0 && c < columns && gameBoard[r, c] == symbol)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
                r--;
                c++;
            }

            r = row + 1;
            c = column - 1;
            while (r < rows && c >= 0 && gameBoard[r, c] == symbol)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
                r++;
                c--;
            }

            // Check diagonal \
            count = 1;
            r = row - 1;
            c = column - 1;
            while (r >= 0 && c >= 0 && gameBoard[r, c] == symbol)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
                r--;
                c--;
            }

            r = row + 1;
            c = column + 1;
            while (r < rows && c < columns && gameBoard[r, c] == symbol)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
                r++;
                c++;
            }

            return false;
        }

        private static bool CheckHorizontal(int row, int column)
        {
            char symbol = gameBoard[row, column];
            int count = 0;
            for (int c = 0; c < columns; c++)
            {
                char curValue = gameBoard[row, c];
                if (gameBoard[row, c] == symbol)
                {
                    if (count == 0 && c > 4)
                    {
                        return false;
                    }
                    else
                    {
                        count++;
                        if (count == 4)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }

        private static bool CheckVertical(int row, int column)
        {
            char symbol = gameBoard[row, column];
            int count = 0;
            for (int r = rows-1; r >=0; r--)
            {
                if (gameBoard[r, column] == symbol)
                {
                    if(count == 0 && r < 3)
                    {
                        return false;
                    }
                    else
                    {
                        count++;
                        if (count == 4)
                        {
                            return true;
                        }
                    }
                }else
                {
                    count = 0;
                }
            }
            return false;
        }
        private static void DisplayBoard()
        {
            for(int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for(int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    char pos = gameBoard[i, j] == '\0' ? '#' : gameBoard[i, j];
                    Console.Write($"{pos} ");
                }
                Console.WriteLine();
            }
        }

        public static void ShowResult()
        {
            Console.WriteLine($"It is a Connect 4.{_playersList[_curPlayer]} Wins!");
        }

        public static void ResetGameBoard()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    gameBoard[row, column] = '\0';
                }
            }
        }
    }

  
        class Program
    {
        static void Main(string[] args)
        {
            bool restart = false;
            do {

                bool debug = false;
                if (debug == true)
                {
                    IPlayer player1 = new HumanPlayer("O");
                    IPlayer player2 = new HumanPlayer("X");
                    ConnectFourGame.AddPlayer(player1);
                    ConnectFourGame.AddPlayer(player2);
                    while (!ConnectFourGame.PlayConnectFour());
                }
                else
                {
                    
                    Console.WriteLine("Please select a game mode: \n(1) 1-player mode \n(2) 2-player mode");
                    string gameMode = Console.ReadLine();
                    if (gameMode == "1")
                    {
                        Console.WriteLine("Add your names.");
                        string name;
                        Console.Write("Player 1: ");
                        name = Console.ReadLine();
                        IPlayer player = new HumanPlayer(name);
                        IPlayer AI = new AIPlayer();
                        Console.WriteLine("Player 2: AI");
                        ConnectFourGame.AddPlayer(player);
                        ConnectFourGame.AddPlayer(AI);

                        while (!ConnectFourGame.PlayConnectFour()) ;
                    }
                    else if (gameMode == "2")
                    {
                        Console.WriteLine("Add the first player name.");
                        string name;
                        for (int i = 0; i < 2; i++)
                        {
                            Console.Write($"Player {i + 1}: ");
                            name = Console.ReadLine();
                            IPlayer player = new HumanPlayer(name);
                            ConnectFourGame.AddPlayer(player);
                        }
                        while (!ConnectFourGame.PlayConnectFour());
                    }
                }
            ConnectFourGame.ShowResult();

            Console.WriteLine("Restart? Yes(1) No(0)");
            string continueGame = Console.ReadLine();
            if(continueGame == "1")
            {
                ConnectFourGame.ResetGameBoard();
                ConnectFourGame._playersList.Clear();
                restart = true;
            }
            else
            {
                restart = false;
            }
            } while (restart == true);
            Console.Read();
            
        }
    }
}

