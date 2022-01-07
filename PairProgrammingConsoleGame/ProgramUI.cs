using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pair_Programming_Console_Game
{
    internal class ProgramUI
    {
        string _playerName = "Jason";
        int _playerCash = 0;
        int _playerChips = 1000;

        int _betAmount = 0;
        int _placedBet = 0;

        int dealerTotal = 0;
        int playerTotal = 0;

        int dealerDownCard = 0;
        int dealerUpCard = 0;
        int playerCard1 = 0;
        int playerCard2 = 0;

        List<string> playerHand = new List<string>();
        List<string> dealerHand = new List<string>();

        readonly Dictionary<string, int> _deckOfCards = new Dictionary<string, int>()
        {
            {"Ace of Spades", 11},
            {"1 of Spades", 1},
            {"2 of Spades", 2},
            {"3 of Spades", 3},
            {"4 of Spades", 4},
            {"5 of Spades", 5},
            {"6 of Spades", 6},
            {"7 of Spades", 7},
            {"8 of Spades", 8},
            {"9 of Spades", 9},
            {"10 of Spades", 10},

            {"Ace of Clubs", 11},
            {"1 of Clubs", 1},
            {"2 of Clubs", 2},
            {"3 of Clubs", 3},
            {"4 of Clubs", 4},
            {"5 of Clubs", 5},
            {"6 of Clubs", 6},
            {"7 of Clubs", 7},
            {"8 of Clubs", 8},
            {"9 of Clubs", 9},
            {"10 of Clubs", 10},

            {"Ace of Hearts", 11},
            {"1 of Hearts", 1},
            {"2 of Hearts", 2},
            {"3 of Hearts", 3},
            {"4 of Hearts", 4},
            {"5 of Hearts", 5},
            {"6 of Hearts", 6},
            {"7 of Hearts", 7},
            {"8 of Hearts", 8},
            {"9 of Hearts", 9},
            {"10 of Hearts", 10},

            {"Ace of Diamonds", 11},
            {"1 of Diamonds", 1},
            {"2 of Diamonds", 2},
            {"3 of Diamonds", 3},
            {"4 of Diamonds", 4},
            {"5 of Diamonds", 5},
            {"6 of Diamonds", 6},
            {"7 of Diamonds", 7},
            {"8 of Diamonds", 8},
            {"9 of Diamonds", 9},
            {"10 of Diamonds", 10}
        };

        public void Run()
        {
            ShowMenu();
        }

        private void StartMenu()
        {
            Console.Clear();
            Console.WriteLine
            (
                "Jason's BlackJack Game\n" +
                "Enter the option below to get started!\n" +
                    "1. Deal me in!\n" +
                    "2. Nah maybe next time..\n"
            );
        }

        private void ShowMenu()
        {
            Console.Clear();
            StartMenu();
            bool continueToRun = true;
            while (continueToRun)
            {
                string userMenuSelection = Console.ReadLine();
                switch (userMenuSelection)
                {
                    case "1":
                        PlayGame();
                        break;
                    case "2":
                        EndGame();
                        break;
                    default:
                        EndGame();
                        break;
                }
            }
        }

        private void BuyChips()
        {
            Console.Clear();
            Console.WriteLine("Please enter your name.");
            _playerName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Thanks {_playerName}!!!!\n" +
                              "How many dollars worth of chips would you like to buy?\n" +
                              "Max amount is $1000\n" +
                              "In even dollar amounts.");
            _playerCash = int.Parse(Console.ReadLine());
            if (_playerCash.GetType() != typeof(int))
            {
                Console.WriteLine("Please enter a amount between $100 and $1000.");
            }
            else if (_playerCash < 100)
            {
                Console.WriteLine("You must purchase at least $100 worth of chips.");
            }
            else if (_playerCash > 1000)
            {
                Console.WriteLine($"Sorry {_playerName}!\n" +
                                  $"You cannot purchase more than $1000 worth of chips.\n" +
                                  $"We are a small humble operation here...");
            }
            else
            {
                _playerChips = _playerCash;
                _playerCash = 0;
                Console.Clear();
                Console.WriteLine($"OK {_playerName} you now have ${_playerChips} in chips.\n" +
                                  $"Please press any key to continue.");
                Console.ReadKey();
                PlayGame();
            }
        }

        private void PlayGame()
        {
            playerTotal = 0;
            dealerTotal = 0;
            Console.Clear();
            if (_playerChips <= 0)
            {
                BuyChips();
            }
            else
            {
                Console.WriteLine($"{_playerName} please your bet!\n" +
                                  $"You have ${_playerChips}\n" +
                                  "Maximum bet is $100.\n" +
                                  "Minimum bet is $10.");
                _betAmount = int.Parse(Console.ReadLine());
                if (_betAmount.GetType() != typeof(int))
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a bet between $10 and $100.");
                }
                else if (_betAmount < 10)
                {
                    Console.Clear();
                    Console.WriteLine("You must purchase at least $10.");
                }
                else if (_betAmount > 100)
                {
                    Console.Clear();
                    Console.WriteLine($"Sorry friend {_playerName}!\n" +
                                      "The Maximum bet amount per hand is $100.\n" +
                                      "We are a small humble operation here...");
                }
                else
                {
                    _placedBet = _betAmount;
                    DealCards();
                }
            }
        }

        private void DealCards()
        {
            Console.Clear();

            var rngDealer = new Random();
            for (var j = 0; j < 2; j++)
            {
                var index = rngDealer.Next(_deckOfCards.Count);
                KeyValuePair<string, int> card = _deckOfCards.ElementAt(index);
                dealerHand.Add(card.ToString());
                if (j == 0)
                {
                    dealerDownCard = card.Value;
                    dealerTotal = dealerTotal + dealerDownCard;
                }
                else if (j == 1)
                {
                    dealerUpCard = card.Value;
                    dealerTotal = dealerTotal + dealerUpCard;
                }
            }

            var rngPlayer = new Random();
            for (var i = 0; i < 2; i++)
            {
                /*var index = rngPlayer.Next(_deckOfCards.Count);
                KeyValuePair<string, int> playerCard = _deckOfCards.ElementAt(index);
                playerHand.Add(playerCard.ToString());
                playerTotal = playerTotal + playerCard.Value;*/

                var index = rngPlayer.Next(_deckOfCards.Count);
                KeyValuePair<string, int> card = _deckOfCards.ElementAt(index);
                dealerHand.Add(card.ToString());
                if (i == 0)
                {
                    playerCard1 = card.Value;
                    playerTotal = playerCard1;
                }
                else if (i == 1)
                {
                    playerCard2 = card.Value;
                    playerTotal = playerCard1 + playerCard2;
                }


            }

            if (playerTotal == 21)
            {
                Console.WriteLine("Black Jack!!! well done friend.\n");
                Console.WriteLine("Please press any key to continue");
                Console.ReadKey();
                StartMenu();
            }
            else if (dealerTotal == 21)
            {
                Console.WriteLine("Ohh so sorry friend the house wins... another round?\n");
                Console.WriteLine("Please press any key to continue");
                Console.ReadKey();
                StartMenu();
            }
            else
            {
                TakeAnotherCard();
            }

        }

        private void DealAnotherCardForDealer()
        {
            if (dealerTotal <= 15)
            {
                var rngDealAnotherCardDealer = new Random();
                var index = rngDealAnotherCardDealer.Next(_deckOfCards.Count);
                KeyValuePair<string, int> dealerCard = _deckOfCards.ElementAt(index);
                // checking for ace if 11 will case bust ace value is changed to 1
                // Bad logic here. Need to fix 
                if (dealerCard.Value == 11 && (dealerTotal + 11) > 21)
                {
                    dealerTotal = dealerTotal + 1;
                }
                else
                {
                    DealAnotherCardForPlayer();
                }
            }
        }

        private void DealAnotherCardForPlayer()
        {
            var rngDealAnotherCardPlayer = new Random();
            var index = rngDealAnotherCardPlayer.Next(_deckOfCards.Count);
            KeyValuePair<string, int> playerCard = _deckOfCards.ElementAt(index);
            // checking for ace if 11 will case bust ace value is changed to 1
            if (playerCard.Value == 11 && (playerTotal + 11) > 21)
            {
                playerTotal = playerTotal + 1;
            }
            else
            {
                playerTotal = playerTotal + playerCard.Value;
            }

            BustChecker();
        }



        private void BustChecker()
        {
            if (playerTotal > 21)
            {
                Console.WriteLine($"You have {playerTotal}... Sorry friend that is a bust!\n" +
                                  $"Press any key to continue");
                playerTotal = 0;
                dealerTotal = 0;
                Console.ReadKey();
                Console.Clear();
                StartMenu();
            }
            else if (dealerTotal > 21)
            {
                Console.WriteLine($"The dealer total is {dealerTotal}. Dealer busts..." +
                                  $"You win!!!.  Well played {_playerName}");
                playerTotal = 0;
                dealerTotal = 0;
                Console.ReadKey();
                Console.Clear();
                StartMenu();
            }
            else
            {
                DisplayCardScores();
            }
        }

        private void TakeAnotherCard()
        {
            Console.Clear();
            // need to display player and dealer info here again
            Console.WriteLine($"You have {playerTotal}.\n" +
                              $"The dealer is showing {dealerUpCard}.\n" +
                              $"Would you like another card?\n" +
                              $"Press y for 'yes' or n for 'no'.");
            var anotherCardResponse = Console.ReadLine().ToLower();
            switch (anotherCardResponse)
            {
                case "y":
                    {
                        DealAnotherCardForDealer();
                        DealAnotherCardForPlayer();
                    }
                    break;
                case "n":
                    {
                        WhoWonHand();
                    }
                    break;
                default:
                    {
                        Console.Clear();
                        Console.WriteLine("How embarrassing something went wrong... Please press any key to return to the game menu.");
                        Console.ReadKey();
                        StartMenu();
                    }
                    break;
            }
        }

        private void DisplayCardScores()
        {
            Console.Clear();
            Console.WriteLine($"Your total is {playerTotal}.\n");
            Console.WriteLine($"The dealer is showing {dealerUpCard}\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            TakeAnotherCard();
        }

        private void WhoWonHand()
        {
            if (dealerTotal == playerTotal)
            {
                Console.Clear();
                Console.WriteLine($"You have {playerTotal} and the dealer has {dealerTotal}... Push\n" +
                                  $"Press any key to continue.");
                playerTotal = 0;
                dealerTotal = 0;
                Console.ReadKey();
                StartMenu();
            }
            else if (dealerTotal > playerTotal)
            {
                Console.Clear();
                _playerChips = -_placedBet;
                _placedBet = 0;
                Console.WriteLine($"You have {playerTotal} and the dealer has {dealerTotal}... sorry friend the house wins\n" +
                                  $"You have ${_playerChips} in chips.\n" +
                                  $"Press any key to continue.");
                playerTotal = 0;
                dealerTotal = 0;
                Console.ReadKey();
                StartMenu();
            }
            else if (playerTotal > 21)
            {
                Console.Clear();
                _playerChips = -_placedBet;
                _placedBet = 0;
                Console.WriteLine($"You have {playerTotal}... sorry friend that is a bust!\n" +
                                  $"You have ${_playerChips} in chips.\n" +
                                  $"Press any key to continue.");
                playerTotal = 0;
                dealerTotal = 0;
                Console.ReadKey();
                StartMenu();
            }
            else
            {
                Console.Clear();
                _playerChips = +_placedBet;
                _placedBet = 0;
                Console.WriteLine($"You have {playerTotal} and the dealer has {dealerTotal}... well played! You win!\n" +
                                  $"Press any key to continue.");
                playerTotal = 0;
                dealerTotal = 0;
                Console.ReadKey();
                StartMenu();
            }

        }

        private void EndGame()
        {
            Console.Clear();
            _playerCash = _playerChips;
            _playerChips = 0;
            Console.WriteLine($"Thank you for playing please Jason's BlackJack!!\n" +
                              $"Please press any key to end the game.\n" +
                              $"We cashed you out you cash total is {_playerCash}");
            Console.Clear();
            System.Environment.Exit(0);
        }
    }
}
