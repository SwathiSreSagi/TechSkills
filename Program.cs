using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    public class Program
    {
        static void Main(string[] args)
        {
            var MaxCoordinates = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToList();
            var StartIndex = Console.ReadLine().Trim().Split(' ');
            var getPos = StartIndex.Length;
            Position position = new Position();

            if (StartIndex.Count() == 3)
            {
                position.X = Convert.ToInt32(StartIndex[0]);
                position.Y = Convert.ToInt32(StartIndex[1]);
                position.Direction = (Directions)Enum.Parse(typeof(Directions), StartIndex[2]);
            }

            var MoveDirection = Console.ReadLine().ToUpper();

            try
            {
                position.StartMoving(MaxCoordinates, MoveDirection);
                Console.WriteLine($"{position.X} {position.Y} {position.Direction.ToString()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
    public enum Directions
    {
        N = 1,
        S = 2,
        E = 3,
        W = 4
    }

    public interface IPosition
    {
        void StartMoving(System.Collections.Generic.List<int> MaxCoordinates, string MoveDirection);
    }
    public class Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Directions Direction { get; set; }

        public Position()
        {
            X = Y = 0;
            Direction = Directions.N;
        }

        private void Move90Left()
        {
            switch (this.Direction)
            {
                case Directions.N:
                    this.Direction = Directions.W;
                    break;
                case Directions.S:
                    this.Direction = Directions.E;
                    break;
                case Directions.E:
                    this.Direction = Directions.N;
                    break;
                case Directions.W:
                    this.Direction = Directions.S;
                    break;
                default:
                    break;
            }
        }

        private void Move90Right()
        {
            switch (this.Direction)
            {
                case Directions.N:
                    this.Direction = Directions.E;
                    break;
                case Directions.S:
                    this.Direction = Directions.W;
                    break;
                case Directions.E:
                    this.Direction = Directions.S;
                    break;
                case Directions.W:
                    this.Direction = Directions.N;
                    break;
                default:
                    break;
            }
        }

        private void MoveSameDirectionWithOneStep()
        {
            switch (this.Direction)
            {
                case Directions.N:
                    this.Y += 1;
                    break;
                case Directions.S:
                    this.Y -= 1;
                    break;
                case Directions.E:
                    this.X += 1;
                    break;
                case Directions.W:
                    this.X -= 1;
                    break;
                default:
                    break;
            }
        }

        public void StartMoving(List<int> MaxCoordinates, string MoveDirection)
        {
            foreach (var move in MoveDirection)
            {
                switch (move)
                {
                    case 'M':
                        this.MoveSameDirectionWithOneStep();
                        break;
                    case 'L':
                        this.Move90Left();
                        break;
                    case 'R':
                        this.Move90Right();
                        break;
                    default:
                        break;
                }

                if (this.X < 0 || this.X > MaxCoordinates[0] || this.Y < 0 || this.Y > MaxCoordinates[1])
                {
                    throw new Exception($"Position can not be outside bounderies (0 , 0) and ({MaxCoordinates[0]} , {MaxCoordinates[1]})");
                }
            }
        }
    }

}
