using System;
using System.Collections.Generic;
using System.Text;
using DDD.Domain.Entities;

namespace DDD.Domain
{
    public class Car
    {
        public IReadOnlyList<Wheel> Wheels => _wheels;

        public Engine Engine { get; private set; }
        private List<Wheel> _wheels;
        public string BodyColour { get; private set; }
        private List<Light> _lights;
        public IReadOnlyList<Light> Lights => _lights;
        public bool IsShipped { get; private set; }

        public Car(List<Light> lights, List<Wheel> wheels, string bodyColour, Engine engine, bool isShipped)
        {
            _lights = lights;
            _wheels = wheels;
            BodyColour = bodyColour;
            Engine = engine;
            IsShipped = isShipped;
        }

        /// <summary>
        /// We apply some business rules
        /// </summary>
        /// <param name="colour"></param>
        public void PaintBody(string colour)
        {
            if (colour == "yellow")
            {
                throw new ArgumentException("No yellow cars are allowed");
            }
            if (_lights.Count > 0)
            {
                throw new ArgumentException("Some lights are already installed");
            }
            BodyColour = colour;
        }
    }
}
