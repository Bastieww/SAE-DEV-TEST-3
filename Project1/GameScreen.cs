using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended;

namespace SAE
{
    public class GameScreen : MonoGame.Extended.Screens.GameScreen
    {
        private Game1 gamescreen;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public GameScreen(Game1 game) : base(game)
        {
            gamescreen = game;
        }
        public override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        { }
        public override void Draw(GameTime gameTime)
        {
            gamescreen.GraphicsDevice.Clear(Color.Red); // on utilise la reference vers
                                                         // Game1 pour chnager le graphisme
        }
    }
}