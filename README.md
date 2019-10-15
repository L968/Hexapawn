# Hexapawn

<pre>
      a    b    c 
   +--------------+
 1 | K1 | K2 | K3 |
   +--------------+
 2 |    |    |    |
   +--------------+
 3 | P1 | P2 | P3 |
   +--------------+
</pre>

A 3x3 chess board containing 3 pawns that has the same chess move rules on each side of the board for each player.<br>
The Player (P) will play against a Bot(K) that each game, will learn with it's mistakes and improve it's performance.<br><br>
There are 3 ways to win:<br>
1- Get a pawn to the other side of the board<br>
2- Take all of your opponent's pieces<br>
3- Leave your opponent without a possible move<br>

We are using SQLite in order to store the moves. The .db3 files are saved in the %appdata% folder in the Hexapawn folder.<br>
In the project, there's a .drawio file containig all the possible moves that can be accessed on https://www.draw.io
