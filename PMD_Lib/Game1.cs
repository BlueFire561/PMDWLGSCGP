using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PMD_Lib
{
    public enum TileIndexes
    {
        Grass,
        Path,
        Ice,
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D manTexture;
        private Vector2 manPos = new Vector2(0, 0);
        private int manSpe = 4;
        private Texture2D grass;
        private Texture2D path;
        private Texture2D ice;
        private int[,] tileMap = new int[200, 200];
        private Vector2 camPos = new Vector2(0, 0);
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            System.Console.WriteLine("hello");

            for (int x = 0; x < tileMap.GetLength(0); x++)
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    tileMap[x, y] = (int)TileIndexes.Path;
                }

        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            manTexture = Content.Load<Texture2D>("man");
            path = Content.Load<Texture2D>("path");
            ice = Content.Load<Texture2D>("ice");
            grass = Content.Load<Texture2D>("grass");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Vector2 manVec = new Vector2();
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                manVec.Y--;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                manVec.X--;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                manVec.Y++;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                manVec.X++;
            if (manVec != Vector2.Zero)
                manVec.Normalize();
            manPos += manVec * manSpe;

            camPos = new Vector2(manPos.X - 200, manPos.Y - 100);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SeaShell);

            int scale = 2;
            Matrix matrix = new Matrix(new Vector4(scale, 0, 0, 0),
                                       new Vector4(0, scale, 0, 0),
                                       new Vector4(0, 0, 1, 0),
                                       new Vector4(-camPos.X * scale, -camPos.Y * scale, 0, 1));

            spriteBatch.Begin(transformMatrix: matrix, samplerState: SamplerState.PointClamp);
      
            // Draw tilemap
            for (int x = 0; x < tileMap.GetLength(0); x++)
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    if (tileMap[x, y] == (int)TileIndexes.Grass)
                    {
                        spriteBatch.Draw(grass, new Vector2(x * 16, y * 16), Color.White);
                    }
                    else if (tileMap[x, y] == (int)TileIndexes.Path)
                    {
                        spriteBatch.Draw(path, new Vector2(x * 16, y * 16), Color.White);
                    }
                    else if (tileMap[x, y] == (int)TileIndexes.Ice)
                    {
                        spriteBatch.Draw(ice, new Vector2(x * 16, y * 16), Color.White);
                    }
                }

            spriteBatch.Draw(manTexture, manPos, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
