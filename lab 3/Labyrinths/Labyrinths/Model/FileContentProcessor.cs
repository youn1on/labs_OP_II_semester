namespace Labyrinths.Model
{

    public class FileContentProcessor
    {
        public static int[][] GetLabyrinthMatrix(string[] fileContent)
        {
            int[][] labyrinth = new int[fileContent.Length][];
            for (int i = 0; i < fileContent.Length; i++)
            {
                labyrinth[i] = new int[fileContent[i].Length / 2 + 1];
                for (int j = 0; j < fileContent[i].Length; j += 2)
                {
                    labyrinth[i][j / 2] = fileContent[i][j] == ' ' ? 0 : 1;
                }
            }

            return labyrinth;
        }

    }
}