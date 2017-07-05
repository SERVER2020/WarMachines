using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Linq;

public class LevelReader
{
    private int _levelToRead = 0;
    private Queue<EnemyData> _enemiesToSpawn = new Queue<EnemyData>();
    public Queue<EnemyData> EnemiesToSpawn { get { return _enemiesToSpawn; } }

    public LevelReader(int level)
    {
        _levelToRead = level;
        _ReadLevel();
    }

    private void _ReadLevel()
    {
        XmlReader reader = XmlReader.Create(Application.dataPath + "\\level" + _levelToRead + ".xml");

        reader.ReadStartElement("LEVEL" + _levelToRead);
        EnemyData enemy = new EnemyData();
        while (reader.Read())
        {
            XElement element;
            if (reader.Name == "ENEMY")
            {
                element = (XElement)XElement.ReadFrom(reader);
                enemy.name = element.Value;
            }
            else if (reader.Name == "SPAWNPOSITION")
            {
                element = (XElement)XElement.ReadFrom(reader);
                enemy.spawnPosition = float.Parse(element.Value);
            }
            else if (reader.Name == "TIME")
            {
                element = (XElement)XElement.ReadFrom(reader);
                enemy.timeToSpawn = float.Parse(element.Value);
                _enemiesToSpawn.Enqueue(enemy);
            }
        }
    }
}
