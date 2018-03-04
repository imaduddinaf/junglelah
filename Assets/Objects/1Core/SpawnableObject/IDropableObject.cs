using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// such as monsters, breakable objects, etc
public interface IDropableObject {
    List<Tuple<IItem, float>> drops { get; set; }

    List<IItem> GetDrops();
}
