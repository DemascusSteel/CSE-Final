using System;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;
using Example.Breaker.Shared;


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
            title.Display("OVER SCENE");
            title.MoveTo(320, 200);
            title.Align(Label.Center);

            Label instructions = new Label();
            instructions.Display("press 'enter' to return to the menu");
            instructions.MoveTo(320, 240);
            instructions.Align(Label.Center);

            Image explosion = new Image();
            string image = settingsService.GetString("Overexplode");
            explosion.Display(image);
            explosion.SizeTo(300, 300);
            explosion.MoveTo(320, 240);

           
            // Instantiate the actions that use the actors.

            LoadSceneAction loadSceneAction = new LoadSceneAction(serviceFactory);
            DrawActorsAction drawActorsAction = new DrawActorsAction(serviceFactory);

            // Clear the given scene, add the actors and actions.
            scene.Clear();
            scene.AddActor("title", title);
            scene.AddActor("instructions", instructions);
            scene.AddActor("explosion", explosion);
            scene.AddAction(Phase.Input, loadSceneAction);
            scene.AddAction(Phase.Output, drawActorsAction);
        }
    }
}

