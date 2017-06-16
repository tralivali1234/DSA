﻿using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.InteropServices.ComTypes;
using Link;

namespace BinarySearchTree
{
    public class BsTree<TKey, TValue> : Dictionary<TKey, TValue> where TKey : IComparable
    {
        private BinNode<TKey, TValue> _rootNode;
        
        public int Length { get; private set; }

        public BsTree()
        {
            _rootNode = null;
            Length = 0;
        }

        public new void Clear()
        {
            _rootNode = null;
            Length = 0;
        }

        public void Insert(TKey key, TValue value)
        {
            _rootNode = InsertHelper(_rootNode, key, value);
            Length++;
        }

        private static BinNode<TKey, TValue> InsertHelper(BinNode<TKey, TValue> rootNode, TKey key, TValue value)
        {
            if (rootNode == null)
                return new BinNode<TKey, TValue>(key, value);
            if (rootNode.Key.CompareTo(key) > 0)
                rootNode.LeftNode = InsertHelper((BinNode<TKey,TValue>) rootNode.LeftNode, key, value);
            else
                rootNode.RightNode = InsertHelper((BinNode<TKey, TValue>)rootNode.RightNode, key, value);
            return rootNode;
        }

        public new TValue Remove(TKey key)
        {
            var temp = FindHelper(_rootNode, key);
            if (temp == null) return default(TValue);
            _rootNode = RemoveHelper(_rootNode, key);
            Length--;
            return temp;
        }

        public TValue Find(TKey key)
        {
            return FindHelper(_rootNode, key);
        }

        private static TValue FindHelper(BinNode<TKey, TValue> rootNode, TKey key)
        {
            if (rootNode == null) return default(TValue);
            if (rootNode.Key.CompareTo(key) > 0)
            {
                return FindHelper((BinNode<TKey,TValue>)rootNode.LeftNode, key);
            }
            return rootNode.Key.CompareTo(key) == 0 ? rootNode.Element : FindHelper((BinNode<TKey, TValue>) rootNode.RightNode, key);
        }

        private static BinNode<TKey, TValue> RemoveHelper(BinNode<TKey, TValue> rootNode, TKey key)
        {
            if (rootNode == null)
                return null;
            if (rootNode.Key.CompareTo(key) > 0)
                rootNode.LeftNode = RemoveHelper((BinNode<TKey, TValue>) rootNode.LeftNode, key);
            else if (rootNode.Key.CompareTo(key) < 0)
                rootNode.RightNode = RemoveHelper((BinNode<TKey, TValue>) rootNode.RightNode, key);
            else
            {
                if (rootNode.LeftNode == null)
                    return (BinNode<TKey, TValue>) rootNode.LeftNode;
                if (rootNode.RightNode == null)
                    return (BinNode<TKey, TValue>) rootNode.RightNode;
                var temp = GetMin((BinNode<TKey, TValue>) rootNode.RightNode);
                rootNode.Element = temp.Element;
                rootNode.Key = temp.Key;
                rootNode.RightNode = DeleteMin(rootNode.RightNode);
            }

            return rootNode;

        }

        private static IBNode<TValue> DeleteMin(IBNode<TValue> bNode)
        {
            if (bNode.LeftNode == null)
                return bNode.RightNode;
            bNode.LeftNode = DeleteMin(bNode.LeftNode);
            return bNode;
        }

        private static BinNode<TKey, TValue> GetMin(BinNode<TKey, TValue> rootNode)
        {
            while (true)
            {
                if (rootNode.LeftNode == null) return rootNode;
                rootNode = (BinNode<TKey, TValue>) rootNode.LeftNode;
            }
        }

        public void Print()
        {
            Inorder(_rootNode);
        }

        private static void Inorder(BinNode<TKey, TValue> rt)
        {
            if (rt == null) return;
            Inorder((BinNode<TKey, TValue>)rt.LeftNode);
            Console.WriteLine(rt.Key + " " + rt.Element);
            Inorder((BinNode<TKey, TValue>)rt.RightNode);
        }
    }
}