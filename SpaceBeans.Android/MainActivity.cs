using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using CodePhile;
using CodePhile.Games;
using SpaceBeans;
using System.Collections.Generic;
using System.Linq;

namespace SpaceBeans.Android
{
    [Activity(Label = "SpaceBeans", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            TextView pvpTextView = FindViewById<TextView>(Resource.Id.pvp);
            pvpTextView.Click += (obj,e) =>
            {
                var setup = new SpaceBeansGameSetup();
                IEnumerable<AndroidPlayer> players = Enumerable.Range(1, 2).Select(i => new LocalPlayer(i)).ToArray();
                foreach (var trader in players.Select(p => p.Trader))
                {
                    setup.AddTrader(trader);
                }
                var game = new SpaceBeansGame(setup);
                new GamePlayer(game, players.Append(new LocalSystem())).PlayGame();


            };
            
        }
        
        private class GamePlayer
        {
            private readonly SpaceBeansGame game;
            private readonly AndroidPlayer[] players;

            public GamePlayer(SpaceBeansGame game, IEnumerable<AndroidPlayer> players)
            {
                this.game = game;
                this.players = players.ToArray();
            }

            public void PlayGame()
            {
                game.Start();
                //while (!game.IsOver)
                //{
                //    foreach (var decision in game.Decisions.ToArray())
                //    {
                //        players.First(p => decision.Trader == p.Trader).MakeDecision(decision);
                //    }
                //}
                
                //TODO: Game flow
            }
        }

        private class LocalSystem : AndroidPlayer
        {
            public override void MakeDecision(ISpaceBeansDecision decision)
            {
                ForEachUntil(GetDecisionMakers(), m => m.MakeDecision(decision));
            }

            private IEnumerable<IDecisionMaker> GetDecisionMakers()
            {
                yield return new GameSetupDecisionMaker();
            }
        }

        internal class GameSetupDecisionMaker : DecisionMaker<SetupDrawPileDecision>
        {
            public override void MakeDecision(SetupDrawPileDecision decision)
            {
                var allCards = StandardRules.GenerateStandardCards();
                allCards = Randomize(allCards);
                decision.AddBeans(allCards);
            }

            private static IEnumerable<T> Randomize<T>(IEnumerable<T> source)
            {
                var sourceCopy = source.ToList();
                var rand = new Random();
                while (sourceCopy.Count > 0)
                {
                    var nextIndex = rand.Next(0, sourceCopy.Count - 1);
                    yield return sourceCopy[nextIndex];
                    sourceCopy.RemoveAt(nextIndex);
                }
            }
        }
    }
}

