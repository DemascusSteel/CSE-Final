using System;
using Byui.Games.Casting;
using Byui.Games.Scripting;
using Byui.Games.Services;


namespace Example.Breaker.Over
{
    public class DrawActorsAction : Byui.Games.Scripting.Action
    {
        private IVideoService _videoService;

        public DrawActorsAction(IServiceFactory serviceFactory)
        {
            _videoService = serviceFactory.GetVideoService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
               Label title = scene.GetFirstActor<Label>("title");
               Label winner = scene.GetFirstActor<Label>("winner");
               Label instructions = scene.GetFirstActor<Label>("instructions");
               Image explosion = scene.GetFirstActor<Image>("explosion");

                _videoService.Draw(explosion);
                _videoService.ClearBuffer();
                _videoService.Draw(title);
                _videoService.Draw(winner);
                _videoService.Draw(instructions);
                _videoService.FlushBuffer();
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't draw actors.", exception);
            }
        }
    }
}