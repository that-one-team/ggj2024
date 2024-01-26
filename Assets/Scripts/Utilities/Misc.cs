using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MiscItems
{
    public static List<string> NPCNames = new() {
        "Lily Sparkle",
        "Charlie Bubblegum",
        "Mia Sunshine",
        "Oliver Giggles",
        "Sophie Dimples",
        "Noah Snugglebug",
        "Zoe Cupcake",
        "Ethan Twinkletoes",
        "Ava Sweetpea",
        "Logan Snickerdoodle",
        "Emma Sprinkles",
        "Caleb Honeybun",
        "Grace Puddingpop",
        "Jackson Bumblebee",
        "Chloe Cuddlebug",
        "Lucas Marshmallow",
        "Harper Lollipop",
        "Benjamin Jellybean",
        "Ella Peaches",
        "Mason Gumdrop"
     };
}

public static class Utilities
{
    public static T SelectRandom<T>(this IEnumerable<T> list)
    {
        var rnd = new System.Random();
        return list.ElementAt(rnd.Next(list.Count()));
    }

}