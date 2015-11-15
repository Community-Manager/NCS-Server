namespace NeighboursCommunitySystem.Server.Common.Generators
{
    using System;

    public class RandomStringGenerator
    {
        private Random Generator = new Random();
        private const string Symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()_+~-";

        /// <summary>
        /// Standard implementation for a random string generator.
        /// Probably the fastest and most intuitive solution.
        /// </summary>
        /// <param name="length">The desired length if the string.</param>
        /// <returns>Returns a random string with the required character length.</returns>
        public string GetString(int length)
        {
            var tokenLength = Generator.Next(length + 1);
            var result = new char[tokenLength];

            for (int i = 0; i < tokenLength; i++)
            {
                result[i] = Symbols[Generator.Next(0, Symbols.Length - 1)];
            }

            return new string(result);
        }
    }
}
