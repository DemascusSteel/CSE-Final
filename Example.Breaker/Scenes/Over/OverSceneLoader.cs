using System;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;
using Example.Breaker.Shared;
using Example.Breaker.Game;


namespace Example.Breaker.Over
{
    public class OverSceneLoader : SceneLoader
    {
        public OverSceneLoader(IServiceFactory serviceFactory) : base(serviceFactory) { }
        public override void Load(Scene scene)
        {
            // Set the background color
            GetServiceFactory().GetVideoService().SetBackground(Color.Gray());
            IServiceFactory serviceFactory = GetServiceFactory();
            ISettingsService settingsService = serviceFactory.GetSettingsService();

            

            // Create the actors that participate in the scene.
            Label title = new Label();
            title.Display("Game Over");
            title.MoveTo(1024/2, 768/3);
            title.Align(Label.Center);

            
            Label winner = new Label();

            Tank tank1 = scene.GetFirstActor<Tank>("tank1");

            if(tank1.IsAlive() == 0)
            {
                winner.Display("Red Tank Wins");
            }
             else
            {
                winner.Display("Green Tank Wins");
            }
            
            winner.MoveTo(1024/2, 768/3+400);
            winner.Align(Label.Center);

            Label instructions = new Label();
            instructions.Display("press 'M' to return to the menu");
            instructions.MoveTo(1024/2, 768/3+300);
            instructions.Align(Label.Center);

            Image explosion = new Image();
            string image = settingsService.GetString("Overexplode");
            explosion.Display(image);
            explosion.SizeTo(804,768);
            explosion.MoveTo((1024-804)/2,0);

           
            // Instantiate the actions that use the actors.

            LoadSceneAction loadSceneAction = new LoadSceneAction(serviceFactory);
            DrawActorsAction drawActorsAction = new DrawActorsAction(serviceFactory);

            // Clear the given scene, add the actors and actions.
            scene.Clear();
            scene.AddActor("title", title);
            scene.AddActor("winner", winner);
            scene.AddActor("instructions", instructions);
            scene.AddActor("explosion", explosion);
            scene.AddAction(Phase.Input, loadSceneAction);
            scene.AddAction(Phase.Output, drawActorsAction);
        }
    }
}

