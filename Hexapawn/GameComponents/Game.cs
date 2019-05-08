using Hexapawn.Exceptions;
using Hexapawn.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexapawn.GameComponents
{
    public class Game
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public Player Winner { get; private set; }
        private Player ActivePlayer { get; set; }
        public Board Board { get; private set; }
        public int Turn { get; private set; }

        public Game()
        {
            Turn = 1;
            Player1 = new Human();
            Player2 = new Bot();
            Board = new Board(this);
            ActivePlayer = Player1; // First turn

        }

        public void Move(string[] move)
        {
            string selectedPawn = move[0]; // e.g: P1, K1
            string pawnDestiny = move[1]; // e.g: A2, B2
            int selectedPawnPositionInArray = 0;
            int pawnDestinyPositionInArray = 0;

            IsExistingPawn();
            IsActivePlayerThePawnsOwner();
            IsPawnStillOnBoard();
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

        #region Basic Validations

        private void IsExistingPawn()
        {
            //if (!PlayerPawns.Contains(SelectedPawn) && !BotPawns.Contains(SelectedPawn))
            //{
            //    throw new MoveException("Peça não encontrada");
            //}
        }

        private void IsPawnStillOnBoard()
        {
            //for (int i = 0; i < Board.GetLength(0); i++)
            //{
            //    if (SelectedPawn == Board[i, 1])
            //    {
            //        return;
            //    }
            //}

            throw new MoveException("Está peça não está mais em jogo.");
        }

        private void IsActivePlayerThePawnsOwner()
        {
            //if (ActivePlayer == "Player")
            //{
            //    if (!PlayerPawns.Contains(SelectedPawn))
            //    {
            //        throw new MoveException("Apenas selecione suas peças!");
            //    }
            //}
            //else
            //{
            //    if (!BotPawns.Contains(SelectedPawn))
            //    {
            //        throw new MoveException("Apenas selecione suas peças!");
            //    }
            //}
        }

        private void IsExistingDestiny(string pawnDestiny)
        {
            //for (int i = 0; i < Board.GetLength(0); i++)
            //{
            //    if (Board[i, 0] == pawnDestiny)
            //    {
            //        return;
            //    }
            //}
            //throw new MoveException("Escolha um destino existente.");
        }

        #endregion
       
        private int GetSelectedPawnPositionInArray()
        {
            //for (int i = 0; i < Board.GetLength(0); i++)
            //{
            //    if (SelectedPawn == Board[i, 1])
            //    {
            //        return i;
            //    }
            //}
            throw new MoveException("Erro ao pegar posição da peça no array");
        }

        private int GetPawnDestinyPositionInArray(string pawnDestiny)
        {
            //for (int i = 0; i < Board.GetLength(0); i++)
            //{
            //    if (pawnDestiny == Board[i, 0])
            //    {
            //        return i;
            //    }
            //}
            throw new MoveException("Erro ao pegar posição do destino no array");
        }

        #region LegalMove Validations

        private bool IsLegalMove(int selectedPawnPositionInArray, int pawnDestinyPositionInArray)
        {
            // Legal moves
            //IsMovingToTheSamePosition(selectedPawnPositionInArray, pawnDestinyPositionInArray); //ok
            //IsMovingSideways(selectedPawnPositionInArray, pawnDestinyPositionInArray); // arrumar default
            //IsMovingBackwards(selectedPawn, selectedPawnPositionInArray, pawnDestinyPositionInArray);
            //IsMovingForward();


            //IsDiagonalMove(selectedPawnPositionInArray, pawnDestinyPositionInArray); // arrumar default
            //HasAPawnInDestinyPosition(selectedPawnPositionInArray);
            return true;
        }

        private bool HasAPawnInDestinyPosition(int selectedPawnPositionInArray)
        {
            switch (selectedPawnPositionInArray)
            {
                
            }
            return false;
        }

        #endregion

        private void MovePawn(int selectedPawnPosition, int pawnDestinyPosition)
        {
            //Board[selectedPawnPosition, 1] = null;
            //Board[pawnDestinyPosition, 1] = SelectedPawn;
            //SelectedPawn = null;
        }

        private void SwitchActivePlayer()
        {
            //if (ActivePlayer == "Player")
            //{
            //    ActivePlayer = "Bot";
            //}
            //else if (ActivePlayer == "Bot")
            //{
            //    ActivePlayer = "Player";
            //}
        }

        private void CheckWinner()
        {

        }
    }
}
