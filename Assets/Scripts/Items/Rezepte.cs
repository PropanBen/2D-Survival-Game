﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rezepte : MonoBehaviour
{
    public string name;
    public int rescount;
    public int dropdownnumber;
    public int LvL;
    public string buttonnumber;
    public string description;
    public string Resource1;
    public string Resource2;
    public string Resource3;
    public string Resource4;
    public int Resource1count;
    public int Resource2count;
    public int Resource3count;
    public int Resource4count;
    public Sprite Icon;
    GameObject Rezept;
    public static List<Rezepte> Handwerksrezepteliste = new List<Rezepte>();
    public static List<Rezepte> Werkzeugliste = new List<Rezepte>();
    public static List<Rezepte> Essensliste = new List<Rezepte>();
    public static List<Rezepte> Kleidungsliste = new List<Rezepte>();
    public static List<Rezepte> Buildinglist = new List<Rezepte>();
    public static List<Rezepte> Weaponlist = new List<Rezepte>();
    public static List<Rezepte> ListofLists = new List<Rezepte>();
    public static List<Rezepte> Otherlist = new List<Rezepte>();

    void Start()
    {
        Werkzeugliste.Add(Axe_wood);
        Werkzeugliste.Add(Axe_stone);
        Werkzeugliste.Add(Axe_copper);
        Werkzeugliste.Add(Axe_bronze);
        Werkzeugliste.Add(Axe_iron);
        Werkzeugliste.Add(Axe_gold);
        Werkzeugliste.Add(Axe_diamond);
        Werkzeugliste.Add(Pickaxe_wood);
        Werkzeugliste.Add(Pickaxe_stone);
        Werkzeugliste.Add(Pickaxe_copper);
        Werkzeugliste.Add(Pickaxe_bronze);
        Werkzeugliste.Add(Pickaxe_iron);
        Werkzeugliste.Add(Pickaxe_gold);
        Werkzeugliste.Add(Pickaxe_diamond);
        Buildinglist.Add(Chest);
        Buildinglist.Add(Oven);
        Weaponlist.Add(Bow);
        Weaponlist.Add(Arrow_stone);
        Weaponlist.Add(Spear);
        Weaponlist.Add(Sword_wood);
        Weaponlist.Add(Sword_stone);
        Weaponlist.Add(Sword_copper);
        Weaponlist.Add(Sword_bronze);
        Weaponlist.Add(Sword_iron);
        Weaponlist.Add(Sword_gold);
        Weaponlist.Add(Sword_diamond);
        Otherlist.Add(Linen);
        Otherlist.Add(Rope);
        Otherlist.Add(Flint);
        Otherlist.Add(Bronzeingot);
        Otherlist.Add(Bottle);
        Otherlist.Add(Healpotion);
        Otherlist.Add(Torch);
    }

    void Update()
    {
    }

// Constructor 1 Resource
public Rezepte(string name,int LvL, int rescount,string buttonnumber, string Resource1, int Resource1count, string description)
    {
        this.name = name;
        this.LvL = LvL;
        this.rescount = rescount;
        this.buttonnumber = buttonnumber;
        this.Resource1 = Resource1;
        this.Resource1count = Resource1count;
        this.description = description;
    }

    // Constructor 2 Resourcen
    public Rezepte(string name, int LvL, int rescount, string buttonnumber, string Resource1, int Resource1count, string Resource2, int Resource2count, string description)
    {
        this.name = name;
        this.LvL = LvL;
        this.rescount = rescount;
        this.buttonnumber = buttonnumber;
        this.Resource1 = Resource1;
        this.Resource2 = Resource2;
        this.Resource1count = Resource1count;
        this.Resource2count = Resource2count;
        this.description = description;
    }

    // Constructor 3 Resourcen
    public Rezepte(string name, int LvL, int rescount, string buttonnumber, string Resource1, int Resource1count, string Resource2, int Resource2count, string Resource3, int Resource3count, string description)
    {
        this.name = name;
        this.LvL = LvL;
        this.rescount = rescount;
        this.buttonnumber = buttonnumber;
        this.Resource1 = Resource1;
        this.Resource2 = Resource2;
        this.Resource3 = Resource3;
        this.Resource1count = Resource1count;
        this.Resource2count = Resource2count;
        this.Resource3count = Resource3count;
        this.description = description;
    }
    // Constructor 4 Resourcen
    public Rezepte(string name, int LvL, int rescount, string buttonnumber, string Resource1, int Resource1count, string Resource2, int Resource2count, string Resource3, int Resource3count, string Resource4, int Resource4count, string description)
    {
        this.name = name;
        this.LvL = LvL;
        this.rescount = rescount;
        this.buttonnumber = buttonnumber;
        this.Resource1 = Resource1;
        this.Resource2 = Resource2;
        this.Resource3 = Resource3;
        this.Resource4 = Resource4;
        this.Resource1count = Resource1count;
        this.Resource2count = Resource2count;
        this.Resource3count = Resource3count;
        this.Resource4count = Resource4count;
        this.description = description;
    }

    // Zugriff auf Rezepte
    // Rezepte.Axtrezept.description


    // Instanz (Object) von Rezept erstellen

    //*****************************************************************//
    //  HIER HANDWERKSREZEPTE EINFÜGEN                                 //
    //*****************************************************************//


    // Werkzeugrezepte  Dropdownmenu value = 1

    // Axes -------------------------------------------------------------------------------------------------------------------------------------------
    public static Rezepte Axe_wood = new Rezepte("Axe_wood",0, 2,"1", "Rope",1, "Twig",3, "To chop down trees.\nDamage 5 \n"+
                                            "Resources needed : \n1xRope, 3xTwig");
    public static Rezepte Axe_stone = new Rezepte("Axe_stone", 1, 3, "2", "Rope", 1, "Twig", 1,"Stone",2, "To chop down trees.\nDamage 10\n"  +
                                        "Resources needed :\n1xRope, 1xTwig, 2xStone");
    public static Rezepte Axe_copper = new Rezepte("Axe_copper", 2, 3, "3", "Rope", 1, "Twig", 1, "Copperingot", 2, "To chop down trees.\nDamage 15\n" +
                                    "Resources needed :\n1xRope, 1xTwig, 2xCopperingot");
    public static Rezepte Axe_bronze = new Rezepte("Axe_bronze", 3, 3, "4", "Rope", 1, "Twig", 1, "Bronzeingot", 2, "To chop down trees.\nDamage 25\n" +
                                "Resources needed :\n1xRope, 1xTwig, 2xBronzeingot");
    public static Rezepte Axe_iron = new Rezepte("Axe_iron", 4, 3, "5", "Rope", 1, "Twig", 1, "Ironingot", 2, "To chop down trees.\nDamage 30\n" +
                            "Resources needed :\n1xRope, 1xTwig, 2xIroningot");
    public static Rezepte Axe_gold = new Rezepte("Axe_gold", 5, 3, "6", "Rope", 1, "Twig", 1, "Goldingot", 2, "To chop down trees.\nDamage 35\n" +
                        "Resources needed :\n1xRope, 1xTwig, 2xGoldingot");
    public static Rezepte Axe_diamond = new Rezepte("Axe_diamond", 6, 3, "7", "Rope", 1, "Twig", 1, "Diamond", 2, "To chop down trees.\nDamage 40\n" +
                    "Resources needed :\n1xRope, 1xTwig, 2xDiamond");
    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // Pickaxes
    public static Rezepte Pickaxe_wood = new Rezepte("Pickaxe_wood", 0, 2, "8", "Rope", 1, "Twig", 3, "To chop down trees.\nDamage 5 \n" +
                                         "Resources needed : \n1xRope, 3xTwig");
    public static Rezepte Pickaxe_stone = new Rezepte("Pickaxe_stone", 1, 3, "9", "Rope", 1, "Twig", 1, "Stone", 2, "To chop down trees.\nDamage 10\n" +
                                    "Resources needed :\n1xRope, 1xTwig, 2xStone");
    public static Rezepte Pickaxe_copper = new Rezepte("Pickaxe_copper", 2, 3, "10", "Rope", 1, "Twig", 1, "Copperingot", 2, "To chop down trees.\nDamage 15\n" +
                                "Resources needed :\n1xRope, 1xTwig, 2xCopperingot");
    public static Rezepte Pickaxe_bronze = new Rezepte("Pickaxe_bronze", 3, 3, "11", "Rope", 1, "Twig", 1, "Bronzeingot", 2, "To chop down trees.\nDamage 25\n" +
                            "Resources needed :\n1xRope, 1xTwig, 2xBronzeingot");
    public static Rezepte Pickaxe_iron = new Rezepte("Pickaxe_iron", 4, 3, "12", "Rope", 1, "Twig", 1, "Ironingot", 2, "To chop down trees.\nDamage 30\n" +
                        "Resources needed :\n1xRope, 1xTwig, 2xIroningot");
    public static Rezepte Pickaxe_gold = new Rezepte("Pickaxe_gold", 5, 3, "13", "Rope", 1, "Twig", 1, "Goldingot", 2, "To chop down trees.\nDamage 35\n" +
                    "Resources needed :\n1xRope, 1xTwig, 2xGoldingot");
    public static Rezepte Pickaxe_diamond = new Rezepte("Pickaxe_diamond", 6, 3, "14", "Rope", 1, "Twig", 1, "Diamond", 2, "To chop down trees.\nDamage 40\n" +
                "Resources needed :\n1xRope, 1xTwig, 2xDiamond");

    // Essen  Dropdownmenu value = 2

    // Kleidungsrezepte  Dropdownmenu value = 3

    // Gebäuderezepte  Dropdownmenu value = 4

    public static Rezepte Chest = new Rezepte("Chest", 0, 3, "1", "Rope", 1, "Twig", 5, "Stone", 1, "You can Store Items in the Chest." +
                                                "Resources needed : 1xRope,5xTwig,1xStone");
    public static Rezepte Oven = new Rezepte("Oven", 0, 2, "2", "Stone", 10, "Clay", 10,"Melt Ores to Ingots or roast consumabels." +
                                            "Resources needed : 10xStone,10xClay");

    // Waffen Dropdownmenu value = 5

    public static Rezepte Bow = new Rezepte("Bow", 3, 3, "1", "Stone", 1, "Twig", 1, "Tendon", 5, "For Hunting\n" +
                                                   "Damage 10\nResources needed :\n1xStone, 1xTwig, 5xTendon");
    public static Rezepte Arrow_stone = new Rezepte("Arrow_stone", 3, 3, "2", "Stone", 1,"Twig",1,"Feather",1, "Ammunition for the Bow\n" +
                                                   "Damage 10\nResources needed :\n1xStone, 1xTwig, 1xFeather");
    public static Rezepte Spear = new Rezepte("Spear", 2, 3, "3", "Flint", 1, "Twig", 3, "Stone", 1, "Throw on Enemeys\n" +
                                               "Damage 10\nResources needed :\n1xFlint, 3xTwig, 1xStone");
    // Schwert--------------------------------------------------------------------------------------------------------------------------------------
    public static Rezepte Sword_wood = new Rezepte("Sword_wood", 0, 2, "4", "Rope", 1, "Twig", 3, "A lousy Sword.\nDamage 10 \n" +
                                     "Resources needed : \n1xRope, 3xTwig");
    public static Rezepte Sword_stone = new Rezepte("Sword_stone", 1, 3, "5", "Rope", 1, "Twig", 1, "Stone", 2, "Better than lousy Wood.\nDamage 20\n" +
                                "Resources needed :\n1xRope, 1xTwig, 2xStone");
    public static Rezepte Sword_copper = new Rezepte("Sword_copper", 2, 3, "6", "Rope", 1, "Twig", 1, "Copperingot", 2, "A much more better Sword.\nDamage 30\n" +
                            "Resources needed :\n1xRope, 1xTwig, 2xCopperingot");
    public static Rezepte Sword_bronze = new Rezepte("Sword_bronze", 3, 3, "7", "Rope", 1, "Twig", 1, "Bronzeingot", 2, "Made from Copper an Tin.\nDamage 40\n" +
                        "Resources needed :\n1xRope, 1xTwig, 2xBronzeingot");
    public static Rezepte Sword_iron = new Rezepte("Sword_iron", 4, 3, "8", "Rope", 1, "Twig", 1, "Ironingot", 2, "A Sword made from good Iron.\nDamage 50\n" +
                    "Resources needed :\n1xRope, 1xTwig, 2xIroningot");
    public static Rezepte Sword_gold = new Rezepte("Sword_gold", 5, 3, "9", "Rope", 1, "Twig", 1, "Goldingot", 2, "A Sword shining in the sun.\nDamage 60\n" +
                "Resources needed :\n1xRope, 1xTwig, 2xGoldingot");
    public static Rezepte Sword_diamond = new Rezepte("Sword_diamond", 6, 3, "10", "Rope", 1, "Twig", 1, "Diamond", 2, "A Sword there is nothing harder.\nDamage 70\n" +
              "Resources needed :\n1xRope, 1xTwig, 2xDiamond");

    // Other Dropdownmenu value = 6

    public static Rezepte Linen = new Rezepte("Linen", 0, 1, "1", "Gras", 3, "For making Ropes\n" +
                                               "Resources needed :\n3xGras");
    public static Rezepte Rope = new Rezepte("Rope", 0, 1, "2", "Linen", 3, "For making Tools\n" +
                                           "Resources needed :\n3xLinen");
    public static Rezepte Flint = new Rezepte("Flint", 0, 1, "3", "Firestone", 1, "For making Fire and sharp Items\n" +
                                       "Resources needed :\n1xFirestone");
    public static Rezepte Bronzeingot = new Rezepte("Bronzeingot", 0, 2, "4", "Copperingot", 1,"Tiningot",1, "The only way to craft bronze\n" +
                                   "Resources needed :\n1xCopperingot, 1xTiningot");
    public static Rezepte Bottle = new Rezepte("Bottle", 0, 1, "5", "Glas", 2, "To store liquids\n" +
                                   "Resources needed :\n2xGlas");
    public static Rezepte Healpotion = new Rezepte("Healpotion", 0, 2, "6", "Bottle", 1,"Healplant",2, "A Potion to heal yourself\n" +
                                    "Healing : 30\nResources needed :\n1xBottle 2xHealplant");
    public static Rezepte Torch = new Rezepte("Torch", 0, 2, "7", "Twig", 1, "Resin", 1, "Spends Light in the Night\n" +
                                "Resources needed :\n1xTwig 1xResin");





}








