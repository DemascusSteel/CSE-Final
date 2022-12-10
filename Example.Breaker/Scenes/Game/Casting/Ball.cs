using System;
using System.Numerics;
using Byui.Games.Casting;



namespace Example.Breaker.Game
{
    public class Ball : Byui.Games.Casting.Image
    {
        public Ball() { }

        public int _lives = 3;

        public bool IsDead()
        {
            return _lives <= 0;
        }

        public void RemoveLife()
        {
            _lives -= 1;
        }
        public void BounceX()
        {
            Vector2 velocity = GetVelocity();
            float x = velocity.X *= -1;
            float y = velocity.Y;
            Vector2 direction = new Vector2(x, y);
            Steer(direction);
        }

        public void BounceY()
        {
            Vector2 velocity = GetVelocity();
            float x = velocity.X;
            float y = velocity.Y *= -1;
            Vector2 direction = new Vector2(x, y);
            Steer(direction);
        }

        public void BounceCorner()
        {
            Vector2 velocity = GetVelocity();
            float x = velocity.X *= -1;
            float y = velocity.Y *= -1;
            Vector2 direction = new Vector2(x, y);
            Steer(direction);
        }

        public virtual bool OverlapsTop(Actor other)
        {
            return (this.GetLeft() < other.GetRight() && this.GetRight() > other.GetLeft()
                && this.GetTop() < other.GetBottom() && this.GetBottom() > other.GetTop()
                && this.GetBottom() < other.GetTop() + 20);
        }
        public virtual bool OverlapsBottom(Actor other)
        {
            return (this.GetLeft() < other.GetRight() && this.GetRight() > other.GetLeft()
                && this.GetTop() < other.GetBottom() && this.GetBottom() > other.GetTop()
                && this.GetTop() > other.GetBottom() - 20);
        }
        public virtual bool OverlapsLeft(Actor other)
        {
            return (this.GetLeft() < other.GetRight() && this.GetRight() > other.GetLeft()
                && this.GetTop() < other.GetBottom() && this.GetBottom() > other.GetTop()
                && this.GetRight() < other.GetLeft() + 20);
        }
        public virtual bool OverlapsRight(Actor other)
        {
            return (this.GetLeft() < other.GetRight() && this.GetRight() > other.GetLeft()
                && this.GetTop() < other.GetBottom() && this.GetBottom() > other.GetTop()
                && this.GetLeft() > other.GetRight() - 20);
        }
    }
}
