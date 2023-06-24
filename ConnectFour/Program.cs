using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;

namespace ConnectFour
{

    /*
    1. Players
        - 2 player mode
        - 1 player mode : ramdom play
        - name, count?,
    2. Game
        - choose the mode: 1 or 2
        - show whose turn
        - show the game board
        - show the result
    3. Column arrays
        - columns name : numbers
        - remember previous locations
    4. Check connect four
        - horizontal
        - vertical
        - diagonal
    5. Interface
        - GetName
        - GetPosition
    6. Classes
        1) Program
        +) Player
        2) HumanPlayer : Player
            - GetName
            - GetPosition : ReadLine
        3) AIPlayer : Player
            - GetName
            - GetPosition : Random
        4) ConnectFourGame
            - PlayerList
            - CreatePlayer
            - ChangeTurn
            - Play
            - CheckHorizontal
            - CheckVertical
            - CheckDiagonal

     */


    public interface IPlayer
    {
        string GetName();
        int GetPosition();
    }

    public class HumanPlayer : IPlayer
    {
        public string Name { get; set; }

        public HumanPlayer(string playerName)
        {
            Name = playerName;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetPosition()
        {
            Console.Write($"Player {Name}, enter the column number to drop your token: ");
            int column = int.Parse(Console.ReadLine());
            return column;
            
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }

    /*
    public class AIPlayer : IPlayer
    {
        public string Name { get; set; }

        public string GetName()
        {
            return Name;
        }

        public int GetPosition()
        {
            Random random = new Random();
            return random.Next(1, 8); // Generate a random column number between 1 and 7 (inclusive)
        }
        public override string ToString()
        {
            return $"${Name}";
        }
    }
    */

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

        public static bool PlayWithHuman()
        {
            bool gameOver = false;
            do
            {
                IPlayer currentPlayer = _playersList[_curPlayer];
                Console.WriteLine($"Now Palying Player:{currentPlayer.GetName()}");
                int column = currentPlayer.GetPosition() - 1;
                if (IsValidMove(column))
                {
                    PutToken(column);
                    /*
                    if (CheckResult() == true)
                    {
                        gameOver = true;
                    }*/
                    DisplayBoard();
                    _curPlayer = (_curPlayer + 1) % _playersList.Count;
                }
                else{
                    Console.WriteLine("Invalid move, try again!");
                }
                
            } while (gameOver==true);

            return gameOver;
        }
        
        public static bool PlayWithAI()
        {
            return true;
        }

        private static void PutToken(int column)
        {
            int row = FindRow(column);
            if (_curPlayer == 0)
            {
                gameBoard[row,column] = 'O';
            }
            else
            {
                gameBoard[row, column] = 'X';
            }
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
            if(column >= gameBoard.GetLength(1)-1)
            {
                return false;
            }
            return true;
        }

        private static bool CheckResult()
        {
            bool result = false;
            Console.WriteLine("Checking the result.");
            //vertical
            //horizontal
            //diagonal
            return result;

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
    }

  
        class Program
    {
        static void Main(string[] args)
        {
            bool restart = false;
            do {
                //when do restart, reset all
                Console.WriteLine("Please select a game mode: \n(1) 1-player mode \n(2) 2-player mode");
                string gameMode = Console.ReadLine();
                if (gameMode == "1")
                {
                    while (!ConnectFourGame.PlayWithAI()) ;
                }
                else if (gameMode == "2")
                {
                    Console.WriteLine("Add the first player Names.");
                    string name;
                    for (int i = 0; i < 2; i++)
                    {
                        Console.Write($"Player {i+1}: ");
                        name = Console.ReadLine();
                        IPlayer player = new HumanPlayer(name);
                        ConnectFourGame.AddPlayer(player);
                    }
                    while (!ConnectFourGame.PlayWithHuman()) ;
                }

                ConnectFourGame.ShowResult();

                Console.WriteLine("Restart? Yes(1) No(0)");
                string continueGame = Console.ReadLine();
                if(continueGame == "1")
                {
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

