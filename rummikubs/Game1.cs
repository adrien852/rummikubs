using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace rummikubs
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<Kub> drawKubs { get; set; }
        private List<List<Kub>> tableKubs { get; set; }
        private AI aiPlayer { get; set; }
        private KeyboardState previousState { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            tableKubs = new List<List<Kub>>();
            drawKubs = new List<Kub>()
            {
                new Kub(5,"B",true),
                new Kub(6,"G",true),
                new Kub(10,"B",true),
                new Kub(10,"R",true),
                new Kub(7,"G",true),
                new Kub(9,"G",true),
                new Kub(4,"R",true),
                new Kub(8,"G",true),
                new Kub(2,"B",true),
                new Kub(3,"B",true),
                new Kub(4,"G",true),
                new Kub(7,"B",true),
                new Kub(7,"R",true),
            };
            aiPlayer = new AI(false);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !previousState.IsKeyDown(Keys.Enter)) {
                aiPlayer.PlayTurn(drawKubs);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !previousState.IsKeyDown(Keys.Space)) {
                aiPlayer.ShowHand();
                ShowDraw();
            }
                

            previousState = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void ShowDraw()
        {
            String kubString = "DRAW = ";
            foreach (Kub kub in drawKubs)
            {
                kubString += kub.score + kub.color + " ";
            }
            Console.WriteLine(kubString);
        }
    }
}
