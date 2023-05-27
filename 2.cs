using System;
using System.Collections.Generic;

namespace CardClasses
{
    public class Domino
    {
        public int SideA { get; set; }  // Represents the value of one side of the domino.
        public int SideB { get; set; }  // Represents the value of the other side of the domino.

        public Domino(int sideA, int sideB)
        {
            SideA = sideA;
            SideB = sideB;
        }

        public override string ToString()
        {
            return $"{SideA}-{SideB}";  // Returns a string representation of the domino, showing its sides separated by a hyphen.
        }
    }

    public class DominoBoneyard
    {
        private List<Domino> dominos = new List<Domino>();  // List to store the dominos in the boneyard.

        public DominoBoneyard(int maxDots)
        {
            // Generates all possible dominos with values from 0 to maxDots (inclusive) on each side.
            for (int i = 0; i <= maxDots; i++)
            {
                for (int j = i; j <= maxDots; j++)
                {
                    dominos.Add(new Domino(i, j));  // Creates a new domino with values i and j, and adds it to the boneyard.
                }
            }
        }

        public int NumDominosRemaining
        {
            get { return dominos.Count; }  // Returns the number of dominos remaining in the boneyard.
        }

        public Domino GetDomino(int position)
        {
            if (position >= 0 && position < dominos.Count)  // Checks if the position is within the valid range.
            {
                return dominos[position];  // Returns the domino at the specified position.
            }
            else
            {
                throw new ArgumentOutOfRangeException("position", "Invalid position");  // Throws an exception if the position is invalid.
            }
        }

        public void ChangeDomino(int position, Domino newDomino)
        {
            if (position >= 0 && position < dominos.Count)  // Checks if the position is within the valid range.
            {
                dominos[position] = newDomino;  // Replaces the domino at the specified position with a new domino.
            }
            else
            {
                throw new ArgumentOutOfRangeException("position", "Invalid position");  // Throws an exception if the position is invalid.
            }
        }

        public Domino DrawDomino()
        {
            if (!IsEmpty)  // Checks if the boneyard is not empty.
            {
                Domino domino = dominos[0];  // Takes the first domino from the boneyard.
                dominos.RemoveAt(0);  // Removes the first domino from the boneyard.
                return domino;  // Returns the drawn domino.
            }
            return null;  // Returns null if the boneyard is empty.
        }

        public bool IsEmpty
        {
            get { return dominos.Count == 0; }  // Returns true if the boneyard is empty, false otherwise.
        }

        public void Shuffle()
        {
            Random random = new Random();  // Creates a new instance of the Random class to generate random numbers.
            int n = dominos.Count;  // Stores the number of dominos in the boneyard.
            while (n > 1)  // While there are at least two dominos remaining.
            {
                n--;  // Decreases the number of remaining dominos.
                int k = random.Next(n + 1);  // Generates a random number between 0 and n (inclusive).
                Domino temp = dominos[k];  // Stores the domino at position k in a temporary variable.
                dominos[k] = dominos[n];  // Replaces the domino at position k with the domino at the last position (n).
                dominos[n] = temp;  // Places the domino originally at position k into the last position (n).
            }
        }

        public override string ToString()
        {
            string output = "";
            foreach (Domino domino in dominos)
            {
                output += domino.ToString() + "\n";  // Builds a string representation of all dominos in the boneyard, separated by newlines.
            }
            return output;
        }
    }
}
