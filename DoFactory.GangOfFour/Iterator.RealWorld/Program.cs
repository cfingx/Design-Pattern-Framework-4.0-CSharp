﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator.RealWorld
{
    class Program
    {
        static void Main()
        {
            Collection collection = new Collection();
            collection[0] = new Item("Item 0");
            collection[1] = new Item("Item 1");
            collection[2] = new Item("Item 2");
            collection[3] = new Item("Item 3");
            collection[4] = new Item("Item 4");
            collection[5] = new Item("Item 5");
            collection[6] = new Item("Item 6");
            collection[7] = new Item("Item 7");
            collection[8] = new Item("Item 8");

            Iterator iterator = new Iterator(collection);

            // skip every other item
            iterator.Step = 2;

            Console.WriteLine("Iterating over colelction:");

            for (Item item = iterator.First(); !iterator.IsDone; item = iterator.Next())
            {
                Console.WriteLine(item.Name);
            }

            Console.ReadKey();
        }
    }

    class Item
    {
        private string _name;

        public Item(string name)
        {
            this._name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }

    interface IAbstractCollection
    {
        Iterator CreateIterator();
    }

    class Collection : IAbstractCollection
    {
        private ArrayList _items = new ArrayList();

        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public object this[int index]
        {
            get { return _items[index]; }
            set { _items.Add(value); }
        }
    }

    interface IAbstractInterator
    {
        Item First();
        Item Next();
        bool IsDone { get; }
        Item CurrentItem { get; }
    }

    class Iterator : IAbstractInterator
    {
        private Collection _collection;
        private int _current = 0;
        private int _step = 1;

        public Iterator(Collection collection)
        {
            this._collection = collection;
        }

        public Item CurrentItem
        {
            get
            {
                return _collection[_current] as Item;
            }
        }

        public bool IsDone
        {
            get
            {
                return _current >= _collection.Count;
            }
        }

        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }

        public Item First()
        {
            _current = 0;
            return _collection[_current] as Item;
        }

        public Item Next()
        {
            _current += _step;
            if (!IsDone)
                return _collection[_current] as Item;
            else
                return null;
        }
    }
}
