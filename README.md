# fruits-basket
Fruits basket game (interview question)


The goal of the game is to guess the weight of a fruit basket.
The weight of the basket will be between 40 – 140 kilos. (Whole numbers) 
Rules:
  The game ends when a player identifies the weight correctly or when 100 attempts were completed. 
  The game has 5 types of players:
    1) Random player: guesses a random number between 40 and 140.
    2) Memory player: guesses a random number between 40 and 140 but does not try the same number more than once.
    3) Thorough player: tries all numbers by order – 41,42,43...
    4) Cheater player: guesses a random number between 40 and 140 – but does not try any of the numbers that other players had already tried.
    5) Thorough Cheater player: tries all numbers by order – 41,42,43... but skips numbers that were already been tried before by any of the players.

If a player guessed a number incorrectly – he will have to wait the absolute delta (between the real weight and his guess) in milliseconds.

For an example: If the actual weight of the basket is 100 – and a player guessed 70 – the player will wait (sleep) for 30 milliseconds.
                If his guess was 130 – he will also sleep for 30 milliseconds.

Inputs:
  1) The number of participating players – 2 through 8
  2) For each player – his name and his type.

Outputs:
  1) The real weight of the basket.
  2) At the end of the game:
    a) If there was a winner – his name and total amount of attempts in the game.
    b) In case there was no winner – the name of the player who was the closest (in absolute value) and his guess.
       If there were more than one – the one that was the first. Also, his guess should be printed as well.
Bonus:
  Finish the game not only if there were 100 attempts but also if 1500 milliseconds passed.
