using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YourEngine;

namespace YourGame.LevelLogic
{
    public partial class Level : GameObject
    {
       
        public void LoadLevel(string levelName)
        {
            StreamReader myReader = new StreamReader(levelName);
            string line = myReader.ReadLine();
            List<string> lines = new List<string>();
            while (line != null)
            {
                lines.Add(line);
                line = myReader.ReadLine();
            }

            
            foreach (string s in lines)
            {
                switch (s)
                {
                    case ".":
                        this.AddChild(new Tiles(Tiles.Type.Empty)); break;
                    case "P":
                        this.AddChild(new Tiles(Tiles.Type.Platform)); break;
                    case "W":
                        this.AddChild(new Tiles(Tiles.Type.Wall)); break;
                }
            }
        }

        private void LoadTile(string symbol)
        {

        }
    }
}
