using System;
using System.Collections.Generic;
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

     */

    public class ConnectFourGame
    {
        public string Name { get; set; }
        public char position;
        public char[,] gameBoard = new char[6, 7];
        //private static Random r; //for the AI
        private static List<ConnectFourGame> _playersList;
        private static int count;

        static ConnectFourGame()
        {
            _playersList = new List<ConnectFourGame>();
            count = 0;
        }
        public ConnectFourGame()
        {
        }
        public ConnectFourGame(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return $"Name: {Name}";
        }
        public static void AddAPlayer(string name)
        {
            var player = new ConnectFourGame
            {
                Name = name
            };
            _playersList.Add(player);
        }

        public static bool Play1()
        {
            return true;
        }
        public static bool Play2()
        {
            Console.WriteLine($"Now Palying Player : {_playersList[count].Name} ");
            Console.WriteLine("Enter a column's number: ");
            string userInput = Console.ReadLine();

            if(userInput == "exit")
            {
                return false;
            } else
            {

            }
            /*
            
            if (userInput > _hiddenNo)
            {
                Console.WriteLine("Guess Lower...");
            }
            else if (userInput < _hiddenNo)
            {
                Console.WriteLine("Guess Heigher...");
            }
            else
            {
                _playersList[count].Score += 10;
                Console.WriteLine($"Congratulations....Player {count + 1}: {_playersList[count].Name}.");
                return false;
            }
            if (Math.Abs(userInput - _hiddenNo) <= 5)
            {
                _playersList[count].Score += 7;
            }
            else if (Math.Abs(userInput - _hiddenNo) <= 10)
            {
                _playersList[count].Score += 5;
            }
            else if (Math.Abs(userInput - _hiddenNo) <= 15)
            {
                _playersList[count].Score += 3;
            }
            else if (Math.Abs(userInput - _hiddenNo) <= 20)
            {
                _playersList[count].Score += 2;
            }
            else
            {
                _playersList[count].Score += 1;
            }
            count = (count + 1) % _playersList.Count;
            */
            return true;
        }
        public static void ShowResult()
        {
            Console.WriteLine($"It is a Connect 4.{_playersList[count].Name} Wins!");
        }
    }

        class Program
    {
        static void Main(string[] args)
        {
            bool restart = false;
            do {
                Console.WriteLine("Please select a game mode: \n(1) 1-player mode \n(2) 2-player mode");
                string gameMode = Console.ReadLine();
                if (gameMode == "1")
                {
                    while (ConnectFourGame.Play1()) ;
                }
                else if (gameMode == "2")
                {
                    Console.WriteLine("Add Player Names.");
                    string name;
                    for (int i = 0; i < 2; i++)
                    {
                        name = Console.ReadLine();
                        ConnectFourGame.AddAPlayer(name);
                    }
                    while (ConnectFourGame.Play2()) ;
                }

                ConnectFourGame.ShowResult();

                Console.WriteLine("Restart? Yes(1) No(0)");
                string continueGame = Console.ReadLine();
                if(continueGame == "1")
                {
                    restart = true;
                }
            } while (restart == true);
            Console.Read();
            
        }
    }
}

