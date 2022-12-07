using System;
using System.Numerics;
using Byui.Games.Casting;
using Byui.Games.Services;
using Byui.Games.Scripting;


namespace Example.Breaker.Game
{
    public class ActorFactory
    {
        private ISettingsService _settingsService;

        public ActorFactory(ISettingsService settingsService) 
        {
            _settingsService = settingsService;
        }

        public Ball CreateBall(Tank tank)
        {
            
            string image = _settingsService.GetString("ballImage");
            float width = _settingsService.GetFloat("ballWidth");
            float height = _settingsService.GetFloat("ballHeight");
            // float x = _settingsService.GetFloat("ballX");
            // float y = _settingsService.GetFloat("ballY") "ballY";
            float directionX = _settingsService.GetFloat("ballVelocity");
            float directionY = directionX *= -1;

            Ball ball = new Ball();
            ball.Display(image);
            ball.SizeTo(width, height);
            ball.Steer(directionX, directionY);
            tank.AttachBall(ball);
            return ball;

        }

        public Actor CreateField()
        {
            float width = _settingsService.GetFloat("fieldWidth");
            float height = _settingsService.GetFloat("fieldHeight");
            float x = _settingsService.GetFloat("fieldX");
            float y = _settingsService.GetFloat("fieldY");

            Actor field = new Actor();
            field.SizeTo(width, height);
            field.MoveTo(x, y);

            return field;
        }

        public Level CreateLevel()
        {
            Level level = new Level();
            level.MoveTo(0, 0);
            level.Align(Label.Left);
            return level;
        }

        // public Lives CreateLives()
        // {
        //     int x = _settingsService.GetInt("screenWidth");
        //     int startingLives = _settingsService.GetInt("startingLives");
            
        //     Lives lives = new Lives(startingLives);
        //     lives.MoveTo(x, 0);
        //     lives.Align(Label.Right);

        //     return lives;
        // }

        public Tank CreateTank(string xPos, string yPos, string tankImage)
        {
            string image = _settingsService.GetString(tankImage);
            float durationInSeconds = _settingsService.GetFloat("tankAnimationLength");
            int framesPerSecond = _settingsService.GetInt("frameRate");
            float width = _settingsService.GetFloat("tankWidth");
            float height = _settingsService.GetFloat("tankHeight");
            float x = _settingsService.GetFloat(xPos);
            float y = _settingsService.GetFloat(yPos);
            
            Tank tank = new Tank();
           tank.Display(image);
            tank.SizeTo(width, height);
            tank.MoveTo(x, y);

            return tank;
        }
                public Image CreateWall(int x,int y)
        {
            
            float width = _settingsService.GetFloat("wallWidth");
            float height = _settingsService.GetFloat("wallHeight");
            string image = _settingsService.GetString("wallImage");
            
            Image wall = new Image();
            wall.Display(image);
            wall.SizeTo(width, height);
            wall.MoveTo(x,y);

            return wall;
        }

        public Score CreateScore()
        {
            int x = (int) _settingsService.GetFloat("screenWidth") / 2;
            int startingLives = _settingsService.GetInt("startingLives");
            
            Score score = new Score();
            score.MoveTo(x, 0);
            score.Align(Label.Center);

            return score;
        }
        
    }
}