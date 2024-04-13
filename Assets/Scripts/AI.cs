using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour
{
    SlotManager[] slotManagers = new SlotManager[9];
    // Start is called before the first frame update
    void Start()
    {
        this.slotManagers = FindObjectOfType<PlayerControl>().slotManagers;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int evaluate()
    {
        // Checking for Rows for X or O victory. 
        for (int i = 0; i <= 6; i += 3)
        {
            if (slotManagers[0 + i].Occupied != false && slotManagers[1 + i].Occupied != false && slotManagers[2 + i].Occupied != false
                && slotManagers[0 + i].isCircle == slotManagers[1 + i].isCircle
                && slotManagers[1 + i].isCircle == slotManagers[2 + i].isCircle)
            {
                if (GameManager.instance.StartSide == GameManager.PlayerSide.Circle)
                {
                    return +10;
                }
                else
                {
                    return -10;
                }
            }
        }

        // Checking for Columns for X or O victory.
        for (int i = 0; i < 3; i++)
        {
            if (slotManagers[0 + i].Occupied != false && slotManagers[3 + i].Occupied != false && slotManagers[6 + i].Occupied != false
                && slotManagers[0 + i].isCircle == slotManagers[3 + i].isCircle
                && slotManagers[3 + i].isCircle == slotManagers[6 + i].isCircle)
            {
                if (GameManager.instance.StartSide == GameManager.PlayerSide.Circle)
                {
                    return +10;
                }
                else
                {
                    return -10;
                }
            }
        }

        // Checking for Diagonals for X or O victory. 
        if (slotManagers[0].Occupied != false && slotManagers[4].Occupied != false && slotManagers[8].Occupied != false
                && slotManagers[0].isCircle == slotManagers[4].isCircle
                && slotManagers[4].isCircle == slotManagers[8].isCircle)
        {
            if (GameManager.instance.StartSide == GameManager.PlayerSide.Circle)
            {
                return +10;
            }
            else
            {
                return -10;
            }
        }

        if (slotManagers[2].Occupied != false && slotManagers[4].Occupied != false && slotManagers[6].Occupied != false
                && slotManagers[2].isCircle == slotManagers[4].isCircle
                && slotManagers[4].isCircle == slotManagers[6].isCircle)
        {
            if (GameManager.instance.StartSide == GameManager.PlayerSide.Circle)
            {
                return +10;
            }
            else
            {
                return -10;
            }
        }

        bool allOccupied = true;
        for (int i = 0; i < slotManagers.Length; i++)
        {
            if (!slotManagers[i].Occupied)
            {
                allOccupied = false;
                break;
            }
        }
        if (allOccupied)
        {
            return 0;  // Tie game
        }

        // Else if none of them have won then return 0 
        return 0;
    }

    // This is the minimax function. It considers all 
    // the possible ways the game can go and returns 
    // the value of the board 
    int minimax(int depth, bool isMax)
    {
        int score = evaluate();

        // Early exit if a terminal state is reached
        if (score == 10 || score == -10)
            return score - depth * (isMax ? 1 : -1);

        bool allOccupied = true;
        for (int i = 0; i < slotManagers.Length; i++)
        {
            if (!slotManagers[i].Occupied)
            {
                allOccupied = false;
                break;
            }
        }
        if (allOccupied) return 0;

        // If this maximizer's move 
        if (isMax)
        {
            int best = -10000;

            // Traverse all cells 
            for (int i = 0; i < 9; i++)
            {
                if (!slotManagers[i].Occupied)
                {
                    // Make the move 
                    slotManagers[i].ShowCircle();

                    // Call minimax recursively and choose 
                    // the maximum value 
                    best = Math.Max(best, minimax(depth + 1, !isMax));

                    // Undo the move 
                    slotManagers[i].UndoMove();
                }
            }
            return best;
        }

        // If this minimizer's move 
        else
        {
            int best = 10000;

            // Traverse all cells 
            for (int i = 0; i < 9; i++)
            {
                // Check if cell is empty 
                if (!slotManagers[i].Occupied)
                {
                    // Make the move 
                    slotManagers[i].ShowCross();

                    // Call minimax recursively and choose 
                    // the minimum value 
                    best = Math.Min(best,minimax(
                                    depth + 1, !isMax));

                    // Undo the move 
                    slotManagers[i].UndoMove();
                }
            }
            return best;
        }
    }

    // This will return the best possible 
    // move for the player 
    int findBestMove()
    {
        int bestVal = -1000;
        int bestMove = -1;

        // Traverse all cells, evaluate minimax function  
        // for all empty cells. And return the cell  
        // with optimal value. 
        for (int i = 0; i < 9; i++)
        {
            // Check if cell is empty 
            if (!slotManagers[i].Occupied)
            {
                // Make the move 
                slotManagers[i].ShowObject();

                // compute evaluation function for this 
                // move. 
                int moveVal = minimax(0, true);

                // Undo the move 
                slotManagers[i].UndoMove();

                // If the value of the current move is 
                // more than the best value, then update 
                // best/ 
                if (moveVal > bestVal)
                {
                    bestMove = i;
                    bestVal = moveVal;
                }
            }
        }

        return bestMove;
    }

    public void MakeBestMove()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        int index = findBestMove();
        if(index == -1)
        {
            Debug.Log("No more valid Moves");
        }
        else
        {
            slotManagers[index].ShowObject();
        }
        
    }

}
