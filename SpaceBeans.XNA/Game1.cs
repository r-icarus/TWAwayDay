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
		private Textures textures;
		private readonly SpaceBeansGame game;
		private IDecisionModel decisionModel;
		private IPointerInput lastMouseInput;
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
			
			textures = new Textures(graphics.GraphicsDevice, Content);
		}
	#endregion

	#region Update and Draw

		protected override void Update(GameTime gameTime)
		{
			lastMouseInput = getCurrentPointerInput();
			while(!game.IsOver && (null == decisionModel || decisionModel.Update(lastMouseInput)))
			{
				var spaceBeansDecision = game.Decisions.FirstOrDefault();
				if(null == spaceBeansDecision)
				{
					break;
				}
				decisionModel = createModelForDecision(spaceBeansDecision);
				if(game.IsOver)
				{
					break;
				}
				lastMouseInput = getCurrentPointerInput();
			}
					
			base.Update(gameTime);
		}

		private IPointerInput getCurrentPointerInput()
		{
#if WINDOWS_PHONE || IOS || ANDROID
			if(null == lastMouseInput)
			{
				lastMouseInput = new TouchInput();
			}
			return new TouchInput((TouchInput)lastMouseInput);
#else
			if (null == lastMouseInput) {
				lastMouseInput = new MouseInput();
			}
			return new MouseInput((MouseInput)lastMouseInput);
#endif
		}

		private IDecisionModel createModelForDecision(ISpaceBeansDecision decision)
		{
			if(decision is DrawDecision)
			{
				return new DrawDecisionModel((DrawDecision)decision, textures);
			} else if(decision is SellDecision)
			{
				return new SellDecisionModel((SellDecision)decision, textures);
			} else if(decision is BuyDecision)
			{
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

			if(game.IsOver)
			{
			} else
			{
				decisionModel.DrawModel(spriteBatch);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}
		#endregion
	}
}
