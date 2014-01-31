﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace OpenGLGame1
{
    public class Board : Unmoveable
    {
        Texture2D blocktext;
        Rectangle mainFrame;
        Random rand;
        List<Cell> cells = new List<Cell>();

        List<Block> blocks;

        public Board(Texture2D tx, Vector2 pos, Rectangle mf, Texture2D bltext, Random random)
            : base(pos, tx)
        {
            this.rand = random;
            this.mainFrame = mf;
            blocks = new List<Block>();
            blocktext = bltext;
            this.generateMazePlan();
        }

        public void generateField()
        {
            Vector2 pos = new Vector2();
            pos.X = 450;
            pos.Y = 180;
            Block b = new Block(blocktext, pos, null, 0);
            b.SetParent(b);
            blocks.Add(b);
            for (int i = 1; i < this.cells.Count; i++)
            {
                bool setNewRoot = false;
                Vector2 newpos = new Vector2();
                Cell C = new Cell();
                int k = this.blocks.Count - 1;
                Block pre = this.blocks.ToArray()[k];
                // Random generation of board blocks!
                // Variables:
                bool BuildBlock = false;
                int loc = rand.Next(1, 4);

                // Loop
                bool NotFound = true;
                while (NotFound)
                {
                    if (setNewRoot)
                        k = rand.Next(0, this.blocks.Count);
                    pre = this.blocks.ToArray()[k];
                    loc = rand.Next(1, 5);
                    // Check Direction to be Built
                    switch (loc)
                    {
                        case 1:
                            newpos.X = pre.Apos.X - 30;
                            newpos.Y = pre.Apos.Y;
                            break;
                        case 2:
                            newpos.X = pre.Apos.X;
                            newpos.Y = pre.Apos.Y - 30;
                            break;
                        case 3:
                            newpos.X = pre.Apos.X + 30;
                            newpos.Y = pre.Apos.Y;
                            break;
                        case 4:
                            newpos.X = pre.Apos.X;
                            newpos.Y = pre.Apos.Y + 30;
                            break;
                        default:
                            break;
                    }

                    if (newpos.X < mainFrame.Width && newpos.Y < mainFrame.Height && newpos.X >= 0 && newpos.Y >= 0)
                    {
                        foreach (Cell c in this.cells)
                        {
                            if (c.X == newpos.X && c.Y == newpos.Y)
                            {
                                C = c;
                                C.visited = true;
                                int freeNeighbors = 0;
                                // check for neighbors
                                if (!isBlockNeighbor((int)newpos.X + 30, (int)newpos.Y))
                                    //if(!isVisitedNeighbor((int)newpos.X + 30, (int)newpos.Y))
                                    freeNeighbors++;
                                if (!isBlockNeighbor((int)newpos.X, (int)newpos.Y + 30))
                                    //if(!isVisitedNeighbor((int)newpos.X, (int)newpos.Y + 30))
                                    freeNeighbors++;
                                if (!isBlockNeighbor((int)newpos.X - 30, (int)newpos.Y))
                                    //if(!isVisitedNeighbor((int)newpos.X - 30, (int)newpos.Y))
                                    freeNeighbors++;
                                if (!isBlockNeighbor((int)newpos.X, (int)newpos.Y - 30))
                                    //if (!isVisitedNeighbor((int)newpos.X, (int)newpos.Y - 30))
                                    freeNeighbors++;
                                if (freeNeighbors >= 3)
                                {
                                    NotFound = false;
                                    BuildBlock = true;
                                }
                                if (freeNeighbors < 3)
                                    setNewRoot = true;
                            }
                        }
                    }
                    if (blocks.Count > 3100)
                        return;
                }

                if (BuildBlock)
                {
                    Block newBlock = new Block(blocktext, newpos, pre, loc);
                    newBlock.cell = C;
                    C.setBlock(newBlock);
                    this.blocks.Add(newBlock);
                    pre.SetChild(newBlock);
                    //System.Console.WriteLine(C.id);
                }
            }
        }

        public bool isVisitedNeighbor(int X, int Y)
        {
            foreach (Cell c in this.cells)
            {
                if (c.X == X && c.Y == Y && c.visited)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isBlockNeighbor(int X, int Y)
        {
            foreach (Cell c in this.cells)
            {
                if (c.X == X && c.Y == Y && c.isBlock)
                {
                    return true;
                }
            }
            return false;
        }

        public void generateMazePlan()
        {
            int ID = 0;
            for (int i = 0; i < mainFrame.Width / 30; i++)
            {
                for (int j = 0; j < mainFrame.Height / 30; j++)
                {
                    Cell c = new Cell(i * 30, j * 30);
                    c.id = ID + 1;
                    ID++;
                    this.cells.Add(c);
                }
            }
        }

        internal void AddtoEntities(List<Entity> entities)
        {
            foreach (Block b in blocks)
            {
                entities.Add(b);
            }
        }

        public bool isOnABlock(float x, float y)
        {
            foreach (Block b in blocks)
            {
                if (b.isInside(x, y))
                    return true;
            }
            return false;
        }


        public Block getCurrentBlock(float x, float y)// returns null if No Block!
        {
            foreach (Block b in blocks)
            {
                if (b.isInside(x, y))
                    return b;
            }
            return null;
        }

        public Cell getCurrentCell(float x, float y)// returns null if No Cell!
        {
            foreach (Cell c in cells)
            {
                if (c.isInside(x, y))
                    return c;
            }
            return null;
        }

        public int whichWayisBlocked(Vector2 pc)// return 8 points around 
        {                                       // the Point of Collision 
            if (!isOnABlock(pc.X - 3, pc.Y - 3))// in 3 units of distance
                return 1;                       // else returns 0
            else if (!isOnABlock(pc.X, pc.Y - 3))
                return 2;
            else if (!isOnABlock(pc.X + 3, pc.Y - 3))
                return 3;
            else if (!isOnABlock(pc.X - 3, pc.Y))
                return 4;
            else if (!isOnABlock(pc.X + 3, pc.Y))
                return 5;
            else if (!isOnABlock(pc.X - 3, pc.Y + 3))
                return 6;
            else if (!isOnABlock(pc.X, pc.Y + 3))
                return 7;
            else if (!isOnABlock(pc.X + 3, pc.Y + 3))
                return 8;
            return 0;
        }
    }
}

