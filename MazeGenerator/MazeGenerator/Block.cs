using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenGLGame1
{
    public class Block : Unmoveable
    {
        public Vector2 Apos;
        public Block parent;
        public Block child;
        public Cell cell;// Corresponding Cell
        public int loc; // relative location of block to the parent: from 0 to 4 , 0 for root 

        public Block(Texture2D tx, Vector2 pos, Block p, int l)
            : base(pos, tx)
        {
            Apos = pos;
            parent = p;
            loc = l;
        }

        public void SetChild(Block c)
        {
            child = c;
        }

        public void SetParent(Block p)
        {
            parent = p;
        }

        public override void draw(GameTime gameTime, SpriteBatch batch)
        {
            batch.Draw(texture, new Rectangle((int)(position.X), (int)position.Y, 30, 30),Color.White*1f);// *0.1f);
        }

        internal bool isInside(float x, float y)
        {
            if (x >= this.position.X && x <= this.position.X + 30 && y >= this.position.Y && y <= this.position.Y + 30)
                return true;
            return false;
        }
    }
}
