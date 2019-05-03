using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn
{
    class Game
    {
        public string[,] GamePawnsPositions { get; private set; }
        public string[] BotPawns { get; private set; }
        public string[] PlayerPawns { get; private set; }
        public string Winner { get; private set; }
        private string SelectedPawn { get; set; }
        private string ActivePlayer { get; set; }

        public Game()
        {
            ActivePlayer = "Player"; // First turn
            Winner = null;
            BotPawns = new string[3] { "K1", "K2", "K3" };
            PlayerPawns = new string[3] { "P1", "P2", "P3" };

            /* 
                 a    b    c
              +--------------+
            1 | K1 | K2 | K3 |
              +--------------+
            2 |    |    |    |
              +--------------+
            3 | P1 | P2 | P3 |
              +--------------+

            */

            // Fixed positions - Position being occupied
            GamePawnsPositions = new string[9, 2] {
                {"A1", BotPawns[0]},     // 0
                {"A2", null},            // 1
                {"A3", PlayerPawns[0]},  // 2
                {"B1", BotPawns[1]},     // 3
                {"B2", null},            // 4
                {"B3", PlayerPawns[1]},  // 5
                {"C1", BotPawns[2]},     // 6
                {"C2", null},            // 7
                {"C3", PlayerPawns[2]}   // 8
            };
        }

        public void Move(string[] move)
        {
            string selectedPawn = move[0]; // e.g: P1, K1
            SelectedPawn = selectedPawn;
            string pawnDestiny = move[1]; // e.g: A2, B2
            int selectedPawnPositionInArray = 0;
            int pawnDestinyPositionInArray = 0;

            IsExistingPawn();
            IsActivePlayerThePawnsOwner();
            IsExistingDestiny(pawnDestiny);

            selectedPawnPositionInArray = GetSelectedPawnPositionInArray();
            pawnDestinyPositionInArray = GetPawnDestinyPositionInArray(pawnDestiny);

            if (IsLegalMove(selectedPawnPositionInArray, pawnDestinyPositionInArray))
            {
                MovePawn(selectedPawnPositionInArray, pawnDestinyPositionInArray);
                CheckWinner();
                SwitchActivePlayer();
            }
            else
            {
                throw new MoveException("Movimento inválido.");
            }
            
        }

        private void IsExistingPawn()
        {
            if (!PlayerPawns.Contains(SelectedPawn) && !BotPawns.Contains(SelectedPawn))
            {
                throw new MoveException("Peça não encontrada");
            }
        }

        private void IsActivePlayerThePawnsOwner()
        {
            if (ActivePlayer == "Player")
            {
                if (!PlayerPawns.Contains(SelectedPawn))
                {
                    throw new MoveException("Apenas selecione suas peças!");
                }
            }
            else
            {
                if (!BotPawns.Contains(SelectedPawn))
                {
                    throw new MoveException("Apenas selecione suas peças!");
                }
            }
        }

        private void IsExistingDestiny(string pawnDestiny)
        {
            for (int i = 0; i < GamePawnsPositions.GetLength(0); i++)
            {
                if (GamePawnsPositions[i, 0] == pawnDestiny)
                {
                    return;
                }
            }
            throw new MoveException("Escolha um destino existente.");
        }

        private int GetSelectedPawnPositionInArray()
        {
            for (int i = 0; i < GamePawnsPositions.GetLength(0); i++)
            {
                if (SelectedPawn == GamePawnsPositions[i, 1])
                {
                    return i;
                }
            }
            throw new MoveException("Erro ao pegar posição da peça no array");
        }

        private int GetPawnDestinyPositionInArray(string pawnDestiny)
        {
            for (int i = 0; i < GamePawnsPositions.GetLength(0); i++)
            {
                if (pawnDestiny == GamePawnsPositions[i, 0])
                {
                    return i;
                }
            }
            throw new MoveException("Erro ao pegar posição do destino no array");
        }        

        private bool IsLegalMove(int selectedPawnPositionInArray, int pawnDestinyPositionInArray)
        {
            // Ilegal moves
            //IsMovingToTheSamePosition(selectedPawnPositionInArray, pawnDestinyPositionInArray); //ok
            //IsMovingSideways(selectedPawnPositionInArray, pawnDestinyPositionInArray); // arrumar default
            //IsMovingBackwards(selectedPawn, selectedPawnPositionInArray, pawnDestinyPositionInArray);
            //IsMovingForward();


            //IsDiagonalMove(selectedPawnPositionInArray, pawnDestinyPositionInArray); // arrumar default
            //HasAPawnInDestinyPosition(selectedPawnPositionInArray);
            return true;
        }

        private bool IsMovingForward(int selectedPawnPositionInArray, int pawnDestinyPositionInArray)
        {
            if (PlayerPawns.Contains(SelectedPawn))
            {
                switch (selectedPawnPositionInArray)
                {
                    case 1: return pawnDestinyPositionInArray == 0;
                    case 2: return pawnDestinyPositionInArray == 1;

                    case 4: return pawnDestinyPositionInArray == 3;
                    case 5: return pawnDestinyPositionInArray == 4;

                    case 7: return pawnDestinyPositionInArray == 6;
                    case 8: return pawnDestinyPositionInArray == 7;
                    default: return false;
                }
            }
            else if (BotPawns.Contains(SelectedPawn))
            {
                switch (selectedPawnPositionInArray)
                {
                    case 0: return pawnDestinyPositionInArray == 1;
                    case 1: return pawnDestinyPositionInArray == 2;

                    case 3: return pawnDestinyPositionInArray == 4;
                    case 4: return pawnDestinyPositionInArray == 5;

                    case 6: return pawnDestinyPositionInArray == 7;
                    case 7: return pawnDestinyPositionInArray == 8;
                    default: return false;
                }
            }
            else
            {
                throw new MoveException("Peça não encontrada.");
            }
        }

        private bool IsMovingToTheSamePosition(int selectedPawnPositionInArray, int pawnDestinyPositionInArray)
        {
            return selectedPawnPositionInArray == pawnDestinyPositionInArray;
        }

        private bool IsMovingSideways(int selectedPawnPositionInArray, int pawnDestinyPositionInArray)
        {
            switch (selectedPawnPositionInArray)
            {
                // A row (left)
                case 0: return pawnDestinyPositionInArray == 3;
                case 1: return pawnDestinyPositionInArray == 4;
                case 2: return pawnDestinyPositionInArray == 5;

                // B row (middle)
                case 3: return pawnDestinyPositionInArray == 0 || pawnDestinyPositionInArray == 6;
                case 4: return pawnDestinyPositionInArray == 1 || pawnDestinyPositionInArray == 7;
                case 5: return pawnDestinyPositionInArray == 2 || pawnDestinyPositionInArray == 8;

                // C row (right)
                case 6: return pawnDestinyPositionInArray == 3;
                case 7: return pawnDestinyPositionInArray == 4;
                case 8: return pawnDestinyPositionInArray == 5;
                default:
                    return true;
            }
        }

        private bool IsMovingBackwards(int selectedPawnPositionInArray, int pawnDestinyPositionInArray)
        {
            if (PlayerPawns.Contains(SelectedPawn))
            {

            }
            else if (BotPawns.Contains(SelectedPawn))
            {

            }
            return false;
        }

        private bool IsDiagonalMove(int selectedPawnPositionInArray, int pawnDestinyPositionInArray)
        {
            switch (selectedPawnPositionInArray)
            {
                // A row (left)
                case 0:
                case 1:
                case 2:
                    return pawnDestinyPositionInArray != 0 && pawnDestinyPositionInArray != 1 && pawnDestinyPositionInArray != 2;

                // B row (middle)
                case 3:
                case 4:
                case 5:
                    return pawnDestinyPositionInArray != 3 && pawnDestinyPositionInArray != 4 && pawnDestinyPositionInArray != 5;

                // C row (right)
                case 6:
                case 7:
                case 8:
                    return pawnDestinyPositionInArray != 6 && pawnDestinyPositionInArray != 7 && pawnDestinyPositionInArray != 8;
                default:
                    return false;
            }
        }

        private bool HasAPawnInDestinyPosition(int selectedPawnPositionInArray)
        {
            switch (selectedPawnPositionInArray)
            {
                
            }
            return false;
        }

        private void MovePawn(int selectedPawnPosition, int pawnDestinyPosition)
        {
            GamePawnsPositions[selectedPawnPosition, 1] = null;
            GamePawnsPositions[pawnDestinyPosition, 1] = SelectedPawn;
        }

        private void SwitchActivePlayer()
        {
            if (ActivePlayer == "Player")
            {
                ActivePlayer = "Bot";
            }
            else if (ActivePlayer == "Bot")
            {
                ActivePlayer = "Player";
            }
            SelectedPawn = null;
        }

        private void CheckWinner()
        {

        }
    }
}
