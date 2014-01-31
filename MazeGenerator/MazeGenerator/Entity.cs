using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenGLGame1
{
    public abstract class Entity
    {
        protected Vector2 position;
        public Texture2D texture;

        public Entity(Vector2 pos, Texture2D tx)
        {
            position = pos;
            texture = tx;
        }

        public virtual void draw(GameTime gameTime, SpriteBatch batch)
        {
            // batch.Draw(texture, new Rectangle((int)(position.X), (int)position.Y, 10, 10), Color.White * 0.1f);
        }

        public void setPos(float X, float Y)
        {
            position.X = X;
            position.Y = Y;
        }

        internal void update()
        {
            
        }
    }
}
