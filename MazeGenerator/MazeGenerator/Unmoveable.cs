using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OpenGLGame1
{
    public abstract class Unmoveable : Entity
    {
        public Unmoveable(Vector2 pos, Texture2D tx)
            : base(pos, tx)
        {

        }
    }
}
