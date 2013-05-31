#region File Description
//-----------------------------------------------------------------------------
// SpaceBeans.MacOSGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion
#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

using SpaceBeans.Xna;

#endregion
namespace SpaceBeans.MacOS
{
	/// <summary>
	/// Default Project Template
	/// </summary>
	public class Game1 : Game
	{
	    #region Fields
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D logoTexture;
	    private Textures textures;

	    private readonly SpaceBeansGame game;
	    private IDecisionModel decisionModel;

	    private MouseInput lastMouseInput = new MouseInput();

	    #endregion

	#region Initialization

		public Game1(SpaceBeansGame game)
		{
			this.game = game;

			graphics = new GraphicsDeviceManager(this);
			
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = false;
		    graphics.PreferredBackBufferWidth = 320;
		    graphics.PreferredBackBufferHeight = 568;
            //TargetElapsedTime = TimeSpan.FromMilliseconds(250);
		    this.IsMouseVisible = true;

#if WINDOWS_PHONE || IOS || ANDROID
				TargetElapsedTime = TimeSpan.FromTicks(333333);
				graphics.IsFullScreen = true;
			#endif
		}
		/// <summary>
		/// Overridden from the base Game.Initialize. Once the GraphicsDevice is setup,
		/// we'll use the viewport to initialize some values.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
		}
		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be use to draw textures.
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			
			// TODO: use this.Content to load your game content here eg.
            logoTexture = Content.Load<Texture2D>("logo");

            textures = new Textures(graphics.GraphicsDevice);
        }
	#endregion

	#region Update and Draw

		/// <summary>
        	/// Allows the game to run logic such as updating the world,
        	/// checking for collisions, gathering input, and playing audio.
        	/// </summary>
        	/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
            lastMouseInput = new MouseInput(lastMouseInput);
            while(null == decisionModel || decisionModel.Update(lastMouseInput)) {
                decisionModel = createModelForDecision(game.Decisions.First());
            }
            		
			base.Update(gameTime);
		}

        private IDecisionModel createModelForDecision(ISpaceBeansDecision decision) {
            if(decision is DrawDecision) {
                return new DrawDecisionModel((DrawDecision)decision, textures);
            } else if(decision is SellDecision) {
                return new SellDecisionModel((SellDecision)decision, textures);
            } else if(decision is BuyDecision) {
                return new BuyDecisionModel((BuyDecision)decision, textures);
            }
            return null;
        }
		/// <summary>
		/// This is called when the game should draw itself. 
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// Clear the backbuffer
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			// draw the logo
            spriteBatch.Draw(logoTexture, new Vector2(130, 200), Color.White);

		    decisionModel.DrawModel(spriteBatch);

		    spriteBatch.End();

			base.Draw(gameTime);
        }

	    #endregion
	}
}
