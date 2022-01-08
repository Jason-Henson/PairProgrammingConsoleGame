using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            {"Jack of Spades", 10},
            {"Queen of Spades", 10},
            {"King of Spades", 10},

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
            {"Jack of Clubs", 10},
            {"Queen of Clubs", 10},
            {"King of Clubs", 10},

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
            {"Jack of Hearts", 10},
            {"Queen of Hearts", 10},
            {"King of Hearts", 10},


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
            {"10 of Diamonds", 10},
            {"Jack of Diamonds", 10},
            {"Queen of Diamonds", 10},
            {"King of Diamonds", 10}
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
                "Welcome to Jason's BlackJack Game\n" +
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
                              $"\n" +
                              "How many dollars worth of chips would you like to buy?\n" +
                              "\n" +
                              "Max amount is $1000\n" +
                              "\n" +
                              "In even dollar amounts.\n");
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
                                  $"\n" +
                                  $"You cannot purchase more than $1000 worth of chips.\n" +
                                  $"\n" +
                                  $"We are a small humble operation here...\n");
            }
            else
            {
                _playerChips = _playerCash;
                _playerCash = 0;
                Console.Clear();
                Console.WriteLine($"OK {_playerName} you now have ${_playerChips} in chips.\n" +
                                  $"\n" +
                                  $"Please press any key to continue.");
                Console.ReadKey();
                PlayGame();
            }
        }

        private void ResetCount()
        {
            playerTotal = 0;
            dealerTotal = 0;
            dealerDownCard = 0;
            dealerUpCard = 0;
            playerCard1 = 0;
            playerCard2 = 0;
            _betAmount = 0;
            _placedBet = 0;
        }

        private void PlayGame()
        {
            ResetCount();
            Console.Clear();
            if (_playerChips <= 0)
            {
                BuyChips();
            }
            else
            {
                try
                {
                    Console.WriteLine($"{_playerName} please place your bet!\n" +
                                      $"\n" +
                                      $"You have ${_playerChips} in chips.\n" +
                                      $"\n" +
                                      "Max bet is $100 and the  Min is $10.\n");
                    _betAmount = int.Parse(Console.ReadLine());
                    _placedBet = _betAmount;
                    _betAmount = 0;
                    DealCards();
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Please ensure you are entering a whole number\n" +
                                      "\n" +
                                      "between 100 and 1000. Your response may not be blank\n" +
                                      "\n" +
                                      "or contain any non-numeric values.\n" +
                                      "\n" +
                                      "Please press any key to continue.");
                    Console.ReadKey();
                    PlayGame();
                }
            }
        }

        private void DealCards()
        {
            Console.Clear();
            Console.WriteLine("Dealing cards...\n" +
                              "\n");
            Thread.Sleep(2500);

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
                Console.WriteLine("Black Jack!!! well done friend.\n" +
                                  "\n");
                Console.WriteLine("Please press any key to continue" +
                                  "\n");
                Console.ReadKey();
                StartMenu();
            }
            else if (dealerTotal == 21)
            {
                Console.WriteLine("Ohh so sorry friend the house wins... another round?\n" +
                                  "\n");
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
            if (dealerTotal >= 17 && dealerTotal < 21)
            {
                DealAnotherCardForPlayer();
            }
            else if (dealerTotal <= 15)
            {
                var rngDealAnotherCardDealer = new Random();
                var index = rngDealAnotherCardDealer.Next(_deckOfCards.Count);
                KeyValuePair<string, int> dealerCard = _deckOfCards.ElementAt(index);

                if (dealerCard.Value == 11 && (dealerTotal + 11) > 21)
                {
                    dealerTotal += 1;
                    DealAnotherCardForPlayer();
                }
                else
                {
                    dealerTotal += dealerCard.Value;
                    BustChecker();
                    DealAnotherCardForPlayer();
                }
            }
        }

        private void DealAnotherCardForPlayer()
        {
            var rngDealAnotherCardPlayer = new Random();
            var index = rngDealAnotherCardPlayer.Next(_deckOfCards.Count);
            KeyValuePair<string, int> playerCard = _deckOfCards.ElementAt(index);
            if (playerCard.Value == 11 && (playerTotal + 11) > 21)
            {
                playerTotal += 1;
            }
            else
            {
                playerTotal += playerCard.Value;
                BustChecker();
            }
        }

        private void BustChecker()
        {
            if (playerTotal > 21)
            {
                Console.Clear();
                Console.WriteLine($"You have {playerTotal}... Sorry friend that is a bust!\n" +
                                  $"\n" +
                                  $"Press any key to continue");
                ResetCount();
                Console.ReadKey();
                StartMenu();
            }
            else if (dealerTotal > 21)
            {
                Console.Clear();
                Console.WriteLine($"The dealer total is {dealerTotal}. Dealer busts...\n" +
                                  $"\n" +
                                  $"You win!!!.  Well played {_playerName}");
                ResetCount();
                Console.ReadKey();
                StartMenu();
            }
            else
            {
                TakeAnotherCard();
            }
        }

        private void TakeAnotherCard()
        {
            try
            {
                Console.Clear();
                Console.WriteLine($"{_playerName} you have a total of {playerTotal}.\n" +
                                  $"\n" +
                                  $"The dealer is showing {dealerUpCard}.\n" +
                                  $"\n" +
                                  $"Would you like another card? Press 1 for 'yes' or 2 for 'no'.\n");
                var anotherCardResponse = Console.ReadLine();
               

                if (anotherCardResponse == "1")
                {
                    DealAnotherCardForDealer();
                    DealAnotherCardForPlayer();
                }
                else if (anotherCardResponse == "2")
                {
                    WhoWonHand();
                }
                else
                {
                    Console.WriteLine("Sorry we did not get your response.  Please select 1 or 2. Let's try again!");
                    Thread.Sleep(2500);
                    TakeAnotherCard();
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Please choose either 1 or 2.");
                TakeAnotherCard();
            }
        }

        private void WhoWonHand()
        {
            if (dealerTotal == playerTotal)
            {
                Console.Clear();
                Console.WriteLine($"{_playerName} you have {playerTotal} and the dealer has {dealerTotal}... Push");
                Thread.Sleep(2000);
                ResetCount();
                StartMenu();
            }
            else if (dealerTotal > playerTotal)
            {
                Console.Clear();
                _playerChips -= _placedBet;
                _placedBet = 0;
                Console.WriteLine($"You have {playerTotal} and the dealer has {dealerTotal}... sorry {_playerName} the house wins...\n");
                Thread.Sleep(2000);
                ResetCount();
                StartMenu();
            }
            else if (playerTotal > 21)
            {
                Console.Clear();
                _playerChips -= _placedBet;
                _placedBet = 0;
                Console.WriteLine($"You have {playerTotal}... sorry {_playerName} that is a bust!");
                Thread.Sleep(2000);
                ResetCount();
                StartMenu();
            }
            else if (playerTotal > dealerTotal)
            {
                Console.Clear();
                _playerChips += _placedBet;
                _placedBet = 0;
                Console.WriteLine($"{_playerName} you have {playerTotal} and the dealer has {dealerTotal}... well played! You win!");
                Thread.Sleep(2000);
                ResetCount();
                StartMenu();
            }
            else
            {
                ResetCount();
                StartMenu();
            }

        }

        private void EndGame()
        {
            Console.Clear();
            _playerCash = _playerChips;
            _playerChips = 0;
            Console.WriteLine($"Thank you for playing please Jason's BlackJack!!\n" +
                              $"\n" +
                              $"Please press any key to end the game.\n" +
                              $"\n" +
                              $"We cashed you out you cash total is {_playerCash}");
            Console.Clear();
            System.Environment.Exit(0);
        }
    }
}
