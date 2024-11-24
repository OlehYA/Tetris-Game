﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Game
{
    public class GameState
    {
        public Block currentBlock;

        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();
            }
        }

        public GameGrid GameGrid { get; }

        public BlockQueue BlockQueue { get; }

        public bool GameOver { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
        }

        private bool BlockFrist()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if(!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }

            return true;
        }

        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            
            if(!BlockFrist())
            {
                CurrentBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();

            if(!BlockFrist())
            {
                CurrentBlock.RotateCW();
            }
        }

        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);

            if(!BlockFrist())
            {
                CurrentBlock.Move(0, 1);   
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if(!BlockFrist())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        private bool IsGameOver()
        {
            return !(GameGrid.IsRoowEmpty(0) && GameGrid.IsRoowEmpty(1));
        }

        private void PlaceBlock()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }

            GameGrid.ClearFullRows();

            if(IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
            }
        }

        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);
            
            if(!BlockFrist())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }
    }
}