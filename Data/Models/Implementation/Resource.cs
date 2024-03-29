﻿namespace Data.Models.Implementation;

public class Resource : IClassNamePrimaryKey
{
    public string ClassName { get; set; }
    public Item Item { get; set; }
    public virtual ICollection<Miner> Miners { get; set; } = new List<Miner>();
}
