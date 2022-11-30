using System;
using System.Numerics;
using Byui.Games.Casting;


namespace Example.Breaker.Game
{
    public class Tank : Byui.Games.Casting.Image
    {
        private Ball _ball;
        
        public Tank() { }

        public bool HasBall()
        {
            return _ball != null;
        }
        public Ball GetBall()
        {
            return _ball;
        }

        public override void Move()
        {
            base.Move();
            if (_ball != null)
            {
                float x = this.GetCenterX() - _ball.GetWidth() / 2;
                float y = this.GetTop() - _ball.GetHeight();
                _ball.MoveTo(x, y);
            }
        }
        public void SteerBall(float directionX, float directionY){
             if (_ball != null)
            {
            _ball.Steer(directionX, directionY);
            }
            
        }

        public void AttachBall(Ball ball)
        {
            _ball = ball;
        }

        public void ReleaseBall()
        {            
            _ball = null;
        }
    }
}